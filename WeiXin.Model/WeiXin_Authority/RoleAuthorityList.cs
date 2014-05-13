
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

namespace WeiXin.Model
{
    /// <summary>
    /// RoleAuthorityList:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RoleAuthorityList : PersistenceObject
    {
        //判断用户是否拥有修改当前菜单功能点的权限
        private int _IsChange;

        public int IsChange
        {
            get { return _IsChange; }
            set { _IsChange = value; }
        }
    }
}