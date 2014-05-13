
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
    /// 企业网站频道管理
    /// </summary>
    public class normal_channelDAO
    {
        WeiXinSystemDBDataContext db = new WeiXinSystemDBDataContext(SqlHelper.connectionString);
        #region Select

        /// <summary>
        /// 系统管理员查询所有的频道分类
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public PagerList<normal_channel> GetChannelByPager(int pageNo, int pageSize, string keyWords)
        {
            IQueryable<normal_channel> list = (from cr in db.normal_channel
                                               where  cr.Title.Contains(keyWords.Trim()) 
                                               select cr);
            int totalData = list.Count();
            return new PagerList<normal_channel>(list.OrderByDescending(o => o.Id), pageNo, pageSize, totalData);

        }

        /// <summary>
        /// 企业用户查询所有的频道分类
        /// </summary>
        /// <param name="eid">企业ID</param>
        /// <param name="pageNo">当前页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="keyWords">搜索词</param>
        /// <returns></returns>
        public PagerList<normal_channel> GetChannelByPager(int eid, int pageNo, int pageSize, string keyWords)
        {
            IQueryable<normal_channel> list = (from cr in db.normal_channel
                                               where cr.eid == eid && cr.Title.Contains(keyWords.Trim())
                                               select cr);
            int totalData = list.Count();
            return new PagerList<normal_channel>(list.OrderByDescending(o => o.Id), pageNo, pageSize, totalData);

        }

        /// <summary>
        /// 系统管理员查询所有的频道分类
        /// </summary> 
        /// <returns></returns>
        public List<normal_channel> GetChannelByList()
        {
            List<normal_channel> list = (from cr in db.normal_channel

                                         select cr).ToList();
            return list;
        }
        /// <summary>
        /// 企业用户查询所有的频道分类
        /// </summary>
        /// <param name="eid">企业ID</param> 
        /// <returns></returns>
        public List<normal_channel> GetChannelByList(int eid)
        {
            List<normal_channel> list = (from cr in db.normal_channel
                                         where cr.eid == eid
                                         select cr).ToList();
            return list;

        }
        /// <summary>
        /// 企业用户查询所有的频道分类
        /// </summary>
        /// <param name="eid">企业ID</param> 
        /// <returns></returns>
        public List<normal_channel> GetChannelByList(int eid,int count)
        {
            List<normal_channel> list = (from cr in db.normal_channel
                                         where cr.eid == eid
                                         select cr).Take(count).ToList();
            return list;

        }
        /// <summary>
        /// 根据用户的ID和频道ID查询频道信息
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public normal_channel GetChannelByidAndEid(int eid, int id)
        {
            return (from c in db.normal_channel where c.eid == eid && c.Id == id select c).FirstOrDefault();
        }

        public DataSet GetChannelBySql(string sql)
        {
            return SqlHelper.ExecuteDataset(CommandType.Text, sql);
            
        }
        #endregion

        #region Insert
        /// <summary>
        /// 添加频道
        /// </summary>
        /// <param name="Channel"></param>
        /// <returns>返回添加后的ID</returns>
        public int InsertChannel(normal_channel Channel)
        {
            db.normal_channel.InsertOnSubmit(Channel);
            db.SubmitChanges();
            return Channel.Id;
        }

        #endregion

        #region Update
        /// <summary>
        /// 修改频道信息
        /// </summary>
        /// <param name="channel"></param>
        /// <returns>受影响的行数</returns>
        public int UpdateChannel(normal_channel channel)
        {
            string sql = string.Format(@"update normal_channel set title='{0}',info='{1}',Itemname='{2}',itemunit='{3}',[Enabled]={4} where  id={5}", channel.Title, channel.Info, channel.ItemName, channel.ItemUnit, channel.Enabled, channel.Id);
            return SqlHelper.ExecuteNonQuery(System.Data.CommandType.Text, sql) ;
        }


        #endregion

        #region Delete
        /// <summary>
        /// 企业删除频道 
        /// </summary>
        /// <param name="id">频道ID</param>
        /// <param name="eid">企业ID</param>
        /// <returns></returns>
        public int DeleteChannel(int id, int eid)
        {
            string sql = "delete normal_channel where id=" + id + " and eid=" + eid;
            return SqlHelper.ExecuteNonQuery(System.Data.CommandType.Text, sql);
        }
        #endregion
    }
}
