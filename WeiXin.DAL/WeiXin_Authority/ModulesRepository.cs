
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
    public class ModulesRepository
    {
        StringBuilder sbString = new StringBuilder();

        /// <summary>
        /// 查询所有菜单数据或者根据ID查询单个菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetModules(string id = "")
        {
            sbString.Clear();
            if (string.IsNullOrEmpty(id))
            {
                //查询所有的数据
                sbString.Append("select Modules.ModuleID,Modules.ModuleName,Modules.ModuleURL,Modules.IsMenu,Modules.ModuleParentID,Modules.ModuleOrder from Modules");
                return SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, sbString.ToString());
            }
            else
            {
                //查询单个对象
                sbString.Append("select * from Modules where ModuleID in (@moduleId)");
                SqlParameter[] patameter = new SqlParameter[]
                {
                    new SqlParameter("@moduleId",int.Parse(id))
                };
                return SqlHelper.ExecuteDataset(CommandType.Text, sbString.ToString(), patameter);
            }
        }

        /// <summary>
        /// 根据类型ID获取全部的菜单
        /// </summary>
        /// <param name="moduleTypeID"></param>
        /// <returns></returns>
        public DataSet GetModulesByTypeId(int moduleTypeID)
        {
            sbString.Clear();
            //查询单个对象
            sbString.Append("select * from Modules where ModuleTypeID=@ModuleTypeID");
            SqlParameter[] patameter = new SqlParameter[]
                {
                    new SqlParameter("@ModuleTypeID",moduleTypeID)
                };
            return SqlHelper.ExecuteDataset(CommandType.Text, sbString.ToString(), patameter);
        }

        /// <summary>
        /// 查询分页数据(全部的数据分页)
        /// </summary>
        /// <returns>返回数据数组格式：  索引-：数据集  索引二：当前页码   索引三：显示数据量  索引四：总数据量</returns>
        public object[] GetPageDataModules(int pageIndex, int pageSize, int moduleParentID = 0, int moduleTypeID = 0, string moduleName = "")
        {
            sbString.Clear();
            sbString.Append(string.IsNullOrEmpty(moduleName) ? "" : string.Format(" and  ModuleName like '%{0}%'", moduleName));
            sbString.Append(moduleParentID == 0 ? "" : string.Format(" and ModuleParentID={0}", moduleParentID));
            sbString.Append(moduleTypeID == 0 ? "" : string.Format(" and ModuleTypeID={0}", moduleTypeID));
            SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@tblName",SqlDbType.VarChar,200){Value="Modules"},
                    new SqlParameter("@ID",SqlDbType.VarChar,150){Value="ModuleID"},
                    new SqlParameter("@fldSort",SqlDbType.VarChar,200){Value="ModuleOrder"},
                    new SqlParameter("@page",SqlDbType.Int){Value=pageIndex==0?1:pageIndex,Direction=ParameterDirection.InputOutput},
                    new SqlParameter("@pageSize",SqlDbType.Int){Value=pageSize==0?10:pageSize,Direction=ParameterDirection.InputOutput},
                    new SqlParameter("@Counts",SqlDbType.Int){Direction=ParameterDirection.Output},
                    new SqlParameter("@strCondition",SqlDbType.VarChar,300){Value=sbString.ToString()}
                };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "proc_paged_2part_selectMax", parameter);
            return new object[] { ds, parameter[3].Value, parameter[4].Value, parameter[5].Value };
        }

        /// <summary>
        /// 根据类型和父级ID查询菜单信息
        /// </summary>
        /// <param name="moduleTypeId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public DataSet GetModulesByIdAndTypeId(int moduleTypeId, int parentId)
        {
            //清空
            sbString.Clear();
            sbString.Append("select * from Modules where ModuleTypeID=@ModuleTypeID and ModuleParentID=@ModuleParentID");
            SqlParameter[] patameter = new SqlParameter[]
                {
                    new SqlParameter("@ModuleTypeID",moduleTypeId),
                    new SqlParameter("@ModuleParentID",parentId)
                };
            return SqlHelper.ExecuteDataset(CommandType.Text, sbString.ToString(), patameter);
        }

        /// <summary>
        /// 添加数据(执行的存储过程)
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="authorityStr"></param>
        /// <param name="boo"></param>
        /// <returns></returns>
        public bool InsertModules(Modules modules, string authorityStr)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@moduleParentId",modules.ModuleParentID),
                    new SqlParameter("@moduleTypeId",modules.ModuleTypeID),
                    new SqlParameter("@moduleName",modules.ModuleName),
                    new SqlParameter("@moduleOrder",modules.ModuleOrder),
                    new SqlParameter("@moduleAreas",modules.ModuleAreas),
                    new SqlParameter("@moduleController",modules.ModuleController),
                    new SqlParameter("@moduleAction",modules.ModuleAction),
                    new SqlParameter("@moduleIcon",modules.ModuleIcon),
                    new SqlParameter("@moduleDescription",modules.ModuleDescription),
                    new SqlParameter("@isMenu",modules.IsMenu),
                    new SqlParameter("@str",authorityStr),
                    new SqlParameter("@split",',')
                };
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_InsertModule", parameter);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更新数据（执行的存储过程）
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="authorityStr"></param>
        /// <returns></returns>
        public bool UpdateModules(Modules modules, string authorityStr)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@moduleId",modules.ModuleID),
                    new SqlParameter("@moduleParentId",modules.ModuleParentID),
                    new SqlParameter("@moduleTypeId",modules.ModuleTypeID),
                    new SqlParameter("@moduleName",modules.ModuleName),
                    new SqlParameter("@moduleOrder",modules.ModuleOrder),
                    new SqlParameter("@moduleAreas",modules.ModuleAreas),
                    new SqlParameter("@moduleController",modules.ModuleController),
                    new SqlParameter("@moduleAction",modules.ModuleAction),
                    new SqlParameter("@moduleIcon",modules.ModuleIcon),
                    new SqlParameter("@moduleDescription",modules.ModuleDescription),
                    new SqlParameter("@isMenu",modules.IsMenu),
                    new SqlParameter("@str",authorityStr),
                    new SqlParameter("@split",',')
                };
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_UpdateModule", parameter);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更新模块状态
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public bool UpdateModuleStatus(string moduleIds)
        {
            SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@ModuleIds",moduleIds),
                    new SqlParameter("@split",",")
                };
            try
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_UpdateModuleStatus", parameter);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 删除菜单（数据库使用的是级联删除-存储过程）
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteModules(string ids)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@table","Modules"),
                new SqlParameter("@property","ModuleId"),
                new SqlParameter("@str",ids)
            };
            try
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_SingleDelete", parameter);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检查模块名称或者标识是否重复
        /// </summary>
        /// <param name="authorityName"></param>
        /// <param name="authorityTag"></param>
        /// <returns></returns>
        public bool IsExists(string moduleName = "", string moduleTag = "")
        {
            sbString.Clear();
            sbString.Append("select 1 from where Modules");
            sbString.Append(string.Format(" where ModuleName='{0}'", moduleName));
            sbString.Append(string.Format(" and ModuleTag='{0}'", moduleTag));
            return SqlHelper.ExecuteDataset(CommandType.Text, sbString.ToString()).Tables["Authority"].Rows.Count > 0 ? true : false;
        }
    }
}