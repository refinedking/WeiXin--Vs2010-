
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
using System.Data;
using WeiXin.Core;
namespace WeiXin.BLL
{
    /// <summary>
    /// 企业网站频道管理
    /// </summary>
    public class normal_channelService
    {
        normal_channelDAO ChannelDao = new normal_channelDAO();

        #region Insert
        /// <summary>
        /// 添加频道
        /// </summary>
        /// <param name="Channel"></param>
        /// <returns>返回添加后的ID</returns>
        public int InsertChannel(normal_channelContract Channel)
        {
            return ChannelDao.InsertChannel(Channel.ToPO<normal_channel>());
        }

        #endregion

        #region Update
        /// <summary>
        /// 修改频道信息
        /// </summary>
        /// <param name="channel"></param>
        /// <returns>受影响的行数</returns>
        public int UpdateChannel(normal_channelContract channel)
        {
            return ChannelDao.UpdateChannel(channel.ToPO<normal_channel>());
        }
        #endregion

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

            return ChannelDao.GetChannelByPager(pageNo, pageSize, keyWords);

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
            return ChannelDao.GetChannelByPager(eid, pageNo, pageSize, keyWords);
        }

        /// <summary>
        /// 系统管理员查询所有的频道分类
        /// </summary>
        /// <returns></returns>
        public List<normal_channel> GetChannelByList()
        {
            return ChannelDao.GetChannelByList();
        }
        /// <summary>
        /// 企业用户查询所有的频道分类
        /// </summary>
        /// <param name="eid">企业ID</param> 
        /// <returns></returns>
        public List<normal_channel> GetChannelByList(int eid)
        {
            return ChannelDao.GetChannelByList(eid);

        }
        /// <summary>
        /// 企业用户查询所有的频道分类
        /// </summary>
        /// <param name="eid">企业ID</param> 
        /// <returns></returns>
        public List<normal_channel> GetChannelByList(int eid, int count)
        {
            return ChannelDao.GetChannelByList(eid, count);


        }
        /// <summary>
        /// 根据用户的ID和频道ID查询频道信息
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public normal_channel GetChannelByidAndEid(int eid, int id)
        {
            return ChannelDao.GetChannelByidAndEid(eid, id);
        }
        public DataSet GetChannelBySql(string sql)
        {
            return ChannelDao.GetChannelBySql(sql);

        }

        public List<normal_channel> ListChannelDT2List(string sql)
        {
            return DataConvert.DataSetToIList<normal_channel>(ChannelDao.GetChannelBySql(sql), 0).ToList();
           
        }


        public void ExecuteTags(ref string PageStr, normal_channel _Channel)
        {
            PageStr = PageStr.Replace("{$ChannelId}", _Channel.Id.ToString());
            PageStr = PageStr.Replace("{$ChannelName}", _Channel.Title);
            PageStr = PageStr.Replace("{$ChannelInfo}", _Channel.Info);
            PageStr = PageStr.Replace("{$ChannelType}", _Channel.Type);
            PageStr = PageStr.Replace("{$ChannelDir}", _Channel.Dir);
            PageStr = PageStr.Replace("{$ChannelItemName}", _Channel.ItemName);
            PageStr = PageStr.Replace("{$ChannelItemUnit}", _Channel.ItemUnit);
            PageStr = PageStr.Replace("{$ChannelLink}", "/Enterprise/web/" + _Channel.Type + "classList?ccid=" + _Channel.Id + "&sessionid={$Sessionid}");
        }
        /// <summary>
        /// 根据用户的ID和频道ID查询频道信息
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public normal_channelContract GetChannelContractByidAndEid(int eid, int id)
        {
            return ChannelDao.GetChannelByidAndEid(eid, id).ToBO<normal_channelContract>();
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
            return ChannelDao.DeleteChannel(id, eid);
        }
        #endregion
    }
}
