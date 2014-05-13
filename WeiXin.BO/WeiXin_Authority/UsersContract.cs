
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
    [Serializable]
    public class UsersContract : BussinessObject
    {
        #region Model

        private int _userid;
        private int _employeeid;
        private int _roleid;
        private string _username;
        private string _password;
        private DateTime _createtime = DateTime.Now;
        private int _status = 0;

        #region 相关属性

        
        public int EId { get; set; }
        private string _positionName;

        public string positionName
        {
            get { return _positionName; }
            set { _positionName = value; }
        }
        private string _wxOldUser;

        public string wxOldUser
        {
            get { return _wxOldUser; }
            set { _wxOldUser = value; }
        }
        private string _wxName;

        public string wxName
        {
            get { return _wxName; }
            set { _wxName = value; }
        }

        private string _branchName;

        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }

        #endregion 相关属性

        /// <summary>
        ///
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }

        /// <summary>
        ///
        /// </summary>
        //[RegularExpression(@"^\d+$", ErrorMessage = "请选择员工")]
        public int EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }

        /// <summary>
        ///
        /// </summary>
        public int RoleID
        {
            set { _roleid = value; }
            get { return _roleid; }
        }

        /// <summary>
        ///
        /// </summary>
        [RegularExpression("^[a-z0-9]{1,18}$", ErrorMessage = "出错啦！ 用户名只能是1到18位数字和字母")]
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }

        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(20, ErrorMessage = "出错啦！ 密码不能超过20位")]
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }

        /// <summary>
        ///
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }

        /// <summary>
        ///
        /// </summary>
        [RegularExpression("^[01]$", ErrorMessage = "出错啦！只能是0和1")]
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }

        #endregion Model
    }
}