
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
using System.Data;
using System.Data.SqlClient;
using WeiXin.Model;
using WeiXin.DAL;
using WeiXin.BO;
using WeiXin.Core;
using WeiXin.Core.Transfer;

namespace WeiXin.BLL
{
    /// <summary>
    /// author:刘强
    /// time:2013-6-20
    /// effect:提供关键字回复业务逻辑访问方法
    /// </summary>
    public class GuanJianZiHuiFuService
    {
        GuanJianZiHuiFuDAL gjzhfDAL = new GuanJianZiHuiFuDAL();
        #region Api
        /// <summary>
        /// 根据关键字回复，暂定 回复最新的一条，稍后更改
        /// </summary>
        /// <param name="gjz"></param>
        /// <param name="empid"></param>
        /// <returns></returns>
        public List<GuanJianZiHuiFu> HuiFu(string gjz, int empid)
        {

            return gjzhfDAL.HuiFu(gjz, empid);
        }

        /// <summary>
        /// 更新访问次数
        /// </summary>
        /// <param name="id"></param>
        public void ArticleRead(int id)
        {
            gjzhfDAL.ArticleRead(id);
        }
        #endregion
        /// <summary>
        /// 查询关键字回复列表
        /// </summary>
        /// <returns></returns>
        public List<GuanJianZiHuiFu> GetGZJHFList()
        {
            return gjzhfDAL.GetGZJHFList();
        }

        /// <summary>
        /// 关键字回复信息分页方法
        /// </summary> 
        /// <param name="pageNo">当前页数</param>
        /// <param name="pageSize">显示数据条数</param>
        /// <param name="keyWords">查询条件</param>
        /// <returns>当前页数的数据</returns>
        public PagerList<GuanJianZiHuiFu> GetGuanJianZIHuiFuByPager(int pageNo, int pageSize, string keyWords)
        {
            return gjzhfDAL.GetGuanJianZIHuiFuByPager(pageNo, pageSize, keyWords);
        }
        /// <summary>
        /// 关键字回复信息分页方法2 查询企业的关键字回复信息
        /// </summary> 
        /// <param name="pageNo">当前页数</param>
        /// <param name="pageSize">显示数据条数</param>
        /// <param name="keyWords">查询条件</param>
        /// <returns>当前页数的数据</returns>
        public PagerList<GuanJianZiHuiFu> GetGuanJianZIHuiFuByPager(int eid,int pageNo, int pageSize, string keyWords)
        {
            return gjzhfDAL.GetGuanJianZIHuiFuByPager(eid,pageNo, pageSize, keyWords);
        }

        /// <summary>
        /// 获取所有的关键字回复类型
        /// </summary>
        /// <returns></returns>
        public List<GuanJianZiHuiFuType> GetAllGJZType()
        {
            List<GuanJianZiHuiFuType> gjzList = gjzhfDAL.GetAllGJZType();
            return gjzList;
        }

        /// <summary>
        /// 根据回复类型编号查询回复类型信息
        /// </summary>
        /// <param name="typeId">回复类型编号</param>
        /// <returns></returns>
        public GuanJianZiHuiFuType GetAllGJZTypeByTypeId(string typeId)
        {
            return gjzhfDAL.GetAllGJZTypeByTypeId(typeId);
        }

        /// <summary>
        /// 根据回复编号查询关键字回复信息
        /// </summary>
        /// <returns>GuanJianZiHuiFuContract</returns>
        public GuanJianZiHuiFuContract GetGuanJianZiHuiFuByHFId(int hfId)
        {
            DataSet ds = gjzhfDAL.GetGuanJianZiHuiFuByHFId(hfId);
            return DataConvert.DataSetToEntity<GuanJianZiHuiFuContract>(ds, 0);
        }


        /// <summary>
        /// 判断要添加的关键字回复内容是否存在
        /// </summary>
        /// <param name="name">关键字内容</param>
        /// <param name="eid">关键字内容所属关键字</param>
        /// <param name="hfId">关键字内容Id</param>
        /// <returns></returns>
        public bool IsExists(string name, string gjzId, string hfId)
        {
            return gjzhfDAL.IsExists(name, gjzId, hfId);
        }

        /// <summary>
        /// 添加关键字回复内容
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        public int AddGuanJianZiHuiFu(GuanJianZiHuiFuContract pc)
        {
            GuanJianZiHuiFu gjz = pc.ToPO<GuanJianZiHuiFu>();
            return gjzhfDAL.AddGuanJianZiHuiFu(gjz);
        }


        /// <summary>
        /// 修改关键字回复内容
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        public int UpdateGuanJianZiHuiFu(GuanJianZiHuiFuContract pc)
        {
            GuanJianZiHuiFu gjz = pc.ToPO<GuanJianZiHuiFu>();
            return gjzhfDAL.UpdateGuanJianZiHuiFu(gjz);
        }

        /// <summary>
        /// 删除关键字回复信息
        /// </summary>
        /// <param name="hfId">关键字回复Id</param>
        /// <returns></returns>
        public int DeleteGuanJianZiHuiFu(int hfId)
        {
            return gjzhfDAL.DeleteGuanJianZiHuiFu(hfId);
        }

        /// <summary>
        /// 根据回复ID查询关键字回复信息
        /// </summary>
        /// <param name="hfId">回复ID</param>
        /// <returns>GuanJianZiHuiFu</returns>
        public GuanJianZiHuiFu GetGuanJianZiHuiFuEntityByHFId(int hfId)
        {
            DataSet ds = gjzhfDAL.GetGuanJianZiHuiFuByHFId(hfId);
            GuanJianZiHuiFuContract ggg = DataConvert.DataSetToEntity<GuanJianZiHuiFuContract>(ds, 0);
            GuanJianZiHuiFu gjz = ggg.ToPO<GuanJianZiHuiFu>();
            return gjz;
        }
    }
}
