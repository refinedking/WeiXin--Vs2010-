
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
    public class AuthorityContract : BussinessObject
    {
        public AuthorityContract()
        { }

        #region Model

        private int _authorityid;
        private string _authorityname;
        private string _authoritytag;
        private string _authoritydescription = "暂无说明";
        private int _authorityorder = 1;
        private int _authoritystate;

        /// <summary>
        ///
        /// </summary>
        public int AuthorityID
        {
            set { _authorityid = value; }
            get { return _authorityid; }
        }

        /// <summary>
        ///
        /// </summary>
        [Required(ErrorMessage = "错误！功能点名称不能为空")]
        [RegularExpression("^[\\u4E00-\\u9FA5\\uF900-\\uFA2D]+$", ErrorMessage = "功能点名称只能为汉字")]
        [StringLength(30, ErrorMessage = "功能点名称太长，不利于记忆，请重新输入容易记忆的名称！")]
        public string AuthorityName
        {
            set { _authorityname = value; }
            get { return _authorityname; }
        }

        /// <summary>
        ///
        /// </summary>
        [Required(ErrorMessage = "错误！功能点标识不能为空")]
        [RegularExpression("^\\w+$", ErrorMessage = "出错啦！只能输入由数字、26个英文字母或者下划线")]
        [StringLength(50, ErrorMessage = "功能点标识过长，不利于记忆，请重新输入容易记忆的标识！")]
        public string AuthorityTag
        {
            set { _authoritytag = value; }
            get { return _authoritytag; }
        }

        [StringLength(100, ErrorMessage = "功能点说明过长，不利于记忆，请重新输入容易记忆的说明！")]
        /// <summary>
        ///
        /// </summary>

        public string AuthorityDescription
        {
            set { _authoritydescription = value; }
            get { return _authoritydescription; }
        }

        /// <summary>
        ///
        /// </summary>
        [Required(ErrorMessage = "错误！功能点排序不能为空")]
        [RegularExpression(@"^\d+$", ErrorMessage = "只能输入非负整数")]
        public int AuthorityOrder
        {
            set { _authorityorder = value; }
            get { return _authorityorder; }
        }

        /// <summary>
        ///
        /// </summary>
        public int AuthorityState
        {
            set { _authoritystate = value; }
            get { return _authoritystate; }
        }

        #endregion Model
    }
}