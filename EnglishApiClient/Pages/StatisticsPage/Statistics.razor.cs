using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Reflection.Metadata;

namespace EnglishApiClient.Pages.StatisticsPage
{
    public partial class Statistics
    {
        [Parameter]
        public int DictionaryId { get; set; }

        public ICollection<TestResultForStatistic> results;

        public ChartOptions chartOptions = new ChartOptions();

        public string[] XAxisLabels;

        public List<ChartSeries> _TestResultData;

        public List<string> _typeOftesting;

        [Inject]
        private ITestResultHttpService _testResultService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetTestResult();
            GetTypeOftesting();
            if (_typeOftesting.Count > 0)
            {
                GetChartSeriesForTest(_typeOftesting.First());
            } 
        }

        public void GetTypeOftesting()
        {
            _typeOftesting = results.Select(r => r.Type.Name).Distinct().ToList();
        }

        public void GetChartSeriesForTest(string typeName)
        {
            var data = results.Where(res => res.Type.Name == typeName)
                              .Select(res => res.Score)
                              .ToArray();
            _TestResultData = new List<ChartSeries>
            {
                new ChartSeries
                {
                    Name = typeName,
                    Data = data
                }
            };

            XAxisLabels = results.Select(r => r.Date.ToShortDateString()).Distinct().ToArray();
        }

        public async Task GetTestResult()
        {
            results = await _testResultService.GetAllByDictionaryId(DictionaryId);
        }
    }
}
