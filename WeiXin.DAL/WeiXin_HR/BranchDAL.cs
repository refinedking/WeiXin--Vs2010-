
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


namespace WeiXin.DAL
{
    /// <summary>
    /// author:刘强
    /// time:2012-2-16
    /// effect:提供部门数据访问方法
    /// </summary>
    public class BranchDAL
    {
        WeiXinSystemDBDataContext db = new WeiXinSystemDBDataContext(SqlHelper.connectionString);
        /// <summary>
        /// 得到所有的部门信息的数据条数
        /// </summary>
        /// <param name="keyWords">关键字</param>
        /// <returns>返回所有部门信息的数据条数</returns>
        public int GetAllBranchCount(string keyWords)
        {
            SqlParameter[] dataparam = new SqlParameter[] {
                new SqlParameter("@keyWords", SqlDbType.VarChar,50) };
            dataparam[0].Value = keyWords;
            return Convert.ToInt32(SqlServerHelper.RunProc("proc_GetAllBranchCount",dataparam));
        }

        /// <summary>
        /// 部门信息分页方法
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="pageNo">当前页数</param>
        /// <param name="pageSize">显示数据条数</param>
        /// <param name="keyWords">查询条件</param>
        /// <returns>当前页数的数据</returns>
        public DataSet GetBranchByPager(string procName, int pageNo, int pageSize, string keyWords)
        {
            SqlParameter[] dataparam = new SqlParameter[] {
                new SqlParameter("@pageNo", SqlDbType.Int), 
                new SqlParameter("@rows", SqlDbType.Int), 
                new SqlParameter("@keyWords", SqlDbType.VarChar,50) };
            dataparam[0].Value = pageNo;
            dataparam[1].Value = pageSize;
            dataparam[2].Value = keyWords;
            return SqlServerHelper.RunProcedure(procName, dataparam, "branch");
            
        }

        /// <summary>
        /// 查询出所有的部门信息（只有部门名称与部门编号）
        /// </summary>
        /// <returns>部门信息（只有部门名称与部门编号）</returns>
        public DataSet  GetBranchNameAndBranchId()
        {
            return SqlServerHelper.RunProcedure("proc_GetBranchNameAndBranchId", null, "branch");
        }

        /// <summary>
        /// 根据部门名称查询该部门是否存在的方法
        /// </summary>
        /// <param name="branchName">部门名称</param>
        /// <returns>受影响的行数</returns>
        public int GetBranchByBranchName(string branchName)
        {
            SqlParameter[] dataparam = new SqlParameter[] { new SqlParameter("@branchName", SqlDbType.VarChar, 50) };
            dataparam[0].Value = branchName;
            return SqlServerHelper.RunProcedure("proc_GetBranchByBranchName", dataparam, "bran").Tables[0].Rows.Count;
        }

        /// <summary>
        /// 根据部门编号查询部门信息
        /// </summary>
        /// <param name="branchID">部门编号</param>
        /// <returns>部门信息</returns>
        public DataSet GetBranchByBranchID(int branchID)
        {
            SqlParameter[] dataparam = new SqlParameter[] { new SqlParameter("@branchID", SqlDbType.Int) };
            dataparam[0].Value = branchID;
            return SqlServerHelper.RunProcedure("proc_GetBranchByBranchID", dataparam, "branch");
        }

        /// <summary>
        /// 添加部门的方法
        /// </summary>
        /// <param name="branchInfo">部门实体</param>
        /// <returns>受影响的行数</returns>
        public int AddBranch(branchInfo branchInfo)
        {
            string sql = @"insert into branchInfo values('" + branchInfo.branchName + "'," + branchInfo.pBranchId + ",'" + branchInfo.branchPrincipal + "','" + branchInfo.branchPhone + "','" + branchInfo.branchFax + "','" + branchInfo.branchRemark + "','"+branchInfo.temp1+"','','','','','','','','','')";
            return SqlServerHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="branchInfo">部门信息实体</param>
        /// <returns>受影响的行数</returns>
        public int UpdateBranch(branchInfo branchInfo)
        {
            string sql = @"update branchInfo set branchName='" + branchInfo.branchName 
                + "',pBranchid=" + branchInfo.pBranchId 
                + ",branchPrincipal='"+ branchInfo.branchPrincipal 
                + "',branchPhone='" + branchInfo.branchPhone 
                + "',branchFax='"  + branchInfo.branchFax
                + "',branchRemark='" + branchInfo.branchRemark 
                +"',temp1='" + branchInfo.temp1 
                +"' where branchID=" + branchInfo.branchID + "";
            return SqlServerHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 根据部门编号查询该部门下是否还有子部门的方法
        /// </summary>
        /// <param name="branchID">部门编号</param>
        /// <returns>返回的数据条数</returns>
        public int IsHaveMicroelectronicsByBranchID(int branchID)
        {
            SqlParameter[] dataparam = new SqlParameter[] { new SqlParameter("@branchId", SqlDbType.Int) };
            dataparam[0].Value = branchID;
            return SqlServerHelper.RunProcedure("proc_IsHaveMicroelectronicsByBranchID", dataparam, "bran").Tables[0].Rows.Count;
        }

        /// <summary>
        /// 根据部门编号查询该部门下是否还有岗位信息的方法
        /// </summary>
        /// <param name="branchID">部门编号</param>
        /// <returns>返回的数据条数</returns>
        public int IsHavePositionByBranchID(int branchID)
        {
            SqlParameter[] dataparam = new SqlParameter[] { new SqlParameter("@branchId", SqlDbType.Int) };
            dataparam[0].Value = branchID;
            return SqlServerHelper.RunProcedure("proc_IsHavePositionByBranchID", dataparam, "bran").Tables[0].Rows.Count;
        }

        /// <summary>
        /// 删除部门信息的方法
        /// </summary>
        /// <param name="branchID">部门编号</param>
        /// <returns>受影响的行数</returns>
        public int DeleteBranch(int branchID)
        {
            string sql = @"delete from branchInfo where branchId=" + branchID;
            return SqlServerHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 根据父部门编号查询部门名称
        /// </summary>
        /// <param name="pBranchID">父部门编号</param>
        /// <returns>部门名称</returns>
        public DataSet GetBranchNameByPBranchID(int pBranchID)
        {
            SqlParameter[] dataparam = new SqlParameter[] { new SqlParameter("@pBranchID", SqlDbType.Int) };
            dataparam[0].Value = pBranchID;
            return SqlServerHelper.RunProcedure("proc_GetBranchNameByPBranchID", dataparam, "branch");
        }   
        /// <summary>
        /// 根据父部门编号查询所有的子部门
        /// </summary>
        /// <param name="pBranchID">父部门编号</param>
        /// <returns>部门名称</returns>
        public DataSet GetBranchByPBranchID(int pBranchID)
        {
            SqlParameter[] dataparam = new SqlParameter[] { new SqlParameter("@pBranchID", SqlDbType.Int) };
            dataparam[0].Value = pBranchID;
            return SqlServerHelper.RunProcedure("proc_GetBranchByPBranchID", dataparam, "branch");
        }
        

        /// <summary>
        /// 根据关键字查询部门信息的方法
        /// </summary>
        /// <param name="keyWords">关键字</param>
        /// <returns>部门信息</returns>
        public DataSet GetBranchByKeyWords(string keyWords)
        {
            SqlParameter[] dataparam = new SqlParameter[] { new SqlParameter("@keyWords", SqlDbType.VarChar, 50) };
            dataparam[0].Value = keyWords;
            return SqlServerHelper.RunProcedure("proc_GetBranchByKeyWords", dataparam, "branch");
        }

        /// <summary>
        /// 检查部门名称是否重复
        /// </summary>
        /// <param name="branchName">部门名称</param>
        /// <param name="branchId">部门编号</param>
        /// <returns>true表示存在  false表示不存在</returns>
        public bool IsExists(string branchName, string branchId)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@table",SqlDbType.VarChar,30){Value="branchInfo"},
                new SqlParameter("@strCondition",SqlDbType.VarChar,1000){Value=string.Format("branchName='{0}'",branchName)},
                new SqlParameter("@primaryKey",SqlDbType.VarChar,30){Value="branchID"},
                new SqlParameter("@id",SqlDbType.VarChar,30){Value=branchId}
            };
            try
            {
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "proc_SingleSelectData", parameter);
                return ds.Tables[0].Rows.Count > 0 ? false : true;
            }
            catch
            {
                return false;
            }
        }

        /******************************************************************************/

        /// <summary>
        /// 查询出所有的部门信息 
        /// </summary>
        /// <returns>部门信息</returns>
        public DataSet GetBranchAll()
        {
            return SqlServerHelper.RunProcedure("proc_GetBranchAll", null, "branch");
        }

       

        /// <summary>
        /// 根据DID查询渠道信息
        /// </summary>
        /// <param name="code">Code</param>
        /// <param name="codeLen">Code长度</param>
        /// <returns></returns>
        public DataSet FindBranchByCode(string code, int codeLen)
        {
            SqlParameter[] dataparam = new SqlParameter[] {
                new SqlParameter("@Code", SqlDbType.VarChar,50),new SqlParameter("@CodeLen", SqlDbType.Int) };
            dataparam[0].Value = code;
            dataparam[1].Value = codeLen;
            return SqlServerHelper.RunProcedure("Proc_FindBranchByCode", dataparam, "Branch");

        }


        /// <summary>
        /// 查询父级渠道
        /// </summary>
        /// <returns></returns>
        public DataSet FindIsFBranch(int CodeLen)
        {
            SqlParameter[] dataparam = new SqlParameter[] {
                new SqlParameter("@CodeLen", SqlDbType.Int) };
            dataparam[0].Value = CodeLen;
            return SqlServerHelper.RunProcedure("Proc_FindBranchIsFBranchByCodeLen", dataparam, "Ditch");
        }
        /// <summary>
        /// 查询最新插入的一条数据
        /// </summary>
       
        /// <returns></returns>
        public DataSet GetNewBranch()
        {
            string sql = @"select top 1 * from branchInfo order by branchID desc";
            return SqlServerHelper.Query(sql);
        }


        #region WAP
        /// <summary>
        /// 查询所有的分类，不包含根分类（联盟分类）
        /// </summary>
        /// <returns></returns>
        public List<branchInfo> GetBranchAllwap()
        {
            return (from b in db.branchInfo where b.temp1 != "0001" select b).ToList();
        }
        #endregion
    }
}
