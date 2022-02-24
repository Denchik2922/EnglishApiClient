using Blazored.Toast.Services;
using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.Dtos.Test;
using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace EnglishApiClient.Pages.Test
{
    public partial class MultipleMatchingTest
    {
        [Parameter]
        public int DictionaryId { get; set; }

        [Parameter]
        public int TypeId { get; set; }

        public string SelectedWordName = "";
        public string SelectedTranslate = "";

        public bool IsFinishedPartOfTest
        {
            get
            {
                var userAnswers = trueAnswers.Count + wrongAnswers.Count;
                var totalQuestion = _paramsForTest.WordNames.Count + _paramsForTest.Translates.Count;

                return userAnswers == totalQuestion;
            }
        }

        public ICollection<string> trueAnswers { get; set;} = new List<string>();
        public ICollection<string> wrongAnswers { get; set; } = new List<string>();

        private TestParameters _parameters { get; set; } = new TestParameters();
        private MultipleMatchingQuestion _paramsForTest { get; set; } = new MultipleMatchingQuestion();
        private ParamsForCheck _paramsCheck { get; set; }

        [Inject]
        private IMultipleMatchingTestHttpService _matchingTestHttp { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        [Inject]
        private AuthenticationStateProvider _authProvider { get; set; }

        private string StyleForInput(string value)
        {
            if (SelectedWordName.Contains(value) || SelectedTranslate.Contains(value))
            {
                return "background:lightblue";
            }
            if (trueAnswers.Contains(value))
            {
                return "background:lightgreen";
            }
            if (wrongAnswers.Contains(value))
            {
                return "background: lightcoral;color: white;";
            }
            else
            {
                return "";
            }
        }

        public async Task SetWordName(string wordName)
        {
            if(!trueAnswers.Contains(wordName) && !wrongAnswers.Contains(wordName))
            {
                SelectedWordName = wordName;
                await CheckForSend();
            }
        }

        public async Task SetTranslate(string translate)
        {
            if (!trueAnswers.Contains(translate) && !wrongAnswers.Contains(translate))
            {
                SelectedTranslate = translate;
                await CheckForSend();
            }
        }

        public async Task CheckForSend()
        {
            if(!String.IsNullOrEmpty(SelectedWordName) && !String.IsNullOrEmpty(SelectedTranslate))
            {
                await CheckTest();
                AddAnswers();
                StateHasChanged();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await StartTest();
        }

        private async Task StartTest()
        {
            _parameters = await _matchingTestHttp.StartTest(DictionaryId);
            await GetTest();
        }

        private async Task NextQuestion()
        {
            _parameters.CurrentQuestion = _paramsCheck.NextQuestion;
            await GetTest();
            _paramsCheck = null;
        }

        private async Task GetTest()
        {
            _paramsForTest = await _matchingTestHttp.GetPartOfTest(_parameters);
        }

        private async Task CheckTest()
        {
            var answer = new ParamsFoAnswer()
            {
                Parameters = _parameters,
                Answer = SelectedTranslate,
                Question = SelectedWordName
            };

            _paramsCheck = await _matchingTestHttp.CheckQuestion(answer);
            _parameters = _paramsCheck.Parameters;
        }

        private void AddAnswers()
        {

            if (_paramsCheck.IsTrueAnswer)
            {
                trueAnswers.Add(SelectedWordName);
                trueAnswers.Add(SelectedTranslate);
            }
            else
            {
                wrongAnswers.Add(SelectedWordName);
                wrongAnswers.Add(SelectedTranslate);
            }

            SelectedWordName = "";
            SelectedTranslate = "";
        }

        private async Task FinishTest()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            var userId = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var testResult = new TestResult()
            {
                EnglishDictionaryId = _parameters.DictionaryId,
                Score = _parameters.Score,
                UserId = userId,
                Date = DateTime.Now,
                TypeOfTestingId = TypeId
            };

            var result = await _matchingTestHttp.FinishTest(testResult);

            if (result)
            {
                _toastService.ShowSuccess("Test finished successfully!");
                await JSRuntime.InvokeVoidAsync("history.back");
            }
        }
    }
}
