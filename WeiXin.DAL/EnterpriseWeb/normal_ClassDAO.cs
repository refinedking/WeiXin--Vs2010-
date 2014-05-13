
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
    /// 企业微网 栏目
    /// </summary>
    public class normal_ClassDAO
    {
        WeiXinSystemDBDataContext db = new WeiXinSystemDBDataContext(SqlHelper.connectionString);
        #region Select

        /// <summary>
        /// 查询企业 频道下的栏目
        /// </summary>
        /// <param name="ccid">频道ID</param>
        /// <param name="eid">企业ID</param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public PagerList<normal_class> GetClassByPager(int ccid, int eid, int pageNo, int pageSize, string keyWords)
        {
            IQueryable<normal_class> list = (from cr in db.normal_class
                                             where cr.ChannelId == ccid &&
                                             cr.normal_channel.eid == eid &&
                                             cr.Title.Contains(keyWords.Trim())
                                             select cr);
            int totalData = list.Count();
            return new PagerList<normal_class>(list.OrderByDescending(o => o.Id), pageNo, pageSize, totalData);

        }

        /// <summary>
        /// 根据频道ID和企业的Eid查询该频道下有那些栏目
        /// </summary>
        /// <param name="ccid">频道ID</param>
        /// <param name="eid">企业Eid</param>
        /// <returns></returns>
        public List<normal_class> GetClassByccidAndEid(int ccid, int eid)
        {
            List<normal_class> list = (from cr in db.normal_class
                                       where cr.ChannelId == ccid &&
                                       cr.normal_channel.eid == eid
                                       select cr).ToList();

            return list;
        }

        /// <summary>
        /// 根据频道ID和企业的Eid查询该频道下有那些栏目
        /// </summary>
        /// <param name="ccid">频道ID</param>
        /// <param name="eid">企业Eid</param>
        /// <param name="eid">提取数据条数</param>
        /// <returns></returns>
        public List<normal_class> GetClassByccidAndEid(int ccid, int eid,int count)
        {
            List<normal_class> list = (from cr in db.normal_class
                                       where cr.ChannelId == ccid &&
                                       cr.normal_channel.eid == eid
                                       select cr).Take(count).ToList();

            return list;
        }
        /// <summary>
        /// 根据Eid和栏目ID查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="eid"></param>
        /// <returns></returns>
        public normal_class GetClassByIdAndEid(int id, int eid)
        {
            return (from c in db.normal_class where c.Id == id && c.normal_channel.eid == eid select c).FirstOrDefault();
        }

        public normal_class GetClassModelByidAndCCid(int id, int ccid)
        {
            return (from c in db.normal_class where c.Id == id && c.ChannelId == ccid select c).FirstOrDefault(); 
        }

        /// <summary>
        /// 根据sql语句查询文章
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetClassBySql(string sql)
        {
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
            return dt;
        }
        /// <summary>
        /// 根据sql语句查询文章
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetClassBySql2(string sql)
        {
           return SqlHelper.ExecuteDataset(CommandType.Text, sql);
            
        }

        #endregion

        #region Insert
        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int InsertClass(normal_class c)
        {
            db.normal_class.InsertOnSubmit(c);
            db.SubmitChanges();
            return c.Id;
        }
        #endregion

        #region Update
        /// <summary>
        /// 修改栏目
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int UpdateClass(normal_class c)
        {
            string sql = string.Format(@"update normal_class set title='{0}',pagesize={1}, templateid={3}, contenttemp={4} where id={2}", c.Title, c.PageSize, c.Id,c.TemplateId,c.ContentTemp);
            return SqlHelper.ExecuteNonQuery(System.Data.CommandType.Text, sql);
        }
        #endregion

        #region Delete
        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>
        public int DeleteClass(int id, int eid)
        {
            string sql = string.Format(@"delete normal_class where id in ( select a.id from normal_class a,normal_channel b where a.ChannelId=b.Id and a.Id={0} and b.eid={1} )", id, eid);
            return SqlHelper.ExecuteNonQuery(System.Data.CommandType.Text, sql);

        }
        #endregion
    }
}
