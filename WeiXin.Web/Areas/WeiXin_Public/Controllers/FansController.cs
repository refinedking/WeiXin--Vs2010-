
/*
 * 程序中文名称: 微联盟
 * 
 * 程序英文名称: WeiMeng
 * 
 * 程序版本: 1.0.X
 * 
 * 程序作者: 王兵 (商业合作请联系：refinedking@gmail.com)
 * 
 * 官方网站: http://weixin.cqzuxia.com/
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeiXin.Web.Controllers;

namespace WeiXin.Web.Areas.WeiXin_Public.Controllers
{
    /// <summary>
    /// 粉丝控制器， 粉丝的所有操作都在这里
    /// </summary>
    [WeiXinException]
    public class FansController : Controller
    {

        /// <summary>
        /// 我的粉丝
        /// </summary>
        /// <returns></returns>
        public ActionResult MyFans()
        {
            return View();
        }

    }
}
