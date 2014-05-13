
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
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WeiXin.BO;
using WeiXin.Core;
using WeiXin.Core.Transfer;
using WeiXin.DAL;
using WeiXin.Model;

namespace WeiXin.BLL
{
    /// <summary>
    /// author:刘强
    /// time:2012-2-16
    /// effect:提供员工业务逻辑访问方法
    /// </summary>
    public class EmployeeService
    {
        EmployeeDAL ed = new EmployeeDAL();

        #region Mr.Wang
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="emp"></param>
        public int InsertEmp(EmployeeInfoContract emp,Users user,employeeData empdata)
        {
            return ed.InsertEmp(emp.ToPO<employeeInfo>(),user,empdata);

        }

        public employeeInfo GetAEmpInfoByApi(int id)
        {
            return ed.GetAEmpInfoByApi(id);
        }

        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="emp"></param>
        public int EditEmp(EmployeeInfoContract emp)
        {

          return  ed.EditEmp(emp.ToPO<employeeInfo>());
        }


        /// <summary>
        /// 查询 分类下面的企业
        /// </summary>
        /// <param name="brid"></param>
        /// <returns></returns>
        public List<employeeInfo> GetEmployeeInfoByBrIDwap(int brid)
        {
            return ed.GetEmployeeInfoByBrIDwap(brid);
        }

        /// <summary>
        /// 获取所有的企业
        /// </summary>
        /// <returns></returns>
        public List<employeeInfo> GetAllEmp()
        {
            List<employeeInfo> gjzList = ed.GetAllEmp();
            return gjzList;
        }
        /// <summary>
        /// 获取所有的企业
        /// </summary>
        /// <returns></returns>
        public List<employeeInfo> GetAllEmp(int eid)
        {
            List<employeeInfo> gjzList = ed.GetAllEmp(eid);
            return gjzList;
        }

        #endregion
        /// <summary>
        /// 得到所有的员工信息的数据条数
        /// </summary>
        /// <param name="keyWords">关键字</param>
        /// <returns>返回所有员工信息的数据条数</returns>
        public int GetAllEmployeeCount(string keyWords)
        {
            return ed.GetAllEmployeeCount(keyWords);
        }
        /// <summary>
        /// 查询所有的员工
        /// </summary>
        /// <returns></returns>
        public List<employeeInfo> GetListEmployee()
        {
            return ed.GetListEmployee();
        }

        /// <summary>
        /// 员工信息分页方法
        /// </summary>
        /// <param name="pageNo">当前页数</param>
        /// <param name="pageSize">显示数据条数</param>
        /// <param name="keyWords">查询条件</param>
        /// <returns>当前页数的数据</returns>
        public PagerList<employeeInfo> GetEmployeeByPager(int pageNo, int pageSize, string keyWords)
        {

            return ed.GetEmployeeByPager(pageNo, pageSize, keyWords);

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

            return ed.GetEmployeeByPager(eid,pageNo, pageSize, keyWords);

        }

        /// <summary>
        /// 根据员工编号查询该员工的职位与部门信息
        /// </summary>
        /// <param name="eId">员工编号</param>
        /// <returns>当前员工的职位与部门信息</returns>
        public DataSet GetPositionAndBranchByEmpId(int eId)
        {
            return ed.GetPositionAndBranchByEmpId(eId);
        }

        /// <summary>
        /// 判断员工身份证号是否存在的方法
        /// </summary>
        /// <param name="IdCard">身份证号</param>
        /// <returns></returns>
        public int IsExistIdCard(string IdCard)
        {
            return ed.IsExistIdCard(IdCard);
        }


        /// <summary>
        /// 添加员工的方法
        /// </summary>
        /// <param name="emp">员工实体</param>
        /// <param name="nowPosIds">职位编号</param>
        /// <returns></returns>
        public int InsertEmpInfo(EmployeeInfoContract emp, string nowPosIds)
        {
            employeeInfo employee = emp.ToPO<employeeInfo>();
            return ed.InsertEmpInfo(employee, nowPosIds);
        }

        /// <summary>
        /// 修改员工的方法
        /// </summary>
        /// <param name="emp">员工实体</param>
        /// <param name="nowPosIds">职位编号</param>
        /// <returns></returns>
        public int UpdateEmpInfo(EmployeeInfoContract emp, string nowPosIds)
        {
            employeeInfo employee = emp.ToPO<employeeInfo>();
            return ed.UpdateEmpInfo(employee, nowPosIds);
        }

        /// <summary>
        /// 根据员工编号查询该员工的信息
        /// </summary>
        /// <param name="eId">EID</param>
        /// <returns>当前员工的信息</returns>
        public EmployeeInfoContract GetEmpInfoByEId(int eId)
        {
            employeeInfo emp = ed.GetEmpInfoByEId(eId);
            return emp.ToBO<EmployeeInfoContract>();
        }

        /// <summary>
        /// 查询企业的信息
        /// </summary>
        /// <param name="eId">EID</param>
        /// <returns>当前员工的信息</returns>
        public employeeInfo GetEmployeeinfoByEId(int eId)
        {
            employeeInfo emp = ed.GetEmpInfoByEId(eId);
            return emp;
        }


        /// <summary>
        /// 根据员工号查询该员工的信息
        /// </summary>
        /// <param name="empId">员工号</param>
        /// <returns>当前员工的信息</returns>
        public EmployeeInfoContract GetEmpInfoByEmpId(string empId)
        {
            DataSet ds = ed.GetEmpInfoByEmpId(empId);
            return DataConvert.DataSetToEntity<EmployeeInfoContract>(ds, 0);
        }

        /// <summary>
        /// 根据关键字查询员工信息
        /// </summary>
        /// <param name="keyWords">关键字</param>
        /// <returns></returns>
        public DataSet GetEmpByKeyWords(string keyWords)
        {
            return ed.GetEmpByKeyWords(keyWords);
        }

        /// <summary>
        /// 得到所有的员工信息
        /// </summary>
        /// <param name="eId">员工EID</param>
        /// <returns>员工的信息</returns>
        public List<EmployeeInfoContract> GetAllEmpInfo()
        {
            DataSet ds = ed.GetAllEmpInfo();
            return DataConvert.DataSetToIList<EmployeeInfoContract>(ds, 0).ToList();
        }

        /// <summary>
        /// 根据部门编号查询员工信息
        /// </summary>
        /// <param name="branchId">部门编号</param>
        /// <returns></returns>
        public List<EmployeeInfoContract> GetEmployeeByBId(string branchId)
        {
            DataSet ds = ed.GetEmployeeByBId(branchId);
            return DataConvert.DataSetToIList<EmployeeInfoContract>(ds, 0).ToList();
        }
        /// <summary>
        /// 根据部门编号查询员工信息
        /// </summary>
        /// <param name="branchId">部门编号</param>
        /// <returns></returns>
        public List<EmployeeInfoContract> GetEmployeeByBId(int branchId)
        {
            DataSet ds = ed.GetEmployeeByBId(branchId);
            return DataConvert.DataSetToIList<EmployeeInfoContract>(ds, 0).ToList();
        }
        /// <summary>
        /// 根据部门编号查询员工信息(试用或者在职)
        /// </summary>
        /// <param name="branchId">部门编号</param>
        /// <returns></returns>
        public List<EmployeeInfoContract> GetEmployeeByBIdAndState(string branchId)
        {
            DataSet ds = ed.GetEmployeeByBIdAndState(branchId);
            return DataConvert.DataSetToIList<EmployeeInfoContract>(ds, 0).ToList();
        }
        /// <summary>
        /// 根据部门编号查询员工信息(试用或者在职)
        /// </summary>
        /// <param name="branchId">部门编号</param>
        /// <returns></returns>
        public DataSet GetEmpByBranchIDNowEmp(int branchId)
        {
            DataSet ds = ed.GetEmpByBranchIDNowEmp(branchId);
            return ds;
        }
        /// <summary>
        /// 获取最新的一条员工的信息
        /// </summary>
        /// <returns></returns>
        public EmployeeInfoContract GetLastEmployee()
        {
            DataSet ds = ed.GetLastEmployee();
            return DataConvert.DataSetToEntity<EmployeeInfoContract>(ds, 0);
        }

        /// <summary>
        /// 根据部门编号查询员工信息(不存在帐号的员工)
        /// </summary>
        /// <param name="branchId">部门编号</param>
        /// <returns></returns>
        public List<EmployeeInfoContract> GetEmployeeByBIdExistsUsers(string branchId)
        {
            DataSet ds = ed.GetEmployeeByBIdExistsUser(branchId);
            return DataConvert.DataSetToIList<EmployeeInfoContract>(ds, 0).ToList();
        }


        /// <summary>
        /// 检查员工的身份证号是否重复
        /// </summary>
        /// <param name="idCard">身份证号码</param>
        /// <param name="id">eId</param>
        /// <returns>true表示存在  false表示不存在</returns>
        public bool IsExists(string idCard, string id)
        {
            return ed.IsExists(idCard, id);
        }


        /// <summary>
        /// 根据职位编号查询员工信息
        /// </summary>
        /// <param name="pId">职位编号</param>
        /// <returns></returns>
        public List<EmployeeInfoContract> GetEmployeeByPId(string pId)
        {
            DataSet ds = ed.GetEmployeeByPId(pId);
            return DataConvert.DataSetToIList<EmployeeInfoContract>(ds, 0).ToList();
        }

        /// <summary>
        /// 根据多个职位编号查询员工信息
        /// </summary>
        /// <param name="pId">职位编号</param>
        /// <returns></returns>
        public List<EmployeeInfoContract> GetEmployeeByPIdS(string pId)
        {
            DataSet ds = ed.GetEmployeeByPIdS(pId);
            return DataConvert.DataSetToIList<EmployeeInfoContract>(ds, 0).ToList();
        }

        /// <summary>
        /// 根据职位编号查询员工信息(不包含离职的)
        /// </summary>
        /// <param name="pId">职位编号</param>
        /// <returns></returns>
        public List<EmployeeInfoContract> GetEmpByPId(string pId)
        {
            DataSet ds = ed.GetEmplByPId(pId);
            return DataConvert.DataSetToIList<EmployeeInfoContract>(ds, 0).ToList();
        }

        /// <summary>
        /// 根据职位编号查询员工信息(不包含离职的)
        /// </summary>
        /// <param name="pId">职位编号(1,2,3)</param>
        /// <returns></returns>
        public List<EmployeeInfoContract> GetEmplByPIds(string pId)
        {
            DataSet ds = ed.GetEmplByPIds(pId);
            return DataConvert.DataSetToIList<EmployeeInfoContract>(ds, 0).ToList();
        }

        /// <summary>
        /// 根据职位编号查询员工信息(不包含离职的)返回dataset
        /// </summary>
        /// <param name="pId">职位编号</param>
        /// <returns></returns>
        public DataSet GetEmpDataSetByPId(string pId)
        {
            DataSet ds = ed.GetEmplByPId(pId);
            return ds;
        }



        /***************************************************************/

        /// <summary>
        /// 根据部门名称查询员工信息
        /// </summary>
        /// <param name="BranchID">部门ID</param>
        /// <returns></returns>
        public DataSet GetEmpinfoByBranchID(int BranchID)
        {
            return ed.GetEmpinfoByBranchID(BranchID);
        }

        /// <summary>
        /// 查询本月入职的员工
        /// </summary>
        /// <param name="date"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public DataSet GetEmpInfoByStartDate(string date, string days)
        {
            return ed.GetEmpInfoByStartDate(date, days);
        }

        /// <summary>
        /// 根据员工姓名查询
        /// </summary>
        /// <param name="name">姓名</param>
        /// <returns></returns>
        public DataSet GetEmpInfoByEName(string name)
        {
            return ed.GetEmpInfoByEName(name);
        }


    }
}