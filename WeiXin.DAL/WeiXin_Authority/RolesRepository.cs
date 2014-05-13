
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
using WeiXin.Core.DBUtility;
using WeiXin.Model;

namespace WeiXin.DAL
{
    public class RolesRepository
    {
        StringBuilder sbString = new StringBuilder();

        /// <summary>
        /// 查询全部角色或者根据ID查询单个角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetRoles(string id = "")
        {
            sbString.Clear();
            sbString.Append("select * from Roles");
            if (!string.IsNullOrEmpty(id))
            {
                sbString.Append(string.Format(" where RoleId={0}", int.Parse(id)));
            }
            return SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, sbString.ToString());
        }

        /// <summary>
        /// 查询分页数据(全部的数据分页)
        /// </summary>
        /// <returns>返回数据数组格式：  索引-：数据集  索引二：当前页码   索引三：显示数据量  索引四：总数据量</returns>
        public object[] GetPageDataRoles(int pageIndex = 0, int pageSize = 0, string roleName = "")
        {
            SqlParameter[] parameter = parameter = new SqlParameter[]
            {
                new SqlParameter("@tblName",SqlDbType.VarChar,200){Value="Roles"},
                new SqlParameter("@ID",SqlDbType.VarChar,150){Value="RoleID"},
                new SqlParameter("@page",SqlDbType.Int){Value=pageIndex==0?1:pageIndex,Direction=ParameterDirection.InputOutput},
                new SqlParameter("@pageSize",SqlDbType.Int){Value=pageSize==0?10:pageSize,Direction=ParameterDirection.InputOutput},
                new SqlParameter("@Counts",SqlDbType.Int){Direction=ParameterDirection.Output},
                new SqlParameter("@strCondition",SqlDbType.VarChar,300){Value=string.IsNullOrEmpty(roleName)?"":string.Format(" and RoleName like '%{0}%'",roleName)}
            };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "proc_paged_2part_selectMax", parameter);
            return new object[] { ds, parameter[2].Value, parameter[3].Value, parameter[4].Value };
        }

        /// <summary>
        /// 添加角色信息
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public string AddRole(Roles roles)
        {

            sbString.Clear();
            try
            {
                SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@RoleName",roles.RoleName),
                    new SqlParameter("@RoleDescription",roles.RoleDescription),
                    new SqlParameter("@RoleId",roles.RoleID){ Direction=ParameterDirection.Output}
                };
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_InsertRole", parameter);
                return parameter[2].Value.ToString();
            }
            catch (SqlException)
            {
                return "";
            }

        }

        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool UpdateRole(Roles roles)
        {


            sbString.Clear();
            try
            {
                sbString.Append("update Roles set RoleName=@RoleName,RoleDescription=@RoleDescription where RoleID=@RoleID");
                SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@RoleName",roles.RoleName),
                    new SqlParameter("@RoleDescription",roles.RoleDescription),
                    new SqlParameter("@RoleID",roles.RoleID)
                };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sbString.ToString(), parameter);
            }
            catch (SqlException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更新角色状态
        /// </summary>
        /// <param name="roleIds">角色ID  比如 1，2，3 或者 1</param>
        /// <returns></returns>
        public bool UpdateRoleStatus(string roleIds)
        {
            SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@roleIds",roleIds),
                    new SqlParameter("@split",",")
                };
            try
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_Role_Status", parameter);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 根据角色ID删除角色信息（级联删除，并删除当前角色拥有的页面功能权限）
        /// </summary>
        /// <param name="roleIDs"></param>
        /// <returns></returns>
        public bool DeleteRole(string roleIDs)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@table","Roles"),
                new SqlParameter("@property","RoleID"),
                new SqlParameter("@str",roleIDs)
            };
            try
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_SingleDelete", parameter);
            }
            catch (SqlException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检查角色名称是否存在
        /// </summary>
        /// <param name="authorityName"></param>
        /// <param name="authorityTag"></param>
        /// <returns></returns>
        public bool IsExists(string roleName)
        {
            SqlParameter[] parameter = new SqlParameter[]
             {
                 new SqlParameter("@roleName",SqlDbType.NChar,50){Value=roleName}
             };
            try
            {
                SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "IsExistsRoleName", parameter);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}