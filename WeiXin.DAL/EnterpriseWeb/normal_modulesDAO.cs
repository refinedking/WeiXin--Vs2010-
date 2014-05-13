
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
using System.Text;
using WeiXin.Model;
using WeiXin.Core.DBUtility;

namespace WeiXin.DAL
{
    /// <summary>
    /// 文章模型（文章、商品、等）
    /// </summary>
    public class normal_modulesDAO
    {
        WeiXinSystemDBDataContext db = new WeiXinSystemDBDataContext(SqlHelper.connectionString);

        #region Select
        /// <summary>
        /// 查询所有的模型
        /// </summary>
        /// <returns></returns>
        public List<normal_modules> GetListModules()
        { 
            return (from m in db.normal_modules select m ).ToList();
        }

        /// <summary>
        /// 根据ID查询的模型
        /// </summary>
        /// <returns></returns>
        public normal_modules GetListModules(int id)
        {
            return (from m in db.normal_modules where m.Id==id select m).FirstOrDefault();
        }

        /// <summary>
        /// 根据模型查询
        /// </summary>
        /// <returns></returns>
        public normal_modules GetListModules2(string type)
        {
            return (from m in db.normal_modules where m.Type == type select m).FirstOrDefault();
        }
        #endregion
    }
}
