
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
    public class UserToEmpContract : BussinessObject
    {
        public UserToEmpContract()
        {
        }
        private int _id;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string userID;
        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        private int empID;
        public int EmpID
        {
            get { return empID; }
            set { empID = value; }
        }        private int subscribeState;
        /// <summary>
        /// 用户关注状态，0关注，1取消关注
        /// </summary>
        public int SubscribeState
        {
            get { return subscribeState; }
            set { subscribeState = value; }
        }  private int subscribeDate;
        /// <summary>
        /// 关注时间
        /// </summary>
        public int SubscribeDate
        {
            get { return subscribeDate; }
            set { subscribeDate = value; }
        }  private int unsubscribeDate;
        /// <summary>
        /// 取消关注时间
        /// </summary>
        public int UnsubscribeDate
        {
            get { return unsubscribeDate; }
            set { unsubscribeDate = value; }
        }
        private string temp1;	     //备用1
        public string Temp1
        {
            get { return temp1; }
            set { temp1 = value; }
        }

        private string temp2;	    //备用2
        public string Temp2
        {
            get { return temp2; }
            set { temp2 = value; }
        }

        private string temp3;     //备用3
        public string Temp3
        {
            get { return temp3; }
            set { temp3 = value; }
        }

        private string temp4;	    //备用4
        public string Temp4
        {
            get { return temp4; }
            set { temp4 = value; }
        }

        private string temp5;		     //备用5
        public string Temp5
        {
            get { return temp5; }
            set { temp5 = value; }
        }
        private string temp6;    //备用6
        public string Temp6
        {
            get { return temp6; }
            set { temp6 = value; }
        }

        private string temp7;	     //备用7
        public string Temp7
        {
            get { return temp7; }
            set { temp7 = value; }
        }

        private string temp8;	    //备用8
        public string Temp8
        {
            get { return temp8; }
            set { temp8 = value; }
        }

        private string temp9;	     //备用9
        public string Temp9
        {
            get { return temp9; }
            set { temp9 = value; }
        }

        private string temp10;    //备用10
        public string Temp10
        {
            get { return temp10; }
            set { temp10 = value; }
        }

    }
}
