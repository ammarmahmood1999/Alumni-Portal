using System.Web;
using System.Web.Mvc;

namespace FAST_Alumni_2020_Web_Portal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
