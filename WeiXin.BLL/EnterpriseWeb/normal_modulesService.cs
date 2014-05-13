
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
using WeiXin.DAL;
using WeiXin.Model;
namespace WeiXin.BLL
{
    /// <summary>
    /// 文章模型
    /// </summary>
    public class normal_modulesService
    {
        normal_modulesDAO ModulesDao = new normal_modulesDAO();

        #region Select
        /// <summary>
        /// 查询所有的模型
        /// </summary>
        /// <returns></returns>
        public List<normal_modules> GetListModules()
        {
            return ModulesDao.GetListModules();
        }

        /// <summary>
        /// 根据ID查询的模型
        /// </summary>
        /// <returns></returns>
        public normal_modules GetListModules(int id)
        {
            return ModulesDao.GetListModules(id);
        }
        /// <summary>
        /// 根据ID查询的模型
        /// </summary>
        /// <returns></returns>
        public normal_modules GetListModules2(string type)
        {
            return ModulesDao.GetListModules2(type);
        }
        #endregion
    }
}
