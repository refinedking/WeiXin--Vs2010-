using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeiXin.DAL;
using WeiXin.Model;

namespace WeiXin.BLL
{
    public class FansToCardService
    {
        FansToCardDAL FTCDao = new FansToCardDAL();

        #region Insert
        public int Insert(FansToCard ftc)
        {

            return FTCDao.Insert(ftc);
        }
        #endregion

        #region Select
        /// <summary>
        /// 根据用户iD查询用户的会员卡
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public FansToCard GetFansToCardByUserID(string userid,int eid)
        {
            return FTCDao.GetFansToCardByUserID(userid,eid);
        }
 


        /// <summary>
        /// 查询最大的卡信息
        /// </summary>
        /// <param name="cardConfigID"></param>
        /// <returns></returns>
        public FansToCard GetMaxCardID(int cardConfigID)
        {
            return FTCDao.GetMaxCardID(cardConfigID);

        }
 

        /// <summary>
        /// 关键字信息分页方法
        /// </summary> 
        /// <param name="pageNo">当前页数</param>
        /// <param name="pageSize">显示数据条数</param>
        /// <param name="keyWords">查询条件</param>
        /// <returns>当前页数的数据</returns>
        public PagerList<FansToCard> GetFansToCardByPager(int pageNo, int pageSize, string keyWords)
        {
            return FTCDao.GetFansToCardByPager(pageNo, pageSize, keyWords);
        }

        /// <summary>
        /// 关键字信息分页方法2 查询企业自己的关键字
        /// </summary> 
        /// <param name="pageNo">当前页数</param>
        /// <param name="pageSize">显示数据条数</param>
        /// <param name="keyWords">查询条件</param>
        /// <returns>当前页数的数据</returns>
        public PagerList<FansToCard> GetFansToCardByPager(int eid, int pageNo, int pageSize, string keyWords)
        {
            return FTCDao.GetFansToCardByPager(eid, pageNo, pageSize, keyWords);
        }
 
        #endregion

        #region update  

        /// <summary>
        ///  根据id冻结会员卡
        /// </summary>
        /// <param name="id">会员卡id</param>
        /// <param name="state">会员卡状态</param>
        /// <returns></returns>
        public int DeleteFansToCard(string id, string state)
        {
            return FTCDao.DeleteFansToCard(id, state);
        }

        /// <summary>
        ///  根据1个或多个会员卡ID查询该会员卡的状态,注：该方法仅适用于批量冻结/解冻会员卡时使用
        /// </summary>
        /// <param name="id">会员卡id</param> 
        /// <returns></returns>
        public string GetCardStateById(string id)
        {
            return FTCDao.GetCardStateById(id);
        }

        #endregion
    }
}
