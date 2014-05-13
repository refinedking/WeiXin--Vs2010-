
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
    public class RolesContract : BussinessObject
    {
        #region Model

        private int _roleid;
        private string _rolename;
        private string _roledescription = "暂无说明";
        private int _status;

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
        [Required(ErrorMessage = "出错啦！角色名称不能为空")]
        [RegularExpression("^[\\u4E00-\\u9FA5\\uF900-\\uFA2D]+$", ErrorMessage = "出错啦！角色名称只能为汉字")]
        //[Remote("IsExistsRoleName", "AuthorityManager", ErrorMessage = "数据已经存在，请重新输入！")]
        public string RoleName
        {
            set { _rolename = value; }
            get { return _rolename; }
        }

        /// <summary>
        ///
        /// </summary>
        [Required(ErrorMessage = "出错啦！角色说明不能为空")]
        public string RoleDescription
        {
            set { _roledescription = value; }
            get { return _roledescription; }
        }

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        #endregion Model
    }
}