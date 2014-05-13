
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

namespace WeiXin.DAL
{
    public class RoleAuthorityListRepository
    {
        StringBuilder sbString = new StringBuilder();

        /// <summary>
        /// 根据菜单ID与角色ID(或者用户ID)查询当前角色拥有的权限
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public DataSet GetRoleAuthority(int moduleId, string roleId = "", string userId = "")
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@ModuleID",moduleId),
                string.IsNullOrEmpty(userId)?new SqlParameter("@RoleID",int.Parse(roleId)):new SqlParameter("@UserID",int.Parse(userId))
            };
            if (!string.IsNullOrEmpty(userId))
            {
                return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "proc_SelectAuthorityByModuleIdAndUserId", parameter);
            }
            return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "proc_SelectAuthorityByModuleIdAndRoleId", parameter);
        }

        /// <summary>
        /// 更新权限(用户或者角色)
        /// </summary>
        /// <param name="role_Authority"></param>
        /// <param name="roleId"></param>
        /// <param name="modulesType"></param>
        /// <returns></returns>
        public bool UpdateRoleAuthority(string role_Authority, int moduleTypeID, string roleId = "", string userId = "")
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    SqlParameter[] parameter = new SqlParameter[]
                    {
                        new SqlParameter("@str",role_Authority),
                        new SqlParameter("@split",','),
                        new SqlParameter("@RoleID",int.Parse(roleId)),
                        new SqlParameter("@ModuleTypeID",moduleTypeID)
                    };
                    SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_Role_Authority", parameter);
                }
                else
                {
                    SqlParameter[] parameter = new SqlParameter[]
                    {
                        new SqlParameter("@str",role_Authority),
                        new SqlParameter("@split",','),
                        new SqlParameter("@userId",int.Parse(userId)),
                        new SqlParameter("@ModuleTypeID",moduleTypeID)
                    };
                    SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_User_Authority", parameter);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取用户拥有的权限
        /// </summary>
        /// <param name="userId">根据用户ID</param>
        /// <returns>返回DataSet(包含ModuleID,AuthorityIDS)</returns>
        public DataSet GetRoleAuthority(int userId)
        {
            //参数化
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@userID",SqlDbType.Int){Value=userId}
            };
            //执行函数
            return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "proc_Authority_SelectModule", parameter);
        }
    }
}