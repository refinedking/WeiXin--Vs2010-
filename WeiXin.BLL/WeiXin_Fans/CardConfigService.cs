
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
using WeiXin.Core.Transfer;
using WeiXin.BO;
using WeiXin.Model;
namespace WeiXin.BLL
{
    public class CardConfigService
    {
        CardConfigDAL CardConfigDao = new CardConfigDAL();
        #region Insert
        public int Insert(CardConfigContract card)
        {
            return CardConfigDao.Insert(card.ToPO<CardConfig>());
        }
        #endregion 
        #region Select
        /// <summary>
        /// 根据企业ID查询会员卡配置信息
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public CardConfigContract GetCardConfigByEid(int eid)
        {
            CardConfig cardConfig= CardConfigDao.GetCardConfigByEid(eid);
            if (cardConfig != null)
            {
                return cardConfig.ToBO<CardConfigContract>();
            }
            else {
                return new CardConfigContract();
            }
           
        }

        /// <summary>
        /// 根据企业ID查询会员卡配置信息
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public CardConfig  GetCardConfigModelByEid(int eid)
        {
            CardConfig cardConfig = CardConfigDao.GetCardConfigByEid(eid);

            return cardConfig;
            

        }
        #endregion

        #region Update
        public int Update(CardConfigContract card)
        {
            return CardConfigDao.Update(card.ToPO<CardConfig>());
        }
        #endregion
    }
}
