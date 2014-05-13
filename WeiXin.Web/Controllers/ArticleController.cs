
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
using WeiXin.BO;
using WeiXin.BLL;
namespace WeiXin.Web.Controllers
{
    public class ArticleController : Controller
    {
        FansService fs = new FansService();
        GuanJianZiHuiFuService HFBll = new GuanJianZiHuiFuService();//关键字回复
        #region 文章页面
        public ActionResult Article(int id, string wxid)
        { 
            GuanJianZiHuiFuContract gjz = HFBll.GetGuanJianZiHuiFuByHFId(id);
            //更新统计
            HFBll.ArticleRead(id); 
            return View(gjz);
        }
        #endregion

    }
}
