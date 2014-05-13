
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
using WeiXin.Model;
using WeiXin.Core.DBUtility;
using System.IO;
using System.Data;

namespace WeiXin.DAL
{
    public class FansDAL
    {
        WeiXinSystemDBDataContext db = new WeiXinSystemDBDataContext(SqlHelper.connectionString);
        #region Insert
        /// <summary>
        /// 用户关注时候添加用户
        /// </summary>
        /// <param name="fans"></param>
        /// <param name="uToe"></param>
        public bool InsertFansAndToEmp(Fans fans, UserToEmp uToe)
        {
            try
            {  //检查用户是否已经存在
                if (CheckIsFans(fans.FromUserName) && CheckIsFans(fans.FromUserName, int.Parse(uToe.EmpID.ToString())))
                {
                    //存在，则一定是用户之前取消了关注，再次关注
                    //修改用户的状态即可
                    string sql = "update UserToEmp set SubscribeState =1 where UserID='" + fans.FromUserName + "' and EmpID=" + uToe.EmpID;
                    SqlHelper.ExecuteNonQuery(System.Data.CommandType.Text, sql);
                }
                else
                {
                    //2张表都没有数据，在2个表里面添加数据
                    db.Fans.InsertOnSubmit(fans);
                    db.UserToEmp.InsertOnSubmit(uToe);
                    db.SubmitChanges();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// 判断用户是否存在数据库
        /// </summary>
        /// <param name="FansOpenId">OpenID</param>
        /// <returns>True 存在， False 不存在</returns>
        public bool CheckIsFans(string FansOpenId)
        {
            Fans F = (from f in db.Fans where f.FromUserName == FansOpenId select f).FirstOrDefault();
            if (F != null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 判断用户是否存在企业的数据库
        /// </summary>
        /// <param name="FansOpenId"></param>
        /// <param name="empID"></param>
        /// <returns>True 存在，False 不存在</returns>
        public bool CheckIsFans(string FansOpenId, int empID)
        {
            UserToEmp U2E = (from u2e in db.UserToEmp where u2e.UserID == FansOpenId && u2e.EmpID == empID select u2e).FirstOrDefault();
            if (U2E != null)
                return true;
            else
                return false;
        }
        #endregion

        #region Update
        /// <summary>
        /// 修改客户的状态
        /// </summary>
        /// <param name="empid">公众微信的ID</param>
        /// <param name="fromusername">当前的用户的Openid</param>
        public void UpdateU2E(string empid, string fromusername)
        {
            string sql = "update UserToEmp set SubscribeState=0,UnsubscribeDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where UserID='" + fromusername + "' and EmpID=" + empid;
            SqlHelper.ExecuteNonQuery(System.Data.CommandType.Text, sql);

        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="fans"></param>
        public void UpdateFansName(Fans fans)
        {
            string sql = "update Fans set TrueName='" + fans.TrueName + "' ,userBr='" + fans.UserBr + "',temp1='" + fans.Temp1 + "' ,UserPhone='"+fans.UserPhone+"' where FromUserName='" + fans.FromUserName + "'"; SqlHelper.ExecuteNonQuery(System.Data.CommandType.Text, sql);
        }
        #endregion

        #region Data
        /// <summary>
        /// 查询最近1天的粉丝增长数据( 增.减)
        /// </summary>
        /// <param name="empid"></param>
        /// <returns></returns>
        public DataSet GetData(int empid,string date)
        {
            string sql = @"select   convert(varchar(10),SubscribeDate,120) date,COUNT(*) as num from UserToEmp where SubscribeState=1 and convert(varchar(10),SubscribeDate,120)='" + date + "'  and EmpID=" + empid + " group by  convert(varchar(10),SubscribeDate,120) select   convert(varchar(10),SubscribeDate,120) date,COUNT(*) as num from UserToEmp where SubscribeState=0 and convert(varchar(10),SubscribeDate,120)='" + date + "'  and EmpID=" + empid + " group by  convert(varchar(10),SubscribeDate,120)  select  convert(varchar(10),Date,120) data,COUNT(*) as num from FansInteraction  where empid=" + empid + " and convert(varchar(10),Date,120)='" + date + "' group by  convert(varchar(10),Date,120)";
       return     SqlHelper.ExecuteDataset(CommandType.Text, sql);

        }
        #endregion

        #region Select
        /// <summary>
        /// 查询Fans的信息
        /// </summary>
        /// <param name="Openid"></param>
        /// <returns></returns>
        public Fans GetFansInfoByOpenID(string Openid)
        { 
            return (from fans in db.Fans where fans.FromUserName==Openid select fans).FirstOrDefault();
        }

        /// <summary>
        /// 查询粉丝与客户的关系
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public UserToEmp GetUserToEmpByOpenID(string OpenId)
        { 
            return (from u in db.UserToEmp where u.UserID==OpenId select u).FirstOrDefault();
        }
        #endregion

    }
}
