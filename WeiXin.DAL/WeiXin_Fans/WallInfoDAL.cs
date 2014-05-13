
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

namespace WeiXin.DAL
{
    public class WallInfoDAL
    {
        WeiXinSystemDBDataContext db = new WeiXinSystemDBDataContext(SqlHelper.connectionString);

        #region Select

        /// <summary>
        /// 分页内容列表
        /// </summary>
        /// <param name="ccid"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public PagerList<WallInfo> GetWallInfoByPager(int eid, int pageNo, int pageSize )
        {
            db.DeferredLoadingEnabled = false;
            IQueryable<WallInfo> list = (from wi in db.WallInfo
                                     where wi.EmpId==eid select wi);
            int totalData = list.Count();
            return new PagerList<WallInfo>(list.OrderByDescending(o => o.id), pageNo, pageSize, totalData,"web");

        }
        #endregion

        #region Insert
        public int Insert(WallInfo wi)
        {
            db.WallInfo.InsertOnSubmit(wi);
            db.SubmitChanges();
            return wi.id;
        }
        #endregion
    }
}
