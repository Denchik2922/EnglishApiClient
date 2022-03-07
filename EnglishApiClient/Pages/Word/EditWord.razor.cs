using Blazored.Toast.Services;
using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using EnglishApiClient.Infrastructure.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EnglishApiClient.Pages.Word
{
    public partial class EditWord
    {
        [Parameter]
        public int WordId { get; set; }
        private WordModel _word { get; set; }

        private string _newTranslate = "";
        private string _newExample = "";

        private ICollection<WordPhoto> pictures { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private IJSRuntime _jsRuntime { get; set; }

        [Inject]
        private IWordHttpService _wordService { get; set; }

        private void AssignImageUrl(string imgUrl) => _word.PictureUrl = imgUrl;

        protected override async Task OnInitializedAsync()
        {
            await GetWord();
            await GetWordPictures();
        }

        public void AddTranslate()
        {
            if (_newTranslate.Length > 1)
            {
                if (ListHelper.AddEntityInList(_word.Translates, _newTranslate))
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

        private void SelectPicture(string picture)
        {
            _word.PictureUrl = picture;
        }

        private async Task GetWord()
        {
            _word = await _wordService.GetById(WordId);
        }

        private async Task GetWordPictures()
        {
            try
            {
                pictures = await _wordService.GetWordPictures(_word.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                pictures = new List<WordPhoto>();
            }
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
                    await _jsRuntime.InvokeVoidAsync("history.back");
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
                await _jsRuntime.InvokeVoidAsync("history.back");
            }
        }
    }
}
