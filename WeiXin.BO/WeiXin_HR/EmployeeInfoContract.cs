
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
using System.Web.Mvc;
using WeiXin.Core.Transfer;
using System.ComponentModel.DataAnnotations;

namespace WeiXin.BO
{
    /// <summary>
    /// 员工BO
    /// </summary>
    public class EmployeeInfoContract : BussinessObject
    {
        public EmployeeInfoContract()
        {

        }
        #region employeeInfo

        private int eId;		  //编号
        public int EId
        {
            get { return eId; }
            set { eId = value; }
        }

        private string _wxName;     //微信名称
        public string wxName
        {
            get { return _wxName; }
            set { _wxName = value; }
        }

        private string _wxUser;		   //微信帐号
        //[Required(ErrorMessage = "员工姓名不能为空!")]
        //[StringLength(10, ErrorMessage = "员工姓名最大为10个汉字!")]
        public string wxUser
        {
            get { return _wxUser; }
            set { _wxUser = value; }
        }

        private string _wxOldUser;  //原始帐号
        public string wxOldUser
        {
            get { return _wxOldUser; }
            set { _wxOldUser = value; }
        }

        private string toKen;  //微信接口TOken
        public string ToKen
        {
            get { return toKen; }
            set { toKen = value; }
        }

        private string addTime;  //添加时间

        public string AddTime
        {
            get { return addTime; }
            set { addTime = value; }
        }

        private string maxTime;    //使用该系统的到期时间
        public string MaxTime
        {
            get { return maxTime; }
            set { maxTime = value; }
        }

        private string name;	   //企业名称
        [Required(ErrorMessage = "企业名称不能为空!")] 
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int branchID;  //行业
        public int BranchID
        {
            get { return branchID; }
            set { branchID = value; }
        }

        private int _positionId;    //用户组
        public int positionId
        {
            get { return _positionId; }
            set { _positionId = value; }
        }

        private string _phone;	  //手机
        public string phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        private string tEL;  //电话 ，座机
        public string TEL
        {
            get { return tEL; }
            set { tEL = value; }
        }

        private string address;		 //地址
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private string lxrName;	    //联系人姓名
        public string LxrName
        {
            get { return lxrName; }
            set { lxrName = value; }
        }
         
        private int isDisplay;	//是否显示
        public int IsDisplay
        {
            get { return isDisplay; }
            set { isDisplay = value; }
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

        private string temp11;	     //备用1
        public string Temp11
        {
            get { return temp11; }
            set { temp11 = value; }
        }

        private string temp12;	    //备用2
        public string Temp12
        {
            get { return temp12; }
            set { temp12 = value; }
        }

        private string temp13;     //备用3
        public string Temp13
        {
            get { return temp13; }
            set { temp13 = value; }
        }

        private string temp14;	    //备用4
        public string Temp14
        {
            get { return temp14; }
            set { temp14 = value; }
        }

        private string temp15;		     //备用5
        public string Temp15
        {
            get { return temp15; }
            set { temp15 = value; }
        }
        private string temp16;    //备用6
        public string Temp16
        {
            get { return temp16; }
            set { temp16 = value; }
        }

        private string temp17;	     //备用7
        public string Temp17
        {
            get { return temp17; }
            set { temp17 = value; }
        }

        private string temp18;	    //备用8
        public string Temp18
        {
            get { return temp18; }
            set { temp18 = value; }
        }

        private string temp19;	     //备用9
        public string Temp19
        {
            get { return temp19; }
            set { temp19 = value; }
        }

        private string temp20;    //备用10
        public string Temp20
        {
            get { return temp20; }
            set { temp20 = value; }
        }

        public string BranchName { get; set; }
        #endregion
    }
}