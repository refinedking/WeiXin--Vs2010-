
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
    [Serializable]
    public class normal_channelContract : BussinessObject
    {
        public normal_channelContract()
        {
        }
        public int Id { get; set; }
         [Required(ErrorMessage = "必填")]
        public string Title { get; set; }
        public int eid { get; set; }
        public string Info { get; set; }
        public string Dir { get; set; }
        public string ItemName { get; set; }
        public string ItemUnit { get; set; }
        public int TemplateId { get; set; }
        public string Type { get; set; }
        public int Enabled { get; set; }
        public int DefaultThumbs { get; set; }
        public int IsPaging { get; set; }
        public int PageSize { get; set; }
        public int IsPost { get; set; }
        public int IsNav { get; set; }
        public int IsHtml { get; set; }
        public int IsTop { get; set; }
        public int ContentTemp { get; set; }
        public string UploadPath { get; set; }
        public string UploadType { get; set; }
        public Int64 UploadSize { get; set; }
        public string LanguageCode { get; set; } 
    }
}
