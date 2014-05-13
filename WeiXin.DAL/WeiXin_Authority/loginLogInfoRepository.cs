
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
// 功能描述：用户登录信息数据访问类
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
    public class LoginLogInfoRepository
    {
        /// <summary>
        /// 添加登录信息
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public bool InsertloginLogInfo(loginLogInfo loginInfo)
        {
            StringBuilder sbString = new StringBuilder();
            sbString.Append("insert into loginLogInfo(userId,lTime,lIP,lState,lRemark) values(@userId,@lTime,@lIP,@IState,@IRemark)");
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@userId",SqlDbType.Int){Value=loginInfo.userId},
                new SqlParameter("@lTime",SqlDbType.VarChar,20){Value=loginInfo.lTime},
                new SqlParameter("@lIP",SqlDbType.VarChar,20){Value=loginInfo.lIP},
                new SqlParameter("@IState",SqlDbType.Int){Value=loginInfo.lState},
                new SqlParameter("@IRemark",SqlDbType.VarChar,200){Value=loginInfo.lRemark}
            };
            try
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, sbString.ToString(), parameter);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}