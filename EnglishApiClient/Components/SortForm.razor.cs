using Microsoft.AspNetCore.Components;

namespace EnglishApiClient.Components
{
    public partial class SortForm
    {
        [Parameter]
        public EventCallback<string> OnSortChanged { get; set; }

        [Parameter]
        public Dictionary<string, string> SortTypes { get; set; }

        private async Task ApplySort(ChangeEventArgs eventArgs)
        {
            await OnSortChanged.InvokeAsync(eventArgs.Value.ToString());
        }
    }
}
