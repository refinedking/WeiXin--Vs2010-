
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
    public class loginLogInfoContract : BussinessObject
    {
        private int _lId;

        public int LId
        {
            get { return _lId; }
            set { _lId = value; }
        }
        private int _userId;

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        private string _lTime;

        public string LTime
        {
            get { return _lTime; }
            set { _lTime = value; }
        }
        private string _lIP;

        public string LIP
        {
            get { return _lIP; }
            set { _lIP = value; }
        }
        private int _lState;

        public int LState
        {
            get { return _lState; }
            set { _lState = value; }
        }
        private string _lRemark = "暂无说明";

        public string LRemark
        {
            get { return _lRemark; }
            set { _lRemark = value; }
        }
    }
}
