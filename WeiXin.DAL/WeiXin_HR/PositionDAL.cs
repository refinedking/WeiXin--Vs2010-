
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
using WeiXin.Core.DBUtility;
using WeiXin.DAL;

namespace WeiXin.DAL
{

    /// <summary>
    /// author:刘强
    /// time:2012-2-16
    /// effect:提供职位数据访问方法
    /// </summary>
    public class PositionDAL
    {
        WeiXinSystemDBDataContext db = new WeiXinSystemDBDataContext(SqlHelper.connectionString);
        #region Mr.Wang

        /// <summary>
        /// 查询 用户组列表
        /// </summary>
        /// <returns></returns>
        public List<positionInfo> GetPoList()
        {
            return (from p in db.positionInfo where p.isDisplay==0 select p).OrderByDescending(p => p.positionId).ToList();
        }
 

        #endregion

        /// <summary>
        /// 得到所有的职位信息的数据条数
        /// </summary>
        /// <param name="keyWords">关键字</param>
        /// <returns>返回所有职位信息的数据条数</returns>
        public int GetAllPositionCount(string keyWords)
        {
            SqlParameter[] dataparam = new SqlParameter[] {
                new SqlParameter("@keyWords", SqlDbType.VarChar,50) };
            dataparam[0].Value = keyWords;
            return Convert.ToInt32(SqlServerHelper.RunProc("proc_GetAllPositionCount", dataparam));
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

            IQueryable<positionInfo> list = (from cr in db.positionInfo
                                             where (cr.positionName.Contains(keyWords.Trim())
                                           )
                                             select cr);
            int totalData = list.Count();
            return new PagerList<positionInfo>(list.OrderByDescending(o => o.positionId), pageNo, pageSize, totalData);
        }


        /// <summary>
        /// 根据部门编号和职位名称查询该职位是否存在的方法
        /// </summary>
        /// <param name="branchId">部门编号</param>
        /// <param name="PositionName">职位名称</param>
        /// <returns>查询到的数据条数</returns>
        public int GetPositionByBranchIdAndPositionName(int branchId, string PositionName)
        {
            SqlParameter[] dataparam = new SqlParameter[] { 
                new SqlParameter("@branchID", SqlDbType.Int),
                new SqlParameter("@positionName", SqlDbType.VarChar, 50) };
            dataparam[0].Value = branchId;
            dataparam[1].Value = PositionName;
            return SqlServerHelper.RunProcedure("proc_GetPositionByPositionName", dataparam, "pos").Tables[0].Rows.Count;
        }

        /// <summary>
        /// 添加职位的方法
        /// </summary>
        /// <param name="positionInfo">职位实体</param>
        /// <returns>受影响的行数</returns>
        public int AddPosition(positionInfo positionInfo)
        {
            string sql = @"insert into positionInfo values('" + positionInfo.positionName + "',1,'" + positionInfo.positionRemark + "')";
            return SqlServerHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 修改职位信息
        /// </summary>
        /// <param name="positionInfo">职位信息实体</param>
        /// <returns>受影响的行数</returns>
        public int UpdatePosition(positionInfo positionInfo)
        {
            string sql = @"update positionInfo set positionName='" + positionInfo.positionName + "',positionRemark='" + positionInfo.positionRemark + "' where positionId=" + positionInfo.positionId;
            return SqlServerHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 根据职位编号查询职位信息
        /// </summary>
        /// <param name="positionId">职位编号</param>
        /// <returns>职位信息</returns>
        public DataSet GetPositionByPositionID(int positionId)
        {
            SqlParameter[] dataparam = new SqlParameter[] { new SqlParameter("@positionId", SqlDbType.Int) };
            dataparam[0].Value = positionId;
            return SqlServerHelper.RunProcedure("proc_GetPositionByPositionID", dataparam, "pos");
        }

        /// <summary>
        /// 根据职位编号查询该职位下是否还有员工的方法
        /// </summary>
        /// <param name="positionID">职位编号</param>
        /// <returns>返回的数据条数</returns>
        public int IsHaveEmpByPositionID(int positionID)
        {
            SqlParameter[] dataparam = new SqlParameter[] { new SqlParameter("@positionId", SqlDbType.Int) };
            dataparam[0].Value = positionID;
            return SqlServerHelper.RunProcedure("proc_IsHaveEmpByPositionId", dataparam, "pos").Tables[0].Rows.Count;
        }

        /// <summary>
        /// 删除职位的方法
        /// </summary>
        /// <param name="positionID">职位编号</param>
        /// <returns>受影响的行数</returns>
        public int DeletePosition(int positionID)
        {
            string sql = @"delete from positionInfo where positionId=" + positionID;
            return SqlServerHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 根据部门编号查询该部门下的职位信息
        /// </summary>
        /// <param name="branchId">部门编号</param>
        /// <returns>职位信息</returns>
        public DataSet GetPositionByBranchID(int branchId)
        {
            SqlParameter[] dataparam = new SqlParameter[] { new SqlParameter("@branchId", SqlDbType.Int) };
            dataparam[0].Value = branchId;
            return SqlServerHelper.RunProcedure("proc_GetPositionByBranchID", dataparam, "pos");
        }

        /// <summary>
        /// 根据关键字查询职位名称（职位autoComplete时使用）
        /// </summary>
        /// <param name="keyWords">关键字</param>
        /// <returns></returns>
        public DataSet GetPositionNameByKeyWords(string keyWords)
        {
            SqlParameter[] dataparam = new SqlParameter[] { new SqlParameter("@keyWords", SqlDbType.VarChar, 50) };
            dataparam[0].Value = keyWords;
            return SqlServerHelper.RunProcedure("proc_GetPositionNameByKeyWords", dataparam, "pos");
        }

        /// <summary>
        /// 根据关键字查询部门名称（职位autoComplete时使用）
        /// </summary>
        /// <param name="keyWords">关键字</param>
        /// <returns></returns>
        public DataSet GetBranchNameByKeyWords(string keyWords)
        {
            SqlParameter[] dataparam = new SqlParameter[] { new SqlParameter("@keyWords", SqlDbType.VarChar, 50) };
            dataparam[0].Value = keyWords;
            return SqlServerHelper.RunProcedure("proc_GetBranchNameByKeyWords", dataparam, "pos");
        }

        /// <summary>
        /// 检查职位名称是否存在
        /// </summary>
        /// <param name="positionName">职位名称</param>
        /// <param name="positionId">职位编号</param> 
        /// <returns>true表示存在  false表示不存在</returns>
        public bool IsExists(string positionName, string positionId)
        {
            IQueryable<positionInfo> p = (from po in db.positionInfo
                                          where
                                              po.positionName.Contains(positionName)
                                          select po);

            try
            {

                return p.Count() > 0 ? false : true;
            }
            catch
            {
                return false;
            }
        }


        /***************************************************************/

        /// <summary>
        /// 查询非自己的岗位
        /// </summary>
        /// <param name="branchId">部门编号</param>
        /// <param name="positionId">岗位编号</param>
        /// <returns>职位信息</returns>
        public DataSet GetPositionByBIDPid(int branchId, int positionId)
        {
            SqlParameter[] dataparam = new SqlParameter[] { 
                new SqlParameter("@branchid", SqlDbType.Int),
                new SqlParameter("@positionId", SqlDbType.Int) };
            dataparam[0].Value = branchId;
            dataparam[1].Value = positionId;
            return SqlServerHelper.RunProcedure("proc_GetPositionByBIDPid", dataparam, "pos");
        }

        /// <summary>
        /// 根据职位编号查询职位，部门信息
        /// </summary>
        /// <param name="positionId">职位编号</param>
        /// <returns>职位信息</returns>
        public DataSet GetPositionByID(int positionId)
        {
            SqlParameter[] dataparam = new SqlParameter[] { new SqlParameter("@positionId", SqlDbType.Int) };
            dataparam[0].Value = positionId;
            return SqlServerHelper.RunProcedure("proc_GetPositionByPID", dataparam, "pos");
        }


        /// <summary>
        /// 根据部门编号查询岗位
        /// </summary>
        /// <param name="branchid">部门编号</param>
        /// <returns>职位信息</returns>
        public DataSet GetPositionByBrID(int branchid)
        {
            SqlParameter[] dataparam = new SqlParameter[] { new SqlParameter("@branchid", SqlDbType.Int) };
            dataparam[0].Value = branchid;
            return SqlServerHelper.RunProcedure("proc_GetPositionByBrID", dataparam, "pos");
        }
    }
}
