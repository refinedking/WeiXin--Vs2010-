
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
  public  class normal_templateContract : BussinessObject
    {
      public normal_templateContract()
        {
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public int eid { get; set; }
        public int IsDefault { get; set; }
        public string Type { get; set; }
        public string SType { get; set; }
        public string content { get; set; }
       
    }
}
