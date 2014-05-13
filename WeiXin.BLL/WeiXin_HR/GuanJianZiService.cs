
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
    /// effect:提供关键字业务逻辑访问方法
    /// </summary>
    public class GuanJianZiService
    {
        GuanJianZiDAL gjzDAL = new GuanJianZiDAL();

        #region Api
        /// <summary>
        /// 匹配关键字
        /// </summary>
        /// <param name="content"></param>
        /// <param name="eid"></param>
        /// <returns></returns>
        public GuanJianZi GetGuanjianzi(string content, int eid)
        {
       return     gjzDAL.GetGuanjianzi(content, eid);
        }
        #endregion

        /// <summary>
        /// 查询关键字列表
        /// </summary>
        /// <returns></returns>
        public List<GuanJianZi> GetGZJList()
        {
            return gjzDAL.GetGZJList();
        }

        /// <summary>
        /// 关键字信息分页方法
        /// </summary> 
        /// <param name="pageNo">当前页数</param>
        /// <param name="pageSize">显示数据条数</param>
        /// <param name="keyWords">查询条件</param>
        /// <returns>当前页数的数据</returns>
        public PagerList<GuanJianZi> GetGuanJianZIByPager(int pageNo, int pageSize, string keyWords)
        {
            return gjzDAL.GetGuanJianZIByPager(pageNo, pageSize, keyWords);
        } 
        /// <summary>
        /// 关键字信息分页方法2 查询企业自己的关键字
        /// </summary> 
        /// <param name="pageNo">当前页数</param>
        /// <param name="pageSize">显示数据条数</param>
        /// <param name="keyWords">查询条件</param>
        /// <returns>当前页数的数据</returns>
        public PagerList<GuanJianZi> GetGuanJianZIByPager(int eid,int pageNo, int pageSize, string keyWords)
        {
            return gjzDAL.GetGuanJianZIByPager(eid,pageNo, pageSize, keyWords);
        }

        /// <summary>
        /// 根据关键字编号查询该关键字所属企业
        /// </summary>
        /// <returns></returns>
        public employeeInfo GetEmployeeInfoByEid(int eId)
        {
            return gjzDAL.GetEmployeeInfoByEid(eId);
        }

        /// <summary>
        /// 根据关键字编号查询关键字信息
        /// </summary>
        /// <returns></returns>
        public GuanJianZiContract  GetGuanJianZiByGJZId(int gjzId)
        { 
            DataSet ds = gjzDAL.GetGuanJianZiByGJZId(gjzId); 
            return DataConvert.DataSetToEntity<GuanJianZiContract>(ds, 0);
        }


        /// <summary>
        /// 添加关键字
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        public int AddGuanJianZi(GuanJianZiContract pc)
        {
            GuanJianZi gjz = pc.ToPO<GuanJianZi>();
            return gjzDAL.AddGuanJianZi(gjz);
        }

        /// <summary>
        /// 修改关键字
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        public int UpdateGuanJianZi(GuanJianZiContract pc)
        {
            GuanJianZi gjz = pc.ToPO<GuanJianZi>();
            
            return gjzDAL.UpdateGuanJianZi(gjz);
        }

        /// <summary>
        /// 判断要添加的关键字是否存在
        /// </summary>
        /// <param name="name">关键字</param>
        /// <param name="eid">关键字所属企业id</param>
        /// <param name="gjzId">关键字id</param>
        /// <returns></returns>
        public bool IsExists(string name, string eid,string gjzId)
        {
            return gjzDAL.IsExists(name, eid, gjzId);
        }

        /// <summary>
        /// 删除关键字的方法
        /// </summary>
        /// <param name="gjzId">关键字编号</param>
        /// <returns></returns>
        public int DeleteGuanJianZi(int gjzId)
        {
            return gjzDAL.DeleteGuanJianZi(gjzId);
        }

        /// <summary>
        /// 获取所有的企业
        /// </summary>
        /// <returns></returns>
        public List<employeeInfo> GetAllEmp()
        {
            List<employeeInfo> gjzList = gjzDAL.GetAllEmp();
            return gjzList;
        }
        /// <summary>
        /// 获取所有的企业
        /// </summary>
        /// <returns></returns>
        public List<employeeInfo> GetAllEmp(int eid)
        {
            List<employeeInfo> gjzList = gjzDAL.GetAllEmp(eid);
            return gjzList;
        }

        /// <summary>
        /// 根据企业编号查询该企业下的关键字(有效关键字)
        /// </summary>
        /// <param name="eId">企业编号</param>
        /// <returns></returns>
        public List<GuanJianZiContract> GetGuanJianZiByEId(int eId)
        {
            DataSet ds = gjzDAL.GetGuanJianZiByEId(eId);
            return DataConvert.DataSetToIList<GuanJianZiContract>(ds, 0).ToList();
        }

        /// <summary>
        /// 判断要删除的关键字下是否存在此关键字的回复信息
        /// </summary>
        /// <param name="gjzId">关键字ID</param>
        /// <returns></returns>
        public int IsHaveGJZHFByGJZId(int gjzId)
        {
            return gjzDAL.IsHaveGJZHFByGJZId(gjzId);
        }

        /// <summary>
        /// 获取所有的关键字类型
        /// </summary>
        /// <returns></returns>
        public List<GuanJianZiType> GetAllGJZType()
        {
            List<GuanJianZiType> gjzList = gjzDAL.GetAllGJZType();
            return gjzList;
        }
    }
}
