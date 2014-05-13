
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
using System.Data;
using System.Data.SqlClient;
using System.Text;
using WeiXin.Core.DBUtility;

namespace WeiXin.DAL
{
    public class ModuleAuthorityListRepository
    {
        /// <summary>
        /// 根据菜单ID查询当前菜单拥有的页面功能
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public DataSet GetAuthorityByModuleID(int moduleId)
        {
            StringBuilder sbString = new StringBuilder();
            sbString.Append("select * from ModuleAuthorityList where ModuleID=@moduleId");
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@moduleId",moduleId)
            };
            return SqlHelper.ExecuteDataset(CommandType.Text, sbString.ToString(), parameter);
        }
    }
}