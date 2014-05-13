
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
    public class AuthorityRepository
    {
        StringBuilder sbString = new StringBuilder();

        #region 查询功能点

        /// <summary>
        /// 查询分页数据或者根据功能点名称查询分页数据
        /// </summary>
        /// <returns>返回数据数组格式：  索引-：数据集  索引二：当前页码   索引三：显示数据量  索引四：总数据量</returns>
        public object[] GetPageDataAuthority(int pageIndex = 0, int pageSize = 0, string authorityName = "")
        {
            SqlParameter[] parameter = parameter = new SqlParameter[]
            {
                new SqlParameter("@tblName",SqlDbType.VarChar,200){Value="Authority"},
                new SqlParameter("@ID",SqlDbType.VarChar,150){Value="AuthorityID"},
                new SqlParameter("@fldSort",SqlDbType.VarChar,200){Value="AuthorityOrder"},
                new SqlParameter("@page",SqlDbType.Int){Value=pageIndex==0?1:pageIndex,Direction=ParameterDirection.InputOutput},
                new SqlParameter("@pageSize",SqlDbType.Int){Value=pageSize==0?10:pageSize,Direction=ParameterDirection.InputOutput},
                new SqlParameter("@Counts",SqlDbType.Int){Direction=ParameterDirection.Output},
                new SqlParameter("@strCondition",SqlDbType.VarChar,300){Value=string.IsNullOrEmpty(authorityName)?"":string.Format(" and AuthorityName like '%{0}%'",authorityName)}
            };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "proc_paged_2part_selectMax", parameter);
            return new object[] { ds, parameter[3].Value, parameter[4].Value, parameter[5].Value };
        }

        /// <summary>
        /// 根据所有功能点（用于其他模块判断）或者根据编号查询功能点（用于显示数据详细）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetAuthority(string id = "")
        {
            sbString.Clear();
            sbString.Append("select * from Authority");
            if (string.IsNullOrEmpty(id))
            {
                return SqlHelper.ExecuteDataset(CommandType.Text, sbString.ToString());
            }
            else
            {
                sbString.Append(" where AuthorityId=@AuthorityId");
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@AuthorityId",int.Parse(id))
                };
                return SqlHelper.ExecuteDataset(CommandType.Text, sbString.ToString(), parameters);
            }
        }

        /// <summary>
        /// 查询菜单拥有的功能点（用于查询用户拥有的功能点）
        /// </summary>
        /// <param name="moduleId">菜单编号</param>
        /// <returns></returns>
        public DataSet GetAuthorityByModuleID(int moduleId)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@ModuleID",SqlDbType.Int){Value=moduleId}
            };
            return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "proc_SelectAuthorityByModuleId", parameter);
        }

        /// <summary>
        /// 检查功能点名称或者标识是否重复
        /// </summary>
        /// <param name="authorityName"></param>
        /// <param name="authorityTag"></param>
        /// <returns>true表示不存在  false表示存在</returns>
        public bool IsExists(string authorityTag, string id)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@table",SqlDbType.VarChar,30){Value="Authority"},
                new SqlParameter("@strCondition",SqlDbType.VarChar,1000){Value=string.Format("AuthorityTag='{0}'",authorityTag)},
                new SqlParameter("@primaryKey",SqlDbType.VarChar,30){Value="AuthorityID"},
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

        #endregion 查询功能点

        #region 新增功能点

        /// <summary>
        /// 添加功能点
        /// </summary>
        /// <param name="authority"></param>
        /// <returns></returns>
        public bool AddAuthority(Authority authority)
        {

            sbString.Append("insert into Authority values(@AuthorityName,@AuthorityTag,@AuthorityDescription,@AuthorityOrder,@AuthorityState);");
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@AuthorityName",authority.AuthorityName),
                new SqlParameter("@AuthorityDescription",authority.AuthorityDescription),
                new SqlParameter("@AuthorityOrder",authority.AuthorityOrder),
                new SqlParameter("@AuthorityState",authority.AuthorityState),
                new SqlParameter("@AuthorityTag",authority.AuthorityTag)
            };
            try
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, sbString.ToString(), parameters);
            }
            catch
            {
                return false;
            }
            return true;
        }

        #endregion 新增功能点

        #region 编辑功能点

        /// <summary>
        /// 修改功能点
        /// </summary>
        /// <param name="authority"></param>
        /// <returns></returns>
        public bool UpdateAuthority(Authority authority)
        {
            
            return true;
        }

        /// <summary>
        /// 更新功能点状态
        /// </summary>
        /// <param name="authorityIds">功能点ID</param>
        /// <returns></returns>
        public bool UpdateAuthorityStatus(string authorityIds)
        {
            SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@authorityIds",authorityIds),
                    new SqlParameter("@split",",")
                };
            try
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_Authority_Status", parameter);
            }
            catch
            {
                return false;
            }
            return true;
        }

        #endregion 编辑功能点

        #region 删除功能点

        /// <summary>
        /// 删除功能点（级联删除）
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteAuthority(string ids)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@authorityId",ids)
            };
            try
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_DeleteAuthority", parameter);
            }
            catch (SqlException)
            {
                return false;
            }
            return true;
        }

        #endregion 删除功能点
    }
}