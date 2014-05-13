
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
using System.Data;
using WeiXin.BO;
using WeiXin.Core;
using WeiXin.DAL;
using WeiXin.Model;
using WeiXin.Core.Transfer;
namespace WeiXin.BLL
{
  public  class UsersServices
    {
      UsersRepository usersRepository = new UsersRepository();

      /// <summary>
      /// 查询用户信息
      /// </summary>
      /// <param name="userId"></param>
      /// <returns></returns>
      public List<UsersContract> GetAllUsers(string userId = "")
      {
          DataSet ds = usersRepository.GetUsers();
          return DataConvert.DataSetToIList<Users>(ds, "Users").Select(u => u.ToBO<UsersContract>()).ToList();
      }

      /// <summary>
      /// 获取单个用户信息（用于详细信息）
      /// </summary>
      /// <param name="usersId"></param>
      /// <returns></returns>
      public UsersContract GetUserEntity(string usersId)
      {
          DataSet ds = usersRepository.GetUsers(userId: usersId);
          return DataConvert.DataSetToEntity<Users>(ds, 0).ToBO<UsersContract>();
      }

      /// <summary>
      /// 获取单个用户信息（用于登录）
      /// </summary>
      /// <param name="userLogin"></param>
      /// <param name="userPwd"></param>
      /// <returns></returns>
      public UsersContract GetUserEntity(string userLogin, string userPwd)
      {
          DataSet ds = usersRepository.GetUsers(userLogin: userLogin, userPwd: userPwd);
          if (ds.Tables[0].Rows.Count > 0)
          {
              return DataConvert.DataSetToEntity<UsersContract>(ds, 0);
          }
          else
          {
              return null;
          }
      }

      /// <summary>
      /// 获得分页数据
      /// </summary>
      /// <param name="pageIndex"></param>
      /// <param name="pageSize"></param>
      /// <returns></returns>
      public PageHelper<UsersContract> GetPageData(int pageIndex, int pageSize, string userName = "")
      {
          object[] obj = usersRepository.GetPageDataRoles(pageIndex, pageSize, userName: userName);
          List<UsersContract> list = DataConvert.DataSetToIList<UsersContract>((DataSet)obj[0], 0).ToList();
          return new PageHelper<UsersContract>(list, (int)obj[1], (int)obj[2], (int)obj[3]);
      }

      /// <summary>
      /// 添加用户
      /// </summary>
      /// <param name="users"></param>
      /// <returns></returns>
      public DataSet InsertUsers(UsersContract usersContract)
      {
          Users users = usersContract.ToPO<Users>();
          return usersRepository.InsertUsers(users: users);
      }

      /// <summary>
      /// 更新用户
      /// </summary>
      /// <param name="users"></param>
      /// <returns></returns>
      public bool UpdateUsers(UsersContract usersContract)
      {
          Users users = usersContract.ToPO<Users>();
          return usersRepository.UpdateUsers(users: users);
      }

      /// <summary>
      /// 重置用户密码
      /// </summary>
      /// <param name="users"></param>
      /// <returns></returns>
      public bool UpdateUsersPassword(string userIds)
      {
          return usersRepository.UpdateUsersPassword(userIds);
      }

      /// <summary>
      /// 更新用户状态
      /// </summary>
      /// <param name="userIds"></param>
      /// <returns></returns>
      public bool UpdateUsersStatus(string userIds)
      {
          return usersRepository.UpdateUsersStatus(userIds);
      }

      /// <summary>
      /// 删除用户
      /// </summary>
      /// <param name="userId"></param>
      /// <returns></returns>
      public bool DeleteUsers(string userIds)
      {
          return usersRepository.DeleteUsers(userIds: userIds);
      }

      /// <summary>
      /// 检查用户名帐号不能重复
      /// </summary>
      /// <param name="authorityName"></param>
      /// <param name="authorityTag"></param>
      /// <returns></returns>
      public bool IsExists(string userId, string userName)
      {
          return usersRepository.IsExists(userId, userName);
      }

      /// <summary>
      /// 根据帐号和新密码修改密码
      /// </summary>
      /// <param name="userName"></param>
      /// <param name="pwd"></param>
      /// <returns></returns>
      public bool UpdatePwd(string userName, string pwd)
      {
          return usersRepository.UpdatePwd(userName, WeiXin.Core.SecurityEncryption.DESEncrypt(pwd));
      }
    }
}