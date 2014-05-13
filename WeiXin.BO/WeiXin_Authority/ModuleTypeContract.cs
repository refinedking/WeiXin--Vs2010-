
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
using WeiXin.Core.Transfer;

namespace WeiXin.BO
{
    public class ModuleTypeContract : BussinessObject
    {
        #region Model

        private int _moduletypeid;
        private string _moduletypename;
        private int _moduletypeorder = 1;
        private string _moduletypeEName;
        private string _moduletypeImg;
        private int _moduletypeStatus = 0;

        public int ModuletypeStatus
        {
            get { return _moduletypeStatus; }
            set { _moduletypeStatus = value; }
        }

        public string ModuletypeImg
        {
            get { return _moduletypeImg; }
            set { _moduletypeImg = value; }
        }

        public string ModuletypeEName
        {
            get { return _moduletypeEName; }
            set { _moduletypeEName = value; }
        }

        private string _moduletypedescription = "暂无说明";

        /// <summary>
        ///
        /// </summary>
        public int ModuleTypeID
        {
            set { _moduletypeid = value; }
            get { return _moduletypeid; }
        }

        /// <summary>
        ///
        /// </summary>
        [Required(ErrorMessage = "出错啦！系统名称不能为空")]
        [RegularExpression("^[\\u4E00-\\u9FA5\\uF900-\\uFA2D]+$", ErrorMessage = "出错啦！系统名称只能为汉字")]
        public string ModuleTypeName
        {
            set { _moduletypename = value; }
            get { return _moduletypename; }
        }

        /// <summary>
        ///
        /// </summary>
        [Required(ErrorMessage = "出错啦！系统排序不能为空")]
        [RegularExpression(@"^\d+$", ErrorMessage = "只能输入非负整数")]
        public int ModuleTypeOrder
        {
            set { _moduletypeorder = value; }
            get { return _moduletypeorder; }
        }

        /// <summary>
        ///
        /// </summary>
        [Required(ErrorMessage = "出错啦！系统说明不能为空")]
        public string ModuleTypeDescription
        {
            set { _moduletypedescription = value; }
            get { return _moduletypedescription; }
        }

        #endregion Model
    }
}