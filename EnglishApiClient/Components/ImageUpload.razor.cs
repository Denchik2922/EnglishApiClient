using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using Tewr.Blazor.FileReader;

namespace EnglishApiClient.Components
{
    public partial class ImageUpload
    {
        private ElementReference _input;

        private bool IsClearInputFile { get; set; }

        [Parameter]
        public string ImgUrl { get; set; }

        [Parameter]
        public EventCallback<string> OnChange { get; set; }

        [Inject]
        public IFileReaderService FileReaderService { get; set; }

        [Inject]
        public IWordHttpService WordService { get; set; }

        private async Task Upload()
        {
            foreach (var file in await FileReaderService.CreateReference(_input).EnumerateFilesAsync())
            {
                if (file != null)
                {
                    var fileInfo = await file.ReadFileInfoAsync();
                    using (var ms = await file.CreateMemoryStreamAsync(4 * 1024))
                    {
                        var content = new MultipartFormDataContent();
                        content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                        content.Add(new StreamContent(ms, Convert.ToInt32(ms.Length)), "image", fileInfo.Name);
                        ImgUrl = await WordService.UploadWordImage(content);
                        await OnChange.InvokeAsync(ImgUrl);
                        ClearInputFile();
                    }
                }
            }
        }
        private void ClearInputFile()
        {
            IsClearInputFile = true;
            StateHasChanged();
            IsClearInputFile = false;
            StateHasChanged();
        }

    }
}
