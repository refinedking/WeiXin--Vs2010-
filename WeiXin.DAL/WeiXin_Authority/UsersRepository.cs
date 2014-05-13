
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
/*-------------------------------------------------------

// Copyright (C) 2011 重庆足下科技有限公司 版权所有。
// 文件名：  UsersRepository.cs
// 功能描述：用户信息数据访问类
// 创建标识：李伟  2012-05-09
// 修改标识：见每个方法前面
// 修改描述：见每个方法前面
//------------------------------------------------------*/
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
    public class UsersRepository
    {
        StringBuilder sbString = new StringBuilder();

        /// <summary>
        /// 查询用户信息或者根据userId查询单个用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataSet GetUsers(string userId = "")
        {
            sbString.Clear();
            sbString.Append("select * from Users");
            if (!string.IsNullOrEmpty(userId))
            {
                sbString.Append(" where UserId=@UserId");
                SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@UserId",int.Parse(userId))
                };
                return SqlHelper.ExecuteDataset(CommandType.Text, sbString.ToString(), parameter);
            }
            return SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, sbString.ToString());
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public DataSet InsertUsers(Users users)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@employeeID",users.EmployeeID),
                    new SqlParameter("@roleID",users.RoleID),
                    new SqlParameter("@userName",users.UserName)
                };
                return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "proc_InsertUser", parameter);
            }
            catch (SqlException)
            {
                return null;
            }
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public bool UpdateUsers(Users users)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@userId",users.UserID),
                    new SqlParameter("@roleId",users.RoleID)
                };
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_UpdateUsers", parameter);
            }
            catch (SqlException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 根据用户名和密码查询用户
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        public DataSet GetUsers(string userLogin, string userPwd)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@UserName",SqlDbType.VarChar,50){Value=userLogin},
                new SqlParameter("@Password",SqlDbType.VarChar,100){Value=userPwd}
            };
            return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "proc_selectUser", parameter);
        }

        /// <summary>
        /// 查询分页数据(全部的数据分页)
        /// </summary>
        /// <returns>返回数据数组格式：  索引-：数据集  索引二：当前页码   索引三：显示数据量  索引四：总数据量</returns>
        public object[] GetPageDataRoles(int pageIndex = 0, int pageSize = 0, string employeeId = "", string userName = "")
        {
            sbString.Clear();
            sbString.Append(string.IsNullOrEmpty(userName) ? "" : string.Format(" and  userName like '%{0}%'", userName));
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@tblName",SqlDbType.VarChar,200){Value="Users"},
                new SqlParameter("@ID",SqlDbType.VarChar,150){Value="UserID"},
                new SqlParameter("@page",SqlDbType.Int){Value=pageIndex==0?1:pageIndex,Direction=ParameterDirection.InputOutput},
                new SqlParameter("@pageSize",SqlDbType.Int){Value=pageSize==0?10:pageSize,Direction=ParameterDirection.InputOutput},
                new SqlParameter("@Counts",SqlDbType.Int){Direction=ParameterDirection.Output},
                new SqlParameter("@strCondition",SqlDbType.VarChar,300){Value=sbString.ToString()}
            };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "proc_paged_2part_selectMax", parameter);
            return new object[] { ds, parameter[2].Value, parameter[3].Value, parameter[4].Value };
        }

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public bool UpdateUsersPassword(string userIds)
        {
            SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@userId",userIds)
                };
            try
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_updateUser_UserPassword", parameter);
            }
            catch (SqlException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public bool UpdateUsersStatus(string userIds)
        {
            SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@userIds",userIds),
                    new SqlParameter("@split",",")
                };
            try
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_updateUser_State", parameter);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>true表示存在  false表示不存在</returns>
        public bool DeleteUsers(string userIds)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@userID",userIds)
            };
            try
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_DeleteUser", parameter);
            }
            catch (SqlException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检查用户名帐号不能重复
        /// </summary>
        /// <param name="authorityName"></param>
        /// <param name="authorityTag"></param>
        /// <returns>true表示不存在  false表示存在</returns>
        public bool IsExists(string userId, string userName)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@table",SqlDbType.VarChar,30){Value="Users"},
                new SqlParameter("@strCondition",SqlDbType.VarChar,1000){Value=string.Format("UserName='{0}'",userName)},
                new SqlParameter("@primaryKey",SqlDbType.VarChar,30){Value="UserID"},
                new SqlParameter("@id",SqlDbType.VarChar,30){Value=userId}
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
        /// 根据帐号和新密码修改密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool UpdatePwd(string userName, string pwd)
        {
            string sql = "UPDATE [dbo].[Users] SET [Password] = '" + pwd + "' WHERE [UserName]= '" + userName + "'";
            return SqlServerHelper.ExecuteSql(sql) > 0;
        }
    }
}