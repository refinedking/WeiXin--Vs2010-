
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
    /// effect:提供部门业务逻辑访问方法
    /// </summary>
    public class BranchService
    {
        BranchDAL bDAL = new BranchDAL();

        /// <summary>
        /// 得到所有的部门信息的条数
        /// </summary>
        /// <param name="keyWords">关键字</param>
        /// <returns>返回所有部门信息的条数</returns>
        public int GetAllBranchCount(string keyWords)
        {
            return bDAL.GetAllBranchCount(keyWords);
        }

        /// <summary>
        /// 部门信息分页方法
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="pageNo">当前页数</param>
        /// <param name="pageSize">显示数据条数</param>
        /// <param name="keyWords">查询条件</param>
        /// <returns>当前页数的数据</returns>
        public List<BranchInfoContract> GetBranchByPager(string procName, int pageNo, int pageSize, string keyWords)
        {
            DataSet branchDS = bDAL.GetBranchByPager(procName, pageNo, pageSize, keyWords);
            return DataConvert.DataSetToIList<BranchInfoContract>(branchDS, 0).ToList();
        }

        /// <summary>
        /// 查询出所有的部门信息（只有部门名称与部门编号）
        /// </summary>
        /// <returns>部门信息（只有部门名称与部门编号）</returns>
        public List<BranchInfoContract> GetBranchNameAndBranchId()
        {
            DataSet ds = bDAL.GetBranchNameAndBranchId();
            return DataConvert.DataSetToIList<BranchInfoContract>(ds, 0).ToList();
        }

        /// <summary>
        /// 根据部门名称查询该部门是否存在的方法
        /// </summary>
        /// <param name="branchName">部门名称</param>
        /// <returns></returns>
        public int GetBranchByBranchName(string branchName)
        {
            return bDAL.GetBranchByBranchName(branchName);
        }

        /// <summary>
        /// 根据部门编号查询部门信息
        /// </summary>
        /// <param name="branchID">部门编号</param>
        /// <returns>部门信息</returns>
        public BranchInfoContract GetBranchByBranchID(int branchID)
        {
            DataSet ds = bDAL.GetBranchByBranchID(branchID);
            return DataConvert.DataSetToEntity<BranchInfoContract>(ds, 0);
        }

        /// <summary>
        /// 添加部门的方法
        /// </summary>
        /// <param name="branchInfo">部门实体</param>
        /// <returns>受影响的行数</returns>
        public int AddBranch(BranchInfoContract branchInfo)
        {
            branchInfo branch = branchInfo.ToPO<branchInfo>();
            return bDAL.AddBranch(branch);
        }

        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="branchInfo">部门信息实体</param>
        /// <returns>受影响的行数</returns>
        public int UpdateBranch(BranchInfoContract branchInfo)
        {
            branchInfo branch = branchInfo.ToPO<branchInfo>();
            return bDAL.UpdateBranch(branch);
        }

        /// <summary>
        /// 根据部门编号查询该部门下是否还有子部门的方法
        /// </summary>
        /// <param name="branchID">部门编号</param>
        /// <returns>返回的数据条数</returns>
        public int IsHaveMicroelectronicsByBranchID(int branchID)
        {
            return bDAL.IsHaveMicroelectronicsByBranchID(branchID);
        }

        /// <summary>
        /// 根据部门编号查询该部门下是否还有岗位信息的方法
        /// </summary>
        /// <param name="branchID">部门编号</param>
        /// <returns>返回的数据条数</returns>
        public int IsHavePositionByBranchID(int branchID)
        {
            return bDAL.IsHavePositionByBranchID(branchID);
        }

        /// <summary>
        /// 删除部门信息的方法
        /// </summary>
        /// <param name="branchID">部门编号</param>
        /// <returns>受影响的行数</returns>
        public int DeleteBranch(int branchID)
        {
            return bDAL.DeleteBranch(branchID);
        }

        /// <summary>
        /// 根据父部门编号查询部门名称
        /// </summary>
        /// <param name="pBranchID">父部门编号</param>
        /// <returns>部门名称</returns>
        public DataSet GetBranchNameByPBranchID(int pBranchID)
        {
            return bDAL.GetBranchNameByPBranchID(pBranchID);
        }

        /// <summary>
        /// 根据关键字查询部门信息的方法
        /// </summary>
        /// <param name="keyWords">关键字</param>
        /// <returns>部门信息</returns>
        public DataSet GetBranchByKeyWords(string keyWords)
        {
            return bDAL.GetBranchByKeyWords(keyWords);
        }

        /// <summary>
        /// 查询出所有的部门信息 
        /// </summary>
        /// <returns>部门信息List<BranchInfoContract></returns>
        public List<BranchInfoContract> GetAllBranch()
        {
            DataSet ds = bDAL.GetBranchAll();
            return DataConvert.DataSetToIList<BranchInfoContract>(ds, 0).ToList();
        }

        /// <summary>
        /// 根据父部门编号查询所有的子部门
        /// </summary>
        /// <param name="pBranchID">父部门编号</param>
        /// <returns>部门名称</returns>
        public DataSet GetBranchByPBranchID(int pBranchID)
        {
            DataSet ds = bDAL.GetBranchByPBranchID(pBranchID);
            return ds;
        }

        /// <summary>
        /// 检查部门名称是否重复
        /// </summary>
        /// <param name="branchName">部门名称</param>
        /// <param name="branchId">部门编号</param>
        /// <returns>true表示存在  false表示不存在</returns>
        public bool IsExists(string branchName, string branchId)
        {
            return bDAL.IsExists(branchName, branchId);
        }

        /***************************************************************/
        /// <summary>
        /// 查询出所有的部门信息 
        /// </summary>
        /// <returns>部门信息DataSet</returns>
        public DataSet GetBranchAll()
        {
            return bDAL.GetBranchAll();

        }


        /// <summary>
        /// 根据DID查询渠道信息
        /// </summary>
        /// <param name="code">Code</param>
        /// <param name="codeLen">Code长度</param>
        /// <returns></returns>
        public DataSet FindBranchByCode(string code, int codeLen)
        {
            return bDAL.FindBranchByCode(code, codeLen);
        }


        /// <summary>
        /// 查询父级渠道
        /// </summary>
        /// <returns></returns>
        public DataSet FindIsFBranch(int CodeLen)
        {
            return bDAL.FindIsFBranch(CodeLen);
        }

        /// <summary>
        /// 查询最新插入的一条数据
        /// </summary>

        /// <returns></returns>
        public DataSet GetNewBranch()
        {
            return bDAL.GetNewBranch();
        }

        #region WAP
        /// <summary>
        /// 查询所有的分类，不包含根分类（联盟分类）
        /// </summary>
        /// <returns></returns>
        public List<branchInfo> GetBranchAllwap()
        {
            return bDAL.GetBranchAllwap();
        }
        #endregion
    }
}
