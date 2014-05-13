using System.Web.Mvc;

namespace WeiXin.Web.Areas.WeiXin_Authority
{
    public class WeiXin_AuthorityAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "WeiXin_Authority";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "WeiXin_Authority_default",
                "WeiXin_Authority/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
