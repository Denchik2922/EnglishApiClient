namespace EnglishApiClient.Infrastructure.RequestFeatures
{
    public class PaginationParameters
    {
        const int maxPageSize = 20;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 7;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public SearchParameters SearchParameters { get; set; } = new SearchParameters();

        public string OrderBy { get; set; } = "name";

    }
}
