
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
using System.Data;

namespace WeiXin.DAL
{
    /// <summary>
    /// 插件
    /// </summary>
    public class normal_extendsDAO
    {
        WeiXinSystemDBDataContext db = new WeiXinSystemDBDataContext(SqlHelper.connectionString);

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
            IQueryable<normal_extends> list = (from cr in db.normal_extends
                                               where  cr.Title.Contains(keyWords.Trim()) 
                                               ||cr.Name.Contains(keyWords.Trim())
                                               select cr);
            int totalData = list.Count();
            return new PagerList<normal_extends>(list.OrderByDescending(o => o.Id), pageNo, pageSize, totalData);

        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public normal_extends GetExtendsById(int id)
        { 
        return (from ne in db.normal_extends where ne.Id==id select ne).FirstOrDefault();
        }
        #endregion

        #region Insert
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="ne"></param>
        /// <returns>返回ID</returns>
        public int Insert(normal_extends ne)
        {
            db.normal_extends.InsertOnSubmit(ne);
            db.SubmitChanges();
            return ne.Id;
        }
        #endregion
        #region Update
        public int Update(normal_extends ne)
        {
            string sql = string.Format(@"update normal_extends set title='{0}',name='{1}',author='{2}',info='{3}',type={4},enabled={5},BaseTable='{7}' where id={6}", ne.Title, ne.Name, ne.Author, ne.Info, ne.Type, ne.Enabled, ne.Id,ne.BaseTable);
            return SqlHelper.ExecuteNonQuery(CommandType.Text ,sql);
        }
        #endregion

        #region Delete
        public void Delete(int id)
        {
            string sql = "delete normal_extends where id="+id;
            SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
        }
        #endregion
    }
}
