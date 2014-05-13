
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

namespace WeiXin.BO
{
    public class CardConfigContract : BussinessObject
    {
        public CardConfigContract()
        {
        }
        public int id { get; set; }
        /// <summary>
        /// 会员卡归属企业
        /// </summary>
        public int Eid { get; set; }
        /// <summary>
        /// 卡名
        /// </summary>
        [Required(ErrorMessage = "卡名必填")]
        public string CardName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [Required(ErrorMessage = "地址必填")]
        public string CardAdd { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [Required(ErrorMessage = "联系电话必填")] 
        public string CardTel { get; set; }
        /// <summary>
        /// 会员是否需要审核
        /// </summary>
        public int CardState { get; set; }
        /// <summary>
        /// 最小卡号
        /// </summary>
        [Required(ErrorMessage = "最小卡号必填")] 
        public int MinCardID { get; set; }
        /// <summary>
        /// 会员卡图片
        /// </summary>
        public string CardImg { get; set; }
        public string Temp1 { get; set; }
        public string Temp2 { get; set; }
        public string Temp3 { get; set; }
        public string Temp4 { get; set; }
        public string Temp5 { get; set; }
    }
}
