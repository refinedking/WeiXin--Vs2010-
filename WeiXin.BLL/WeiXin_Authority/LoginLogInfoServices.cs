
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
using WeiXin.BO;
using WeiXin.Model;
using WeiXin.Core.Transfer;

namespace WeiXin.BLL
{
  public class LoginLogInfoServices
  {
      LoginLogInfoRepository loginLogInfoRepository = new LoginLogInfoRepository();
      /// <summary>
      /// 添加登录信息
      /// </summary>
      /// <param name="loginInfo"></param>
      /// <returns></returns>
      public bool InsertloginLogInfo(loginLogInfoContract loginInfoContract)
      {
          loginLogInfo loginInfo = loginInfoContract.ToPO<loginLogInfo>();
          return loginLogInfoRepository.InsertloginLogInfo(loginInfo);
      }
    }
}
