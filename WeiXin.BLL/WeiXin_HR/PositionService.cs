
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
    /// time:2012-2-16
    /// effect:提供职位业务逻辑访问方法
    /// </summary>
    public class PositionService
    {
        PositionDAL pDAL = new PositionDAL();

        #region Mr.Wang

        /// <summary>
        /// 查询 用户组列表
        /// </summary>
        /// <returns></returns>
        public List<positionInfo> GetPoList()
        {
            return pDAL.GetPoList();
        }


        #endregion
        /// <summary>
        /// 得到所有的职位信息的数据条数
        /// </summary>
        /// <param name="keyWords">关键字</param>
        /// <returns>返回所有职位信息的数据条数</returns>
        public int GetAllPositionCount(string keyWords)
        {
            return pDAL.GetAllPositionCount(keyWords);
        }


        /// <summary>
        /// 职位信息分页方法
        /// </summary> 
        /// <param name="pageNo">当前页数</param>
        /// <param name="pageSize">显示数据条数</param>
        /// <param name="keyWords">查询条件</param>
        /// <returns>当前页数的数据</returns>
        public PagerList<positionInfo> GetPositionByPager(int pageNo, int pageSize, string keyWords)
        {
            return pDAL.GetPositionByPager(pageNo, pageSize, keyWords);
        }



        /// <summary>
        /// 根据部门编号和职位名称查询该职位是否存在的方法
        /// </summary>
        /// <param name="branchId">部门编号</param>
        /// <param name="PositionName">职位名称</param>
        /// <returns>查询到的数据条数</returns>
        public int GetPositionByBranchIdAndPositionName(int branchId, string PositionName)
        {
            return pDAL.GetPositionByBranchIdAndPositionName(branchId, PositionName);
        }

        /// <summary>
        /// 添加职位的方法
        /// </summary>
        /// <param name="positionInfo">职位实体</param>
        /// <returns>受影响的行数</returns>
        public int AddPosition(PositionInfoContract positionInfo)
        {
            positionInfo position = positionInfo.ToPO<positionInfo>();
            return pDAL.AddPosition(position);
        }

        /// <summary>
        /// 修改职位信息
        /// </summary>
        /// <param name="positionInfo">职位信息实体</param>
        /// <returns>受影响的行数</returns>
        public int UpdatePosition(PositionInfoContract positionInfo)
        {
            positionInfo position = positionInfo.ToPO<positionInfo>();
            return pDAL.UpdatePosition(position);
        }

        /// <summary>
        /// 根据职位编号查询职位信息
        /// </summary>
        /// <param name="positionId">职位编号</param>
        /// <returns>职位信息</returns>
        public PositionInfoContract GetPositionByPositionID(int positionId)
        {
            DataSet ds = pDAL.GetPositionByPositionID(positionId);
            return DataConvert.DataSetToEntity<PositionInfoContract>(ds, 0);
        }

        /// <summary>
        /// 根据职位编号查询该职位下是否还有员工的方法
        /// </summary>
        /// <param name="positionID">职位编号</param>
        /// <returns>返回的数据条数</returns>
        public int IsHaveEmpByPositionID(int positionID)
        {
            return pDAL.IsHaveEmpByPositionID(positionID);
        }

        /// <summary>
        /// 删除职位的方法
        /// </summary>
        /// <param name="positionID">职位编号</param>
        /// <returns>受影响的行数</returns>
        public int DeletePosition(int positionID)
        {
            return pDAL.DeletePosition(positionID);
        }

        /// <summary>
        /// 根据部门编号查询该部门下的职位信息
        /// </summary>
        /// <param name="branchId">部门编号</param>
        /// <returns>职位信息</returns>
        public List<PositionInfoContract> GetPositionByBranchID(int branchId)
        {
            DataSet ds = pDAL.GetPositionByBranchID(branchId);
            return DataConvert.DataSetToIList<PositionInfoContract>(ds, 0).ToList();
        }

        /// <summary>
        /// 根据部门编号查询该部门下的职位信息
        /// </summary>
        /// <param name="branchId">部门编号</param>
        /// <returns>职位信息</returns>
        public List<PositionInfoContract> GetPositionDSByBranchID(int branchId)
        {
            DataSet ds = pDAL.GetPositionByBranchID(branchId);
            return DataConvert.DataSetToIList<PositionInfoContract>(ds, 0).ToList();
        }

        /// <summary>
        /// 根据关键字查询职位名称（职位autoComplete时使用）
        /// </summary>
        /// <param name="keyWords">关键字</param>
        /// <returns></returns>
        public DataSet GetPositionNameByKeyWords(string keyWords)
        {
            return pDAL.GetPositionNameByKeyWords(keyWords);
        }

        /// <summary>
        /// 根据关键字查询部门名称（职位autoComplete时使用）
        /// </summary>
        /// <param name="keyWords">关键字</param>
        /// <returns></returns>
        public DataSet GetBranchNameByKeyWords(string keyWords)
        {
            return pDAL.GetBranchNameByKeyWords(keyWords);
        }

        /// <summary>
        /// 检查职位名称是否存在
        /// </summary>
        /// <param name="positionName">职位名称</param>
        /// <param name="positionId">职位编号</param> 
        /// <returns>true表示存在  false表示不存在</returns>
        public bool IsExists(string positionName, string positionId)
        {
            return pDAL.IsExists(positionName, positionId);
        }

        /***************************************************************/


        /// <summary>
        /// 根据部门编号查询该部门下的职位信息
        /// </summary>
        /// <param name="branchId">部门编号</param>
        /// <returns>职位信息</returns>
        public DataSet GetPositionByID(int branchId)
        {
            DataSet ds = pDAL.GetPositionByID(branchId);
            return ds;
        }


        /// <summary>
        /// 根据职位编号查询职位，部门信息
        /// </summary>
        /// <param name="positionId">职位编号</param>
        /// <returns>职位信息</returns>
        public DataSet GetPositionByPID(int positionId)
        {
            return pDAL.GetPositionByID(positionId);
        }

        /// <summary>
        /// 查询非自己的岗位
        /// </summary>
        /// <param name="branchId">部门编号</param>
        /// <param name="branchId">岗位编号</param>
        /// <returns>职位信息</returns>
        public DataSet GetPositionByBIDPid(int branchId, int positionId)
        {
            return pDAL.GetPositionByBIDPid(branchId, positionId);

        }

        /// <summary>
        /// 根据职位编号查询职位，部门信息
        /// </summary>
        /// <param name="positionId">职位编号</param>
        /// <returns>职位信息</returns>
        public DataSet GetPositionByBrID(int branchId)
        {
            return pDAL.GetPositionByBrID(branchId);
        }
    }
}
