
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
using WeiXin.Core;

namespace WeiXin.Web
{
    /// <summary>
    /// 公共方法控制器 提供公共的方法
    /// </summary>
    public class CommonController : Controller
    {

        #region 初始化分页数据变量
        public int pageSize = 10;
        public int pageIndex = 1;
        #endregion

        #region 公共方法

        /// <summary>
        /// 查询页面拥有的功能点权限
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public List<AuthorityContract> GetPageAuthority(string tag)
        {
            string filePath = Server.MapPath("/FileXML/" + (Session["user"] as UsersContract).UserName + "_xml");
            PublicBusinessMethodCore PBMC = new PublicBusinessMethodCore();
            return PBMC.GetPageAuthority<AuthorityContract>(filePath, tag);
        }

        #endregion

    }
}
