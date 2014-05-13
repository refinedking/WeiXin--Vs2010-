
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
using System.Web.Security;
using WeiXin.BO;
using System.Data;
using WeiXin.BLL;

namespace WeiXin.Web.Controllers
{
    [WeiXinException]
    public class HomeController : Controller
    {
        private ModulesServices modulesServices = new ModulesServices();
        private ModuleTypeServices moduleTypeServices = new ModuleTypeServices();
       

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        
        public ActionResult Index()
        {
            //文件路径（虚拟路径）
            string filePath = "/FileXML/" + (Session["user"] as UsersContract).UserName + "_xml";
            UsersContract uc = Session["user"] as UsersContract;
            //获取当前登录人员的所有拥有权限的系统
            DataSet ds = moduleTypeServices.GetModuleTyepIdByUser(uc.UserID, uc.RoleID);
            //加载系统菜单
            ViewBag.sysMenu = moduleTypeServices.GetModuleTypeMenu(Server.MapPath(filePath), ds.Tables[0].Rows[0][0].ToString());
            //加载当前选中系统下的菜单
            ViewBag.menu = GetMenu(ds.Tables[0].Rows[0][0].ToString());
            return View();
        }

        public ActionResult HomePage()
        {
            return View();
        }

        /// <summary>
        /// 获得菜单
        /// </summary>
        /// <param name="id">系统编号</param>
        /// <returns></returns>
    
        public string GetMenu(string id)
        {
            //文件路径（虚拟路径）
            string filePath = "/FileXML/" + (Session["user"] as UsersContract).UserName + "_xml";
            //加载的系统
            int moduleTypeId;
            if (!int.TryParse(id, out moduleTypeId))
            {
                throw new Exception("数据转换失败,请联系管理员");
            };
            return "<ul id='ulMenu'><li href='" + Url.Action("HomePage") + "' menuid='0' class='liBegin'><div>首页</div> </li>" + modulesServices.GetMenu(Server.MapPath(filePath), moduleTypeId);
        }

        /// <summary>
        /// 登出，退回到登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Shared");
        }


    }
}
