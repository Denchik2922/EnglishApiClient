using EnglishApiClient.Dtos.Interfaces;

namespace EnglishApiClient.Infrastructure.Helpers
{
    public static class ListHelper
    {
        public static bool AddEntityInList<T>(List<T> list, string data) where T : class, IExtraWordInfo, new()
        {
            var exsistEntity = list.FirstOrDefault(i => i.Name.ToLower() == data.ToLower());
            if (exsistEntity == null && !String.IsNullOrEmpty(data))
            {
                list.Add(new T() { Name = data });
                return true;
            }
            return false;
        }

        public static void RemoveEntityFromList<T>(List<T> list, string data) where T : class, IExtraWordInfo, new()
        {
            var exsistEntity = list.FirstOrDefault(t => t.Name.ToLower() == data.ToLower());
            if (exsistEntity != null)
            {
                list.RemoveAll(x => x.Name == data);
            }
        }
    }
}
