
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
using System.Data;

namespace WeiXin.DAL
{
    public class FansToCardDAL
    {
        WeiXinSystemDBDataContext db = new WeiXinSystemDBDataContext(SqlHelper.connectionString);
        #region Insert
        public int Insert(FansToCard ftc)
        {
            db.FansToCard.InsertOnSubmit(ftc);
            db.SubmitChanges();
            return ftc.id;
        }
        #endregion

        #region Select
        /// <summary>
        /// 根据用户iD查询用户的会员卡
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public FansToCard GetFansToCardByUserID(string userid, int eid)
        {
            return (from ftc in db.FansToCard where ftc.UserToEmp.UserID == userid select ftc).FirstOrDefault();
        }

        /// <summary>
        /// 会员卡信息分页方法
        /// </summary> 
        /// <param name="pageNo">当前页数</param>
        /// <param name="pageSize">显示数据条数</param>
        /// <param name="keyWords">查询条件</param>
        /// <returns>当前页数的数据</returns>
        public PagerList<FansToCard> GetFansToCardByPager(int pageNo, int pageSize, string keyWords)
        {
            IQueryable<FansToCard> list = (from cr in db.FansToCard
                                           where (cr.Cardid.ToString().Contains(keyWords.Trim()) || cr.UserToEmp.Fans.TrueName.Contains(keyWords.Trim())
                                         )
                                           select cr);
            int totalData = list.Count();
            return new PagerList<FansToCard>(list.OrderByDescending(o => o.id), pageNo, pageSize, totalData);
        }

        /// <summary>
        /// 会员卡信息分页方法2 查询企业自己的会员卡信息
        /// </summary> 
        /// <param name="pageNo">当前页数</param>
        /// <param name="pageSize">显示数据条数</param>
        /// <param name="keyWords">查询条件</param>
        /// <param name="eid">企业id</param>
        /// <returns>当前页数的数据</returns>
        public PagerList<FansToCard> GetFansToCardByPager(int eid, int pageNo, int pageSize, string keyWords)
        {
            IQueryable<FansToCard> list = (from cr in db.FansToCard
                                           where (cr.CardConfig.employeeInfo.Eid == eid && (cr.Cardid.ToString().Contains(keyWords.Trim()) || cr.UserToEmp.Fans.TrueName.Contains(keyWords.Trim()))
                                         )
                                           select cr);
            int totalData = list.Count();
            return new PagerList<FansToCard>(list.OrderByDescending(o => o.id), pageNo, pageSize, totalData);
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
            string sql = "";
            if (state == "0")
            {
                sql = string.Format(@"UPDATE FansToCard set CardState=1 where id in({0})", id);
            }
            else
            {
                sql = string.Format(@"UPDATE FansToCard set CardState=0 where id in({0})", id);
            }
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
        }

        /// <summary>
        ///  根据1个或多个会员卡ID查询该会员卡的状态,注：该方法仅适用于批量冻结/解冻会员卡时使用
        /// </summary>
        /// <param name="id">会员卡id</param> 
        /// <returns></returns>
        public FansToCard GetCardStateById(string userid, int eid)
        {

            return (from ftc in db.FansToCard where ftc.UserToEmp.UserID == userid && ftc.UserToEmp.EmpID == eid select ftc).FirstOrDefault();
        }
        public string GetCardStateById(string id)
        {
            string state = "";
            string sql = "";
            if (!string.IsNullOrEmpty(id))
            {
                string[] strs = id.Split('/');
                sql = string.Format(@"select CardState from FansToCard where id in(" + strs[1] + ")");
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["CardState"].ToString() == "0")
                    {
                        state = "0";
                    }
                    else
                    {
                        state = "";
                        break;
                    }
                }

                if (state != "0")
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["CardState"].ToString() == "1")
                        {
                            state = "1";
                        }
                        else
                        {
                            state = "";
                            break;
                        }
                    }
                }
            }

            return state;
        }


        /// <summary>
        /// 查询最大的卡信息
        /// </summary>
        /// <param name="cardConfigID"></param>
        /// <returns></returns>
        public FansToCard GetMaxCardID(int cardConfigID)
        {
            return (from ftc in db.FansToCard where ftc.CardConfigID == cardConfigID select ftc).OrderByDescending(s => s.id).FirstOrDefault();

        }





    }
        #endregion
}
