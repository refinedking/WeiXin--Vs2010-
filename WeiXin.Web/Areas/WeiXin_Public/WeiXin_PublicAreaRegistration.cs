using System.Web.Mvc;

namespace WeiXin.Web.Areas.WeiXin_Public
{
    public class WeiXin_PublicAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "WeiXin_Public";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "WeiXin_Public_default",
                "WeiXin_Public/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
