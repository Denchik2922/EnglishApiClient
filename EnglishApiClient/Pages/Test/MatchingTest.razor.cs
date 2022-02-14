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
    public partial class MatchingTest
    {
        [Parameter]
        public int DictionaryId { get; set; }

        [Parameter]
        public int TypeId { get; set; }

        public bool IsShowCheck { get; set; }
        public bool IsDisabledAnswers 
        { 
            get { 
                return _paramsCheck != null;
            }
        }

        public string UserAnswer { get; set; }
        private TestParameters _parameters { get; set; } = new TestParameters();
        private ParamsForMatchingQuestion _paramsForTest { get; set; } = new ParamsForMatchingQuestion();
        private ParamsForCheck _paramsCheck { get; set; }

        [Inject]
        private IMatchingTestHttpService _matchingTestHttp { get; set; }

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

        private string StyleForAnswer(string answer)
        {   
            if (_paramsCheck != null && answer == UserAnswer)
            {
                if (_paramsCheck.IsTrueAnswer == true )
                {
                    return "background:lightgreen";
                }
                else
                {
                    return "background:red";
                }
            }
            else if (_paramsCheck != null && answer == _paramsCheck.TrueAnswer)
            {
                if (_paramsCheck.IsTrueAnswer == false)
                {
                    return "background:lightgreen";
                }
                return "";
            }
            else if (UserAnswer == answer)
            {
                return "background:lightblue";
            }
            else
            {
                return "";
            }

        }

        public void SetAnswer(string answer)
        {
            if(IsDisabledAnswers == false){
                UserAnswer = answer;
                IsShowCheck = true;
                StateHasChanged();
            }
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
            UserAnswer = "";
        }

        private async Task GetTest()
        {
            _paramsForTest = await _matchingTestHttp.GetPartOfTest(_parameters);
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

        private async Task CheckTest()
        {
            var answer = new ParamsForAnswer()
            {
                Parameters = _parameters,
                Answer = UserAnswer,
                Question= _paramsForTest.WordName
            };

            IsShowCheck = false;
            _paramsCheck = await _matchingTestHttp.CheckQuestion(answer);
            _parameters = _paramsCheck.Parameters;
        }
    }
}
