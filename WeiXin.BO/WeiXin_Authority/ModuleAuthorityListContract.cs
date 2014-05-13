
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
using WeiXin.Core.Transfer;

namespace WeiXin.BO
{
    public class ModuleAuthorityListContract : BussinessObject
    {
        #region Model
        private int _id;
        private int? _moduleid;
        private int _authorityid;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ModuleID
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
