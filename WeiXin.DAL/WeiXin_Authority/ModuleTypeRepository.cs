
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
using WeiXin.Core.DBUtility;

namespace WeiXin.DAL
{
    public class ModuleTypeRepository
    {
        /// <summary>
        /// 查询所有的系统名称
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModuleType()
        {
            String sql = "select * from ModuleType";
            return SqlHelper.ExecuteDataset(CommandType.Text, sql);
        }

        /// <summary>
        /// 根据用户ID和角色ID查询当前登录用户拥有的菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public DataSet GetModuleTyepIdByUser(int userId, int roleId)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@userId",SqlDbType.Int){Value=userId},
                new SqlParameter("@roleId",SqlDbType.Int){Value=roleId}
            };
            return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "proc_selectModuleTypeByUsers", parameter);
        }
    }
}