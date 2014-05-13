
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
    public class CardConfigDAL
    {
        WeiXinSystemDBDataContext db = new WeiXinSystemDBDataContext(SqlHelper.connectionString);

        #region Insert
        public int Insert(CardConfig card)
        {
            db.CardConfig.InsertOnSubmit(card);
            db.SubmitChanges();
            return card.id;
        }
        #endregion

        #region Select
        /// <summary>
        /// 根据企业ID查询会员卡配置信息
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public CardConfig GetCardConfigByEid(int eid)
        {
            return (from card in db.CardConfig where card.Eid == eid select card).FirstOrDefault();
        }
        #endregion

        #region Update
        public int Update(CardConfig cc)
        {
            string sql = string.Format(@"update CardConfig set cardName='{0}',cardadd='{1}',cardtel='{2}',cardstate={3},cardimg='{4}',mincardid={5} where id={6}", cc.CardName, cc.CardAdd, cc.CardTel, cc.CardState, cc.CardImg, cc.MinCardID, cc.id);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
        }
        #endregion
    }
}
