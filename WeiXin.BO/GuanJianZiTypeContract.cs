
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
using WeiXin.Core.Transfer;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WeiXin.BO
{ 
    /// <summary>
    /// 关键字类型BO
    /// </summary>
    public class GuanJianZiTypeContract : BussinessObject
    {
        public GuanJianZiTypeContract()
        {
        }

        #region GuanJianZiTypeContract
        public int id { get; set; } 
        public string name { get; set; } 
        public string remark { get; set; } 
        public string temp1 { get; set; }
        public string temp2 { get; set; }
        public string temp3 { get; set; }
        public string temp4 { get; set; }
        public string temp5 { get; set; } 
        #endregion
    }
}
