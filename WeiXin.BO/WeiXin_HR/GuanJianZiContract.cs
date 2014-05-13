
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
    /// 关键字BO
    /// </summary>
    public class GuanJianZiContract : BussinessObject
    {
        public GuanJianZiContract()
        {
        }

        #region GuanJianZiContract
        public int gjzId { get; set; }

        [Required(ErrorMessage = "错误！关键字名称不能为空")]
        [StringLength(15, ErrorMessage = "关键字名称最大为15个汉字")] 
        public string name { get; set; }
        public int typeId { get; set; }
        public int eId { get; set; }
        public string time { get; set; }
        public int isDisplay { get; set; }
         [Required(ErrorMessage = "错误！必填，至少2字说明该关键字的作用")]
        public string remark { get; set; }

        public string temp1 { get; set; }
        public string temp2 { get; set; }
        public string temp3 { get; set; }
        public string temp4 { get; set; }
        public string temp5 { get; set; }
        public string temp6 { get; set; }
        public string temp7 { get; set; }
        public string temp8 { get; set; }
        public string temp9 { get; set; }
        public string temp10 { get; set; }
        #endregion
    }
}
