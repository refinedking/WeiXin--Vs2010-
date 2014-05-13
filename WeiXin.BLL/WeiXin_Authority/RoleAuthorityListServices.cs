
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
using WeiXin.DAL;
using System.Data;
using WeiXin.BO;
using WeiXin.Core;
using WeiXin.Model;

using WeiXin.Core.Transfer;
namespace WeiXin.BLL
{
  public  class RoleAuthorityListServices
  {
      RoleAuthorityListRepository roleAuthorityListRepository = new RoleAuthorityListRepository();
      /// <summary>
      /// 获取用户拥有的菜单和菜单的功能点
      /// </summary>
      /// <param name="userId">根据用户ID</param>
      /// <returns>返回DataSet(包含ModuleID,AuthorityIDS)</returns>
      public DataSet GetRoleAuthority(int userId)
      {
          return roleAuthorityListRepository.GetRoleAuthority(userId);
      }
      /// <summary>
      /// 根据菜单ID与角色ID(或者用户ID)查询当前角色拥有的权限
      /// </summary>
      /// <param name="moduleId"></param>
      /// <param name="roleId"></param>
      /// <returns></returns>
      public List<RoleAuthorityListContract> GetRoleAuthority(int moduleId, string roleId = "", string userId = "")
      {
          DataSet ds = null;
          if (string.IsNullOrEmpty(userId))
          {
              ds = roleAuthorityListRepository.GetRoleAuthority(moduleId: moduleId, roleId: roleId);
          }
          else
          {
              ds = roleAuthorityListRepository.GetRoleAuthority(moduleId: moduleId, userId: userId);
          }
          return DataConvert.DataSetToIList<RoleAuthorityList>(ds, 0).Select(r => r.ToBO<RoleAuthorityListContract>()).ToList();
      }
      /// <summary>
      /// 更新角色或者用户权限
      /// </summary>
      /// <param name="role_Authority"></param>
      /// <param name="roleId"></param>
      /// <param name="modulesType"></param>
      /// <returns></returns>
      public bool UpdateRoleAuthority(string role_Authority, int moduleTypeID, string roleId = "", string userId = "")
      {
          if (string.IsNullOrEmpty(userId))
          {
              return roleAuthorityListRepository.UpdateRoleAuthority(role_Authority: role_Authority, roleId: roleId, moduleTypeID: moduleTypeID);
          }
          return roleAuthorityListRepository.UpdateRoleAuthority(role_Authority: role_Authority, userId: userId, moduleTypeID: moduleTypeID);
      }

  }
}
