
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
using WeiXin.BO;
using System.Data;
using WeiXin.Core;
using WeiXin.Model;
using WeiXin.DAL;

using WeiXin.Core.Transfer;
namespace WeiXin.BLL
{
    public class RolesServices
    {
        RolesRepository rolesRepository = new RolesRepository();

        /// <summary>
        /// 查询全部角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<RolesContract> GetRoles()
        {
            DataSet ds = rolesRepository.GetRoles();
            return DataConvert.DataSetToIList<Roles>(ds, "Roles").Select(r => r.ToBO<RolesContract>()).ToList();
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PageHelper<RolesContract> GetPageData(int pageIndex, int pageSize, string roleName = "")
        {
            object[] obj = rolesRepository.GetPageDataRoles(pageIndex, pageSize, roleName);
            List<RolesContract> list = DataConvert.DataSetToIList<RolesContract>((DataSet)obj[0], 0).ToList();
            return new PageHelper<RolesContract>(list, (int)obj[1], (int)obj[2], (int)obj[3]);
        }

        /// <summary>
        /// 根据角色ID查询单个角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RolesContract GetRoleEntity(string id)
        {
            DataSet ds = rolesRepository.GetRoles(id);
            return DataConvert.DataSetToEntity<Roles>(ds, 0).ToBO<RolesContract>();
        }

        /// <summary>
        /// 添加角色信息
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public string AddRole(RolesContract rolesContract)
        {
            Roles roles = rolesContract.ToPO<Roles>();
            return rolesRepository.AddRole(roles: roles);
        }

        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool UpdateRole(RolesContract rolesContract)
        {
            Roles roles = rolesContract.ToPO<Roles>();
            return rolesRepository.UpdateRole(roles: roles);
        }

        /// <summary>
        /// 更新角色状态
        /// </summary>
        /// <param name="roleIds">角色ID  比如 1，2，3 或者 1</param>
        /// <returns></returns>
        public bool UpdateRoleStatus(string roleIds)
        {
            return rolesRepository.UpdateRoleStatus(roleIds);
        }

        /// <summary>
        /// 根据角色ID删除角色信息（级联删除，并删除当前角色拥有的页面功能权限）
        /// </summary>
        /// <param name="roleIDs"></param>
        /// <returns></returns>
        public bool DeleteRole(string roleIDs)
        {
            return rolesRepository.DeleteRole(roleIDs: roleIDs);
        }

        /// <summary>
        /// 检查角色名称是否存在
        /// </summary>
        /// <param name="authorityName"></param>
        /// <param name="authorityTag"></param>
        /// <returns></returns>
        public bool IsExists(string roleName)
        {
            return rolesRepository.IsExists(roleName);
        }
    }
}