using System.Web.Mvc;

namespace WeiXin.Web.Areas.MemberCard
{
    public class MemberCardAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MemberCard";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MemberCard_default",
                "MemberCard/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
