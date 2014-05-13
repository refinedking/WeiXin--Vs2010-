
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
    public class normal_classContract : BussinessObject
    {
        public normal_classContract()
        {
        }
        public int Id { get; set; }
        public int ChannelId { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public string Keywords { get; set; }
        public string Img { get; set; }
        public string Content { get; set; }
        public int SortRank { get; set; }
        public string Folder { get; set; }
        public string FilePath { get; set; }
        public string Code { get; set; }
        public int IsPost { get; set; }
        public int IsTop { get; set; }
        public int TopicNum { get; set; }
        public int TemplateId { get; set; }
        public int ContentTemp { get; set; }
        public int IsPaging { get; set; }
        public int PageSize { get; set; }
        public int IsOut { get; set; }
        public string FirstPage { get; set; }
        public string AliasPage { get; set; }
        public int ReadGroup { get; set; }
    }
}
