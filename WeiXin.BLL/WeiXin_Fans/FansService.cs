
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
using WeiXin.Model;
using System.Data;
using WeiXin.BO;
using WeiXin.Core.Transfer;
namespace WeiXin.BLL
{
    public class FansService
    {
        FansDAL FansDao = new FansDAL();
        #region Insert
        /// <summary>
        /// 用户关注时候添加用户
        /// </summary>
        /// <param name="fans"></param>
        /// <param name="uToe"></param>
        public bool InsertFansAndToEmp(Fans fans, UserToEmp uToe)
        {
            return FansDao.InsertFansAndToEmp(fans, uToe);
        }
        /// <summary>
        /// 判断用户是否存在数据库
        /// </summary>
        /// <param name="FansOpenId">OpenID</param>
        /// <returns>True 存在， False 不存在</returns>
        public bool CheckIsFans(string FansOpenId)
        {
            return FansDao.CheckIsFans(FansOpenId);
        }

        /// <summary>
        /// 判断用户是否存在企业的数据库
        /// </summary>
        /// <param name="FansOpenId"></param>
        /// <param name="empID"></param>
        /// <returns>True 存在，False 不存在</returns>
        public bool CheckIsFans(string FansOpenId, int empID)
        {
            return FansDao.CheckIsFans(FansOpenId, empID);
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
            FansDao.UpdateU2E(empid, fromusername);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="fans"></param>
        public void UpdateFansName(FansContract fans)
        {
            FansDao.UpdateFansName(fans.ToPO<Fans>());
        }
        #endregion


        #region Data
        /// <summary>
        /// 查询最近7天的粉丝增长数据(增/减)
        /// </summary>
        /// <param name="empid"></param>
        /// <returns></returns>
        public DataSet GetDataInfo(int empid, string date)
        {
            return FansDao.GetData(empid, date);
        }


        #endregion

        #region Select
        /// <summary>
        /// 查询Fans的信息 用于用户编辑信息
        /// </summary>
        /// <param name="Openid"></param>
        /// <returns></returns>
        public FansContract GetFansInfoByOpenID(string Openid)
        {
            return FansDao.GetFansInfoByOpenID(Openid).ToBO<FansContract>();
        }



        /// <summary>
        /// 查询粉丝与客户的关系
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public UserToEmp GetUserToEmpByOpenID(string OpenId)
        {
            return FansDao.GetUserToEmpByOpenID(OpenId);
        }
        #endregion

    }
}
