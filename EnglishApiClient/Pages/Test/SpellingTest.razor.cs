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
    public partial class SpellingTest
    {
        [Parameter]
        public int DictionaryId { get; set; }

        [Parameter]
        public int TypeId { get; set; }

        public bool IsShowCheck { get; set; } = true;
        public bool IsDisabledAnswers { get; set; }

        public SpellingAnswerModel SpellingModel = new SpellingAnswerModel();

        private TestParameters _parameters { get; set; } = new TestParameters();
        private ParamsForSpellingQuestion _paramsForTest { get; set; } = new ParamsForSpellingQuestion();
        private ParamsForCheck _paramsCheck { get; set; }

        [Inject]
        private ISpellingTestHttpService _spellingTestHttp { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        [Inject]
        private AuthenticationStateProvider _authProvider { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await StartTest();
        }

        private string StyleForAnswer()
        {
            if (_paramsCheck != null && _paramsCheck.IsTrueAnswer == true)
            {
                return "background:lightgreen";
            }
            else if (_paramsCheck != null && _paramsCheck.IsTrueAnswer == false)
            {
                return "background:red;color:white;";
            }
            else
            {
                return "";
            }
        }

        private async Task StartTest()
        {
            _parameters = await _spellingTestHttp.StartTest(DictionaryId);
            await GetTest();
        }

        private async Task NextQuestion()
        {
            _parameters.CurrentQuestion = _paramsCheck.NextQuestion;
            await GetTest();
            _paramsCheck = null;
            SpellingModel.AnswerForQuestion = "";
            IsDisabledAnswers = false;
            IsShowCheck = true;
        }

        private async Task GetTest()
        {
            _paramsForTest = await _spellingTestHttp.GetPartOfTest(_parameters);
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

            var result = await _spellingTestHttp.FinishTest(testResult);

            if (result)
            {
                _toastService.ShowSuccess("Test finished successfully!");
                await JSRuntime.InvokeVoidAsync("history.back");
            }
        }

        private async Task CheckTest()
        {
            var answer = new ParamsForAnswer()
            {
                Parameters = _parameters,
                Answer = SpellingModel.AnswerForQuestion,
                Question = _paramsForTest.WordName
            };

            IsDisabledAnswers = true;
            IsShowCheck = false;
            _paramsCheck = await _spellingTestHttp.CheckQuestion(answer);
            _parameters = _paramsCheck.Parameters;
        }

    }
}
