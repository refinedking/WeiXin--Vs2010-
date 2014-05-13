
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
using WeiXin.Core.Transfer;

namespace WeiXin.BO
{
    [Serializable]
    public class ModulesContract : BussinessObject
    {
        public ModulesContract()
        { }

        #region Model

        private int _moduleid;
        private int _moduleparentid;
        private int _moduletypeid;
        private string _modulename;
        private int _moduleorder = 1;
        private string _moduleAreas;
        private string _moduleController;
        private string _moduleAction;
        private string _moduleicon;
        private string _moduledescription = "暂无说明";
        private int _ismenu;

        [Required(ErrorMessage = "出错啦！模块域不能为空")]
        [RegularExpression(@"^\w+$", ErrorMessage = "只能输入字母和数字")]
        [StringLength(200, ErrorMessage = "菜单域过长，请输入合适、容易记忆的菜单域！")]
        public string ModuleAreas
        {
            get { return _moduleAreas; }
            set { _moduleAreas = value; }
        }

        [RegularExpression(@"^[0-9a-zA-Z]+$", ErrorMessage = "只能输入字母和数字")]
        [StringLength(200, ErrorMessage = "菜单控制器过长，请输入合适、容易记忆的菜单控制器！")]
        public string ModuleController
        {
            get { return _moduleController; }
            set { _moduleController = value; }
        }

        //[RegularExpression(@"^[0-9a-zA-Z]+$", ErrorMessage = "只能输入字母和数字")]
        [StringLength(200, ErrorMessage = "菜单名称过长，请输入合适、容易记忆的菜单名称！")]
        public string ModuleAction
        {
            get { return _moduleAction; }
            set { _moduleAction = value; }
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
        [Required(ErrorMessage = "出错啦！父级ID不能为空")]
        [RegularExpression(@"^\d+$", ErrorMessage = "只能输入非负整数")]
        public int ModuleParentID
        {
            set { _moduleparentid = value; }
            get { return _moduleparentid; }
        }

        /// <summary>
        ///
        /// </summary>
        [Required(ErrorMessage = "出错啦！菜单所属系统不能为空")]
        [RegularExpression(@"^\d+$", ErrorMessage = "只能输入非负整数")]
        public int ModuleTypeID
        {
            set { _moduletypeid = value; }
            get { return _moduletypeid; }
        }

        /// <summary>
        ///
        /// </summary>
        [Required(ErrorMessage = "出错啦！菜单名称不能为空")]
        [RegularExpression("^[\\u4E00-\\u9FA5\\uF900-\\uFA2D]+$", ErrorMessage = "出错啦！菜单名称只能为汉字")]
        [StringLength(30, ErrorMessage = "此菜单名称不容易记忆和理解，请输入合适、容易记忆的菜单名称！")]
        public string ModuleName
        {
            set { _modulename = value; }
            get { return _modulename; }
        }

        /// <summary>
        ///
        /// </summary>
        [Required(ErrorMessage = "出错啦！菜单排序不能为空")]
        [RegularExpression(@"^\d+$", ErrorMessage = "只能输入非负整数")]
        public int ModuleOrder
        {
            set { _moduleorder = value; }
            get { return _moduleorder; }
        }

        /// <summary>
        ///
        /// </summary>
        [StringLength(50, ErrorMessage = "菜单控制器过长，请输入合适、容易记忆的菜单控制器！")]
        public string ModuleIcon
        {
            set { _moduleicon = value; }
            get { return _moduleicon; }
        }

        /// <summary>
        ///
        /// </summary>
        [Required(ErrorMessage = "出错啦！模块说明不能为空")]
        public string ModuleDescription
        {
            set { _moduledescription = value; }
            get { return _moduledescription; }
        }

        /// <summary>
        ///
        /// </summary>
        [Required(ErrorMessage = "出错啦！显示菜单不能为空")]
        [RegularExpression("^[01]$", ErrorMessage = "出错啦！只能是0和1")]
        public int IsMenu
        {
            set { _ismenu = value; }
            get { return _ismenu; }
        }

        #endregion Model
    }
}