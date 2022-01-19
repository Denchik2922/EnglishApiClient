using Blazored.Toast.Services;
using EnglishApiClient.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Models;

namespace EnglishApiClient.Pages.Word
{
    public partial class AddWord
    {
        [Parameter]
        public int DictionaryId { get; set; }
        private string _newTranslate = "";
        private WordModel _word = new WordModel();
        private ICollection<WordPhoto> pictures = new List<WordPhoto>();

        [Inject]
        private IJSRuntime _jsRuntime { get; set; }

        [Inject]
        private IWordHttpService _wordService { get; set; }

        [Inject]
        private NavigationManager _navigation { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        public void AddTranslate()
        {
            if(_newTranslate.Length > 1)
            {
                var translatedWord = _word.Translates.FirstOrDefault(t => t.Name.ToLower() == _newTranslate.ToLower());
                if (translatedWord == null && !String.IsNullOrEmpty(_newTranslate))
                {
                    _word.Translates.Add(new TranslatedWord() { Name = _newTranslate });
                    _newTranslate = "";
                }
            }
        }

        public void RemoveTranslate(string translate)
        {
            var translatedWord = _word.Translates.FirstOrDefault(t => t.Name.ToLower() == translate.ToLower());
            if (translatedWord != null)
            {
                _word.Translates.RemoveAll(x => x.Name == translate);
            }
        }

        private async void GenerateWordInfo(string wordName)
        {
            if(wordName != null)
            {
                 WordInformation wordInfo = await _wordService.GenerateWordInformation(wordName);
                 _word.Audio = wordInfo.AudioUrl;
                 _word.Translates = new List<TranslatedWord>() { new TranslatedWord() { Name = wordInfo.Translate } };
                 _word.Transcription = wordInfo.Transcription;
                 _word.Picture = null;
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
                _navigation.NavigateTo("/");
            }
        }

        private void SelectPicture(string picture)
        {
            _word.Picture = picture;
        }

        private async Task PlaySound()
        {
            await _jsRuntime.InvokeAsync<string>("PlayAudio", "roar");
        }
    }
}
