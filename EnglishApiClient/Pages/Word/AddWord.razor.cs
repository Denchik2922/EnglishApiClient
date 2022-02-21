using Blazored.Toast.Services;
using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using EnglishApiClient.Infrastructure.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EnglishApiClient.Pages.Word
{
    public partial class AddWord
    {
        [Parameter]
        public int DictionaryId { get; set; }

        private string _newTranslate = "";
        private string _newExample = "";

        private WordModel _word = new WordModel();
        private ICollection<WordPhoto> pictures;

        [Inject]
        private IJSRuntime _jsRuntime { get; set; }

        [Inject]
        private IWordHttpService _wordService { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        private void AssignImageUrl(string imgUrl) => _word.PictureUrl = imgUrl;

        public void AddTranslate()
        {
            if(_newTranslate.Length > 1)
            {
                if(ListHelper.AddEntityInList(_word.Translates, _newTranslate))
                {
                    _newTranslate = "";
                }
            }
        }

        public void AddExample()
        {
            if (_newExample.Length > 1)
            {
                if (ListHelper.AddEntityInList<ExampleWord>(_word.WordExamples, _newExample))
                {
                    _newExample = "";
                }
            }
        }

        public void RemoveTranslate(string translate)
        {
            ListHelper.RemoveEntityFromList(_word.Translates, translate);
        }

        public void RemoveExample(string example)
        {
            ListHelper.RemoveEntityFromList(_word.WordExamples, example);
        }

        private async void GenerateWordInfo(string wordName)
        {
            if(wordName != null)
            {
                 WordInformation wordInfo = await _wordService.GenerateWordInformation(wordName);
                 _word.AudioUrl = wordInfo.AudioUrl;
                 _word.Translates = new List<TranslatedWord>() { new TranslatedWord() { Name = wordInfo.Translate } };
                 _word.WordExamples = wordInfo.WordExamples.Select(w => new ExampleWord() { Name = w }).ToList();
                 _word.Transcription = wordInfo.Transcription;
                 _word.PictureUrl = null;
                 pictures = wordInfo.PictureUrls;
                 StateHasChanged();
            }
        }

        private async Task CreateWord()
        {
            _word.EnglishDictionaryId = DictionaryId;
            var result = await _wordService.Create(_word);
            if (result)
            {
                _toastService.ShowSuccess($"Word with name {_word.Name} added successfully!");
                await _jsRuntime.InvokeVoidAsync("history.back");
            }
        }

        private void SelectPicture(string picture)
        {
            _word.PictureUrl = picture;
        }

        private async Task PlaySound()
        {
            await _jsRuntime.InvokeAsync<string>("PlayAudio", "sound");
        }
    }
}
