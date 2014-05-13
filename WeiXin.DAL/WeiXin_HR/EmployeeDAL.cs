
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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WeiXin.Core.DBUtility;
using WeiXin.Model;

namespace WeiXin.DAL
{
    /// <summary>
    /// author:刘强
    /// time:2012-2-16
    /// effect:提供员工数据访问方法
    /// </summary>
    public class EmployeeDAL
    {
        WeiXinSystemDBDataContext db = new WeiXinSystemDBDataContext(SqlHelper.connectionString);

        #region Mr.Wang


        /// <summary>
        /// 获取所有的企业
        /// </summary>
        /// <returns></returns>
        public List<employeeInfo> GetAllEmp()
        {
            IQueryable<employeeInfo> list = (from cr in db.employeeInfo select cr);
            return list.ToList();
        }

        /// <summary>
        /// 获取所有的企业
        /// </summary>
        /// <returns></returns>
        public List<employeeInfo> GetAllEmp(int eid)
        {
            IQueryable<employeeInfo> list = (from cr in db.employeeInfo where cr.Eid == eid select cr);
            return list.ToList();
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="emp"></param>
        public int InsertEmp(employeeInfo emp,Users user,employeeData empdata)
        {
            db.employeeInfo.InsertOnSubmit(emp);
            db.SubmitChanges();
            user.EmployeeID = emp.Eid;
            user.Password = "iqYMA+IwRJo=";//初始密码，123456
            user.Status = 0;
            user.CreateTime = DateTime.Now;
            empdata.eid = emp.Eid;
            db.employeeData.InsertOnSubmit(empdata);
            db.Users.InsertOnSubmit(user);

            db.SubmitChanges();
            return emp.Eid;
        }

        public employeeInfo GetAEmpInfoByApi(int id)
        {
            return (from emp in db.employeeInfo where emp.Eid==id select emp).FirstOrDefault();
        }


        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="emp"></param>
        public int EditEmp(employeeInfo emp)
        {
            string sql = string.Format("update employeeInfo set wxName='{0}',wxUser='{1}',wxOldUser='{2}',ToKen='{3}',Name='{4}',BranchID={5},positionId={6},phone='{7}',TEL='{8}',Address='{9}',LxrName='{10}',temp1='{12}',temp2='{13}',temp3='{14}',temp4='{15}',temp5='{16}' where Eid={11}", emp.wxName, emp.wxUser, emp.wxOldUser, emp.ToKen, emp.Name, emp.BranchID, emp.positionId, emp.phone, emp.TEL, emp.Address, emp.LxrName, emp.Eid,emp.temp1,emp.temp2,emp.temp3,emp.temp4,emp.temp5);
          return  SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
        }

        /// <summary>
        /// 查询 分类下面的企业
        /// </summary>
        /// <param name="brid"></param>
        /// <returns></returns>
        public List<employeeInfo> GetEmployeeInfoByBrIDwap(int brid)
        {
            return (from e in db.employeeInfo where e.BranchID == brid select e).ToList();
        }
        #endregion

        /// <summary>
        /// 查询所有的员工
        /// </summary>
        /// <returns></returns>
        public List<employeeInfo> GetListEmployee()
        {
            return (from e in db.employeeInfo where e.isDisplay==0 select e).ToList();
        }
        /// <summary>
        /// 得到所有的员工信息的数据条数
        /// </summary>
        /// <param name="keyWords">关键字</param>
        /// <returns>返回所有员工信息的数据条数</returns>
        public int GetAllEmployeeCount(string keyWords)
        {
            SqlParameter[] dataparam = new SqlParameter[] {
                new SqlParameter("@keyWords", SqlDbType.VarChar,50) };
            dataparam[0].Value = keyWords;
            DataSet ds = SqlServerHelper.RunProcedure("proc_GetAllEmployeeCount", dataparam, "emp");
            return Convert.ToInt32(ds.Tables[0].Rows.Count);
        }

        /// <summary>
        /// 企业信息分页方法
        /// </summary>
        /// <param name="pageNo">当前页数</param>
        /// <param name="pageSize">显示数据条数</param>
        /// <param name="keyWords">查询条件</param>
        /// <returns>当前页数的数据</returns>
        public PagerList<employeeInfo> GetEmployeeByPager(int pageNo, int pageSize, string keyWords)
        {
            IQueryable<employeeInfo> list = (from cr in db.employeeInfo
                                             where (cr.wxName.Contains(keyWords.Trim())
                                           )
                                             select cr);
            int totalData = list.Count();
            return new PagerList<employeeInfo>(list.OrderByDescending(o => o.Eid), pageNo, pageSize, totalData);

        }
        /// <summary>
        /// 企业信息分页方法2 根据自己企业的ID查询自己的企业
        /// </summary>
        /// <param name="pageNo">当前页数</param>
        /// <param name="pageSize">显示数据条数</param>
        /// <param name="keyWords">查询条件</param>
        /// <returns>当前页数的数据</returns>
        public PagerList<employeeInfo> GetEmployeeByPager(int eid,int pageNo, int pageSize, string keyWords)
        {
            IQueryable<employeeInfo> list = (from cr in db.employeeInfo
                                             where (cr.wxName.Contains(keyWords.Trim())
                                             && cr.Eid==eid
                                           )
                                             select cr);
            int totalData = list.Count();
            return new PagerList<employeeInfo>(list.OrderByDescending(o => o.Eid), pageNo, pageSize, totalData);

        }
        /// <summary>
        /// 根据员工编号查询该员工的职位与部门信息
        /// </summary>
        /// <param name="eId">员工编号</param>
        /// <returns>当前员工的职位与部门信息</returns>
        public DataSet GetPositionAndBranchByEmpId(int eId)
        {
            SqlParameter[] dataparam = new SqlParameter[] {
                new SqlParameter("@eId", SqlDbType.Int)};
            dataparam[0].Value = eId;
            return SqlServerHelper.RunProcedure("proc_GetPositionAndBranchByEmpId", dataparam, "emp");
        }

        /// <summary>
        /// 判断员工身份证号是否存在的方法
        /// </summary>
        /// <param name="IdCard">身份证号</param>
        /// <returns></returns>
        public int IsExistIdCard(string IdCard)
        {
            SqlParameter[] dataparam = new SqlParameter[] {
                new SqlParameter("@IdCard", SqlDbType.VarChar,50)};
            dataparam[0].Value = IdCard;
            return SqlServerHelper.RunProcedure("proc_IsExistIdCard", dataparam, "emp").Tables[0].Rows.Count;
        }

        /// <summary>
        /// 得到员工表中的最新一条数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetLastEmployee()
        {
            return SqlServerHelper.RunProcedure("proc_GetLasTemployee", null, "emp");
        }

        /// <summary>
        /// 添加员工的方法
        /// </summary>
        /// <param name="emp">员工实体</param>
        /// <param name="nowPosIds">职位编号</param>
        /// <returns></returns>
        public int InsertEmpInfo(employeeInfo emp, string nowPosIds)
        {
            int count = 0;

            return count;
        }

        /// <summary>
        /// 修改员工的方法
        /// </summary>
        /// <param name="emp">员工实体</param>
        /// <param name="nowPosIds">职位编号</param>
        /// <returns></returns>
        public int UpdateEmpInfo(employeeInfo emp, string nowPosIds)
        {
            int count = 0;

            return count;
        }

        /// <summary>
        /// 根据员工编号查询该员工的信息
        /// </summary>
        /// <param name="eId">Eid</param>
        /// <returns>当前员工的信息</returns>
        public employeeInfo GetEmpInfoByEId(int eId)
        {
            employeeInfo emp = (from e in db.employeeInfo where e.Eid == eId select e).FirstOrDefault();
            return emp;
        }

        /// <summary>
        /// 根据员工号查询该员工的信息
        /// </summary>
        /// <param name="empId">员工号</param>
        /// <returns>当前员工的信息</returns>
        public DataSet GetEmpInfoByEmpId(string empId)
        {
            SqlParameter[] dataparam = new SqlParameter[] {
                new SqlParameter("@empId", SqlDbType.VarChar,10)};
            dataparam[0].Value = empId;
            return SqlServerHelper.RunProcedure("proc_GetEmployeeByEmpId", dataparam, "emp");
        }

        /// <summary>
        /// 根据关键字查询员工信息
        /// </summary>
        /// <param name="keyWords">关键字</param>
        /// <returns></returns>
        public DataSet GetEmpByKeyWords(string keyWords)
        {
            SqlParameter[] dataparam = new SqlParameter[] {
                new SqlParameter("@keyWords", SqlDbType.VarChar,50)};
            dataparam[0].Value = keyWords;
            return SqlServerHelper.RunProcedure("proc_GeTemployeeByKeyWords", dataparam, "emp");
        }

        /// <summary>
        /// 得到所有的员工信息
        /// </summary>
        /// <param name="eId">员工EID</param>
        /// <returns>员工的信息</returns>
        public DataSet GetAllEmpInfo()
        {
            return SqlServerHelper.RunProcedure("proc_GetAllEmployee", null, "emp");
        }

        /// <summary>
        /// 根据部门编号查询员工信息
        /// </summary>
        /// <param name="branchId">部门编号</param>
        /// <returns></returns>
        public DataSet GetEmployeeByBId(string branchId)
        {
            SqlParameter[] dataparam = new SqlParameter[] {
                new SqlParameter("@branchId", SqlDbType.Int)};
            dataparam[0].Value = branchId;
            return SqlServerHelper.RunProcedure("proc_GetEmployeeByBranchID", dataparam, "emp");
        }
        /// <summary>
        /// 根据部门编号查询员工信息(不包含重复信息)
        /// </summary>
        /// <param name="branchId">部门编号</param>
        /// <returns></returns>
        public DataSet GetEmployeeByBId(int branchId)
        {
            string sql = string.Format("select * from employeeInfo where eId in(select eId from empIdOrpId where pId in(select positionId from positionInfo where branchId={0}))", branchId);
            return SqlHelper.ExecuteDataset(CommandType.Text, sql);
        }

        /// <summary>
        /// 根据部门编号查询员工信息(试用或者在职)
        /// </summary>
        /// <param name="branchId">部门编号</param>
        /// <returns></returns>
        public DataSet GetEmployeeByBIdAndState(string branchId)
        {
            SqlParameter[] dataparam = new SqlParameter[] {
                new SqlParameter("@branchId", SqlDbType.Int)};
            dataparam[0].Value = branchId;
            return SqlServerHelper.RunProcedure("proc_GetEmployeeByBranchID2", dataparam, "emp");
        }

        /// <summary>
        /// 根据部门编号查询员工信息(不存在帐号的员工)
        /// </summary>
        /// <param name="branchId">部门编号</param>
        /// <returns></returns>
        public DataSet GetEmployeeByBIdExistsUser(string branchId)
        {
            SqlParameter[] dataparam = new SqlParameter[] {
                new SqlParameter("@branchId", SqlDbType.Int)};
            dataparam[0].Value = branchId;
            return SqlServerHelper.RunProcedure("proc_GetEmployeeByBranchIDAndExistsUser", dataparam, "emp");
        }

        /// <summary>
        /// 检查员工的身份证号是否重复
        /// </summary>
        /// <param name="idCard">身份证号码</param>
        /// <param name="id">eId</param>
        /// <returns>true表示存在  false表示不存在</returns>
        public bool IsExists(string idCard, string id)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@table",SqlDbType.VarChar,30){Value="employeeInfo"},
                new SqlParameter("@strCondition",SqlDbType.VarChar,1000){Value=string.Format("empRawness='{0}'",idCard)},
                new SqlParameter("@primaryKey",SqlDbType.VarChar,30){Value="eId"},
                new SqlParameter("@id",SqlDbType.VarChar,30){Value=id}
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


        /// <summary>
        /// 根据职位编号查询员工信息
        /// </summary>
        /// <param name="pId">职位编号</param>
        /// <returns></returns>
        public DataSet GetEmployeeByPId(string pId)
        {
            SqlParameter[] dataparam = new SqlParameter[] {
                new SqlParameter("@positionId", SqlDbType.Int)};
            dataparam[0].Value = pId;
            return SqlServerHelper.RunProcedure("proc_GetEmployeeByPId", dataparam, "emp");
        }

        /// <summary>
        /// 根据多个职位编号查询员工信息
        /// </summary>
        /// <param name="pId">职位编号</param>
        /// <returns></returns>
        public DataSet GetEmployeeByPIdS(string pId)
        {
            SqlParameter[] dataparam = new SqlParameter[] {
                new SqlParameter("@positionId", SqlDbType.VarChar,50)};
            dataparam[0].Value = pId;
            return SqlServerHelper.RunProcedure("proc_GetEmployeeByPIdS", dataparam, "emp");
        }


        /// <summary>
        /// 根据职位编号查询员工信息(不包含离职的)
        /// </summary>
        /// <param name="pId">职位编号</param>
        /// <returns></returns>
        public DataSet GetEmplByPId(string pId)
        {
            SqlParameter[] dataparam = new SqlParameter[] {
                new SqlParameter("@positionId", SqlDbType.Int)};
            dataparam[0].Value = pId;
            return SqlServerHelper.RunProcedure("proc_GetEmpByPId", dataparam, "emp");
        }

        /// <summary>
        /// 根据多个职位编号查询员工信息(不包含离职的)
        /// </summary>
        /// <param name="pId">职位编号(1,2,3)</param>
        /// <returns></returns>
        public DataSet GetEmplByPIds(string pId)
        {
            string sql = "select * from employeeinfo where eId in(select eid from empIdOrpId where pid in (" + pId + ")) and empState<>2  ";
            return SqlHelper.ExecuteDataset(CommandType.Text, sql);
        }
        /// <summary>
        /// 获取所有员工所属职位和部门
        /// </summary>
        /// <returns></returns>
        public DataSet GetEmpAndBranch(string empState)
        {
            switch (empState)
            {
                case "":
                    empState = "0,1,2";
                    break;
            }
            string sql = "SELECT distinct(EI.empId),EI.empName,EI.empState,PI.branchId FROM employeeInfo EI LEFT JOIN empIdOrpId EO ON EI.eId=EO.eId LEFT JOIN positionInfo PI ON PI.positionId=EO.pId where EI.empState in (" + empState + ")";
            return SqlHelper.ExecuteDataset(CommandType.Text, sql);
        }
        /***************************************************************************/

        /// <summary>
        /// 根据部门名称查询员工信息
        /// </summary>
        /// <param name="brName">部门名称</param>
        /// <returns></returns>
        public DataSet GetEmpinfoByBranchID(int branchid)
        {
            SqlParameter[] dataparam = new SqlParameter[] {
                new SqlParameter("@branchid", SqlDbType.Int)};
            dataparam[0].Value = branchid;
            return SqlServerHelper.RunProcedure("Proc_SelectEmpByBranchID", dataparam, "pos");
        }

        /// <summary>
        ///  根据部门编号查询员工信息(试用或者在职)
        /// </summary>
        /// <param name="brName">部门名称</param>
        /// <returns></returns>
        public DataSet GetEmpByBranchIDNowEmp(int branchid)
        {
            SqlParameter[] dataparam = new SqlParameter[] {
                new SqlParameter("@branchid", SqlDbType.Int)};
            dataparam[0].Value = branchid;
            return SqlServerHelper.RunProcedure("Proc_GetEmpByBranchIDNowEmp", dataparam, "pos");
        }


        /// <summary>
        /// 查询本月入职的员工
        /// </summary>
        /// <param name="date"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public DataSet GetEmpInfoByStartDate(string date, string days)
        {
            SqlParameter[] dataparam = new SqlParameter[] {
                new SqlParameter("@date", SqlDbType.VarChar,20),
            new SqlParameter("@days",SqlDbType.VarChar,20)};
            dataparam[0].Value = date;
            dataparam[1].Value = days;
            return SqlServerHelper.RunProcedure("proc_GeTemployeeByStartDate", dataparam, "pos");
        }

        /// <summary>
        /// 根据员工姓名查询
        /// </summary>
        /// <param name="name">姓名</param>
        /// <returns></returns>
        public DataSet GetEmpInfoByEName(string name)
        {
            SqlParameter[] dataparam = new SqlParameter[] {
                new SqlParameter("@empName", SqlDbType.VarChar,20)};
            dataparam[0].Value = name;
            return SqlServerHelper.RunProcedure("proc_GetEmployeeByEName", dataparam, "pos");
        }




    }
}