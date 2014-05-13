
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

namespace WeiXin.BO
{
    [Serializable]
    public class module_productContract : BussinessObject
    {
       public module_productContract() { }
        public int Id { get; set; }
        public int ChannelId { get; set; }
        public int ClassId { get; set; }
        public string Title { get; set; }
        public string TColor { get; set; }
        public DateTime AddDate { get; set; }
        public string Summary { get; set; }
        public string Editor { get; set; }
        public string Author { get; set; }
        public string Tags { get; set; }
        public int ViewNum { get; set; }
        public int IsPass { get; set; }
        public int IsImg { get; set; }
        public string Img { get; set; }
        public int IsTop { get; set; }
        public int IsFocus { get; set; }
        public int IsHead { get; set; }
        public int UserId { get; set; }
        public int ReadGroup { get; set; }
        public string SourceFrom { get; set; }
        public string Content { get; set; }
        public string FirstPage { get; set; }
        public string AliasPage { get; set; }
        public decimal Price0 { get; set; }
        public decimal Points { get; set; } 
    }
}
