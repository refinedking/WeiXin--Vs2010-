
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
using System.Text;
using WeiXin.Core.Transfer;

namespace WeiXin.BO
{
    public class RoleAuthorityListContract : BussinessObject
    {
        #region Model

        private int _role_authorityid;
        private int _userid;
        private int _roleid;
        private int _moduleid;
        private int _authorityid;
        private int _IsChange;

        public int IsChange
        {
            get { return _IsChange; }
            set { _IsChange = value; }
        }

        /// <summary>
        ///
        /// </summary>
        public int Role_AuthorityID
        {
            set { _role_authorityid = value; }
            get { return _role_authorityid; }
        }

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
        public int RoleID
        {
            set { _roleid = value; }
            get { return _roleid; }
        }

        /// <summary>
        ///
        /// </summary>
        public int ModuleID
        {
            set { _moduleid = value; }
            get { return _moduleid; }
        }

        /// <summary>
        ///
        /// </summary>
        public int AuthorityID
        {
            set { _authorityid = value; }
            get { return _authorityid; }
        }

        #endregion Model
    }
}