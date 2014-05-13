
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using WeiXin.Core.Transfer;

namespace WeiXin.BO
{
    /// <summary>
    /// 部门BO
    /// </summary>
    public class BranchInfoContract : BussinessObject
    {
        public BranchInfoContract()
        {
        }

        #region BranchInfo

        private int branchID;	 //部门编号

        public int BranchID
        {
            get { return branchID; }
            set { branchID = value; }
        }

        private string branchName;		 //部门名称

        // [Remote("ValidBranchName", "BranchManage", "OA_Hrms", ErrorMessage = "该部门名称已经存在!请重新输入")]
        [Required(ErrorMessage = "错误！部门名称不能为空")]
        [StringLength(25 ,ErrorMessage="部门名称最大为25个汉字")]
        [RegularExpression("^[\u4e00-\u9fa5]{2,10}$", ErrorMessage = "部门名称只能为中文哟!")]
        public string BranchName
        {
            get { return branchName; }
            set { branchName = value; }
        }

        private int companyId;   	//公司编号

        public int CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        }

        private int pBranchId;		//父部门编号

        public int PBranchId
        {
            get { return pBranchId; }
            set { pBranchId = value; }
        }

        private string branchPrincipal; //部门负责人

        [Required(ErrorMessage = "错误！部门负责人不能为空")]
        [StringLength(10, ErrorMessage = "部门名称最大为10个汉字！")]
        public string BranchPrincipal
        {
            get { return branchPrincipal; }
            set { branchPrincipal = value; }
        }

        private string branchPhone;		 //部门电话

        [Required(ErrorMessage = "错误！负责人电话不能为空")]
        [RegularExpression("^(13|15|18)[0-9]{9}$", ErrorMessage = "电话格式错误,请先确认")]
        public string BranchPhone
        {
            get { return branchPhone; }
            set { branchPhone = value; }
        }

        private string branchFax; //部门传真
 

        [RegularExpression("^[0-9]{7,8}$", ErrorMessage = "传真格式错误,请先确认,如：88888888")]
 

 
        public string BranchFax
        {
            get { return branchFax; }
            set { branchFax = value; }
        }

        private string branchRemark;  //部门简述

        public string BranchRemark
        {
            get { return branchRemark; }
            set { branchRemark = value; }
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

        #endregion BranchInfo
    }
}