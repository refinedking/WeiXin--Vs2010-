
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
using WeiXin.BO;
using WeiXin.Core.Transfer;

using WeiXin.Core;
namespace WeiXin.BLL
{
    public class normal_extendsService
    {
        normal_extendsDAO ExtendsDao = new normal_extendsDAO();
        #region Select
        /// <summary>
        /// 查询所有的插件
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public PagerList<normal_extends> GetExtendsByPager(int pageNo, int pageSize, string keyWords)
        {
            return ExtendsDao.GetExtendsByPager(pageNo, pageSize, keyWords);
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public normal_extendsContract GetExtendsById(int id)
        {
            return ExtendsDao.GetExtendsById(id).ToBO<normal_extendsContract>();
        }
        /// <summary>
        /// 根据ID查询Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public normal_extends GetNormal_ExtendsById(int id)
        {
            return ExtendsDao.GetExtendsById(id);
        }
        #endregion


        #region Insert
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="ne"></param>
        /// <returns>返回ID</returns>
        public int Insert(normal_extendsContract nec)
        {
            return ExtendsDao.Insert(nec.ToPO<normal_extends>());
        }
        #endregion

        #region Update
        public int Update(normal_extendsContract nec)
        {
            return ExtendsDao.Update(nec.ToPO<normal_extends>());
        }
        #endregion
        #region Delete
        public void Delete(int id)
        {
              ExtendsDao.Delete(id);
        }
        #endregion
    }
}
