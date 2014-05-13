
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
using WeiXin.BLL;
using WeiXin.BO;
namespace WeiXin.Web.Controllers
{
    public class wapController : Controller
    {
        BranchService brBll = new BranchService();//分类
        EmployeeService empBll = new EmployeeService();//企业
        FansService FansBll = new FansService();//Fans
        employeeDataService empDataBll = new employeeDataService();//企业店铺信息
        /// <summary>
        /// 微网首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            string session = Request.QueryString["sessionid"];
            ViewData["sessionid"] = session;
            //不用Session 考虑操作过期
            // Session["sessionId"] = session;
            return View();
        }

        /// <summary>
        /// 微网 分类页
        /// </summary>
        /// <returns></returns>
        public ActionResult product_type()
        {
            string session = Request.QueryString["sessionid"];
            ViewData["sessionid"] = session;
            //查询分类 
            return View(brBll.GetBranchAllwap());
        }

        /// <summary>
        /// 微网 分类 =>企业页
        /// </summary>
        /// <param name="id">分类ID</param>
        /// <returns></returns>
        public ActionResult weixinInfos(int id)
        {
            string session = Request.QueryString["sessionid"];
            ViewData["sessionid"] = session;
            //查询该分类下的企业 
            return View(empBll.GetEmployeeInfoByBrIDwap(id));
        }

        /// <summary>
        /// 微网 分类=>企业页=> 企业地图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult map(int id)
        {
            string session = Request.QueryString["sessionid"];
            ViewData["sessionid"] = session;
            ViewData["empdata"] = empDataBll.GetEmpData(id);
            return View(empBll.GetEmployeeinfoByEId(id));
        
        }

        /// <summary>
        /// 微网 我的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult member()
        {
            string session = Request.QueryString["sessionid"];
            ViewData["sessionid"] = session;
            //查询会员的信息
            return View(FansBll.GetFansInfoByOpenID(session));
        }

        /// <summary>
        ///  微网 我的信息{修改}
        /// </summary>
        /// <param name="fc"></param>
        /// <param name="fans"></param>
        /// <returns></returns>
        [HttpPost] 
        public ActionResult member(FormCollection fc,FansContract fans)
        {
            string session = Request.QueryString["sessionid"];
            ViewData["sessionid"] = session;
            fans.FromUserName = session;
            //修改会员的姓名和生日
            try
            {
                FansBll.UpdateFansName(fans);
            }
            catch (Exception)
            { 
            }

            return View(fans);
        }

        /// <summary>
        /// 微网 关于我们
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            string session = Request.QueryString["sessionid"];
            ViewData["sessionid"] = session;
            return View();
        }

        /// <summary>
        /// 微网 TelUs
        /// </summary>
        /// <returns></returns>
        public ActionResult TelUs()
        {
            string session = Request.QueryString["sessionid"];
            ViewData["sessionid"] = session;
            return View();
        }

    }
}
