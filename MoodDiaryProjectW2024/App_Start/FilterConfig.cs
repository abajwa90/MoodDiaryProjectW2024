using System.Web;
using System.Web.Mvc;

namespace MoodDiaryProjectW2024
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
