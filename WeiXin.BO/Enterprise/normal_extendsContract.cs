
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
    public class normal_extendsContract : BussinessObject
    {
        public normal_extendsContract()
        {
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Info { get; set; }
        public int Type { get; set; }
        public int pId { get; set; }
        public string BaseTable { get; set; }
        public int Enabled { get; set; }
        public int Locked { get; set; } 
    }
}
