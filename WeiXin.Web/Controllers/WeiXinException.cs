
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
    /// <summary>
    /// 异常处理类，用于处理发生异常的Action，当发生异常后，统一跳到中间页，并显示相应提示信息
    /// </summary>
    public class WeiXinException : FilterAttribute, IExceptionFilter, IActionFilter
    {
        #region IExceptionFilter 成员

        /// <summary>
        /// 异常处理方法，当“监视”的Action发生异常后会由本方法截取并处理
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            //取出出错的页面地址
            //filterContext.Controller.ViewData.Add(new KeyValuePair<string, object>("controllerName", filterContext.RouteData.Values["controller"].ToString()));
            //filterContext.Controller.ViewData.Add(new KeyValuePair<string, object>("actionName", filterContext.RouteData.Values["action"].ToString()));
            //判断是否为服务器验证异常
            if (ex.GetType().Name == "InvalidOperationException")
            {
                filterContext.Controller.ViewData.Add(new KeyValuePair<string, object>("msg", "您输入的数据有误，请核对"));
            }
            else if (ex.GetType().Name == "NullReferenceException")
            {
                filterContext.Controller.ViewData.Add(new KeyValuePair<string, object>("msg", "获取数据失败!请重试!"));
            }
            else if (ex.GetType().Name == "SqlException")
            {
                //判断是否数据库服务器异常
                filterContext.Controller.ViewData.Add(new KeyValuePair<string, object>("msg", "数据库错误或你暂时不能这样操作!"));
            }
            else
            {
                filterContext.Controller.ViewData["msg"] = string.IsNullOrEmpty(ex.Message) ? "系统未知错误" : ex.Message;
            }
            //改变要显示的视图
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = filterContext.Controller.ViewData
            };
            //标记异常已经被处理
            filterContext.ExceptionHandled = true;
        }

        #endregion

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData["backUrlOld"] = filterContext.HttpContext.Request.QueryString["backUrl"];
            filterContext.Controller.ViewData["backUrlNow"] = filterContext.HttpContext.Request.Url;
            if (filterContext.HttpContext.Session["user"] == null)
            {
                filterContext.Controller.ViewData["msg"] = "由于您长时间未操作，因安全因素请重新登录";
                //对获得菜单超时异常进行特殊处理
                if (!filterContext.RouteData.Values["action"].ToString().Equals("GetMenu"))
                {
                    filterContext.Controller.ViewData["backUrl"] = filterContext.HttpContext.Request.Url;
                }
                else
                {
                    filterContext.Controller.ViewData["backUrl"] = null;
                }
                if (filterContext.RouteData.DataTokens.Keys.Contains("area"))
                {
                    filterContext.Controller.ViewData.Add(new KeyValuePair<string, object>("url", new UrlHelper(filterContext.RequestContext).Action("Login", "../Shared", new { Areas = "" })));
                }
                else
                {
                    filterContext.Controller.ViewData.Add(new KeyValuePair<string, object>("url", new UrlHelper(filterContext.RequestContext).Action("Login", "Shared", new { Areas = "" })));
                }

                //改变要显示的视图
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error",
                    ViewData = filterContext.Controller.ViewData
                };
            }
           
        }
    }
}