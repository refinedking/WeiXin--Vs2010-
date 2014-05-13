
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
    public class FansContract : BussinessObject
    {
        public FansContract()
        {
        }
       
        private string fromUserName;
        /// <summary>
        /// 发送方微信号
        /// </summary>
        public string FromUserName
        {
            get { return fromUserName; }
            set { fromUserName = value; }
        }
        private string userName;
        /// <summary>
        /// 用户微信名称
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private string trueName;
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string TrueName
        {
            get { return trueName; }
            set { trueName = value; }
        }
        private string userPhone;
        /// <summary>
        /// 用户手机
        /// </summary>
        public string UserPhone
        {
            get { return userPhone; }
            set { userPhone = value; }
        }

        private string userBr;
        /// <summary>
        /// 用户的生日
        /// </summary>
        public string UserBr
        {
            get { return userBr; }
            set { userBr = value; }
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
