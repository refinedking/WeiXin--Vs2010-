
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
   public class FansInteractionService
    {
       FansInteractionDAL FIDao = new FansInteractionDAL();

       #region Insert
       /// <summary>
       /// 添加互动内容
       /// </summary>
       /// <param name="fi"></param>
       public void InsertInteraction(FansInteraction fi)
       {
           FIDao.InsertInteraction(fi);
       }
       #endregion
    }
}
