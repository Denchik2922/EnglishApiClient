using Blazored.Toast.Services;
using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EnglishApiClient.Pages.Word
{
    public partial class EditWord
    {
        [Parameter]
        public int WordId { get; set; }
        private WordModel? _word { get; set; }

        private string _newTranslate = "";
        private ICollection<WordPhoto> pictures = new List<WordPhoto>();

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private IJSRuntime _jsRuntime { get; set; }

        [Inject]
        private NavigationManager _navigation { get; set; }

        [Inject]
        private IWordHttpService _wordService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetWord();
            await GetWordPictures();
        }

        public void AddTranslate()
        {
            if (_newTranslate.Length > 1)
            {
                var translatedWord = _word.Translates.FirstOrDefault(t => t.Name.ToLower() == _newTranslate.ToLower());
                if (translatedWord == null && !String.IsNullOrEmpty(_newTranslate))
                {
                    _word.Translates.Add(new TranslatedWord() { Name = _newTranslate });
                    _newTranslate = "";
                }
            }
        }

        private void SelectPicture(string picture)
        {
            _word.PictureUrl = picture;
        }

        public void RemoveTranslate(string translate)
        {
            var translatedWord = _word.Translates.FirstOrDefault(t => t.Name.ToLower() == translate.ToLower());
            if (translatedWord != null)
            {
                _word.Translates.RemoveAll(x => x.Name == translate);
            }
        }

        private async Task GetWord()
        {
            _word = await _wordService.GetById(WordId);
        }

        private async Task GetWordPictures()
        {
            pictures = await _wordService.GetWordPictures(_word.Name);
        }

        private async Task PlaySound()
        {
            await _jsRuntime.InvokeAsync<string>("PlayAudio", "sound");
        }

        private async Task DeleteWord()
        {
            var confirmed = await _jsRuntime.InvokeAsync<bool>("confirm",
                                                                $"Are you sure you want to delete word with name {_word.Name}?");
            if (confirmed)
            {
                bool result = await _wordService.Delete(WordId);
                if (result)
                {
                    _toastService.ShowSuccess($"Word with name {_word.Name} deleted successfully!");
                    _navigation.NavigateTo("/");
                }
                else
                {
                    _toastService.ShowError("Word deleted failed!");
                }
            }
        }

        private async Task UpdateWord()
        {
            var result = await _wordService.Update(_word);
            if (result)
            {
                _toastService.ShowSuccess("Word edited successfully!");
                _navigation.NavigateTo("/");
            }
        }
    }
}
