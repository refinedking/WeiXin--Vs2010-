
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
namespace WeiXin.BLL
{
    public class WallInfoService
    {
        WallInfoDAL WallDao = new WallInfoDAL();
        #region Insert
        public int Insert(WallInfo wi)
        {
            return WallDao.Insert(wi);
        }
        #endregion
        #region Select

        /// <summary>
        /// 分页内容列表
        /// </summary>
        /// <param name="ccid"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public PagerList<WallInfo> GetWallInfoByPager(int eid, int pageNo, int pageSize)
        {

            return WallDao.GetWallInfoByPager(eid, pageNo, pageSize);

        }
        #endregion
    }
}
