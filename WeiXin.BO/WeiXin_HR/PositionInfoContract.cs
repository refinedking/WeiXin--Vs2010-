
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
using WeiXin.Core.Transfer;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WeiXin.BO
{
    /// <summary>
    /// 用户组BO
    /// </summary>
    public class PositionInfoContract : BussinessObject
    {
        public PositionInfoContract()
        {
        }

        #region PositionInfo

        private int positionId;	//用户组编号
        public int PositionId
        {
            get { return positionId; }
            set { positionId = value; }
        }

        private string positionName;	//用户组名称
        [Required(ErrorMessage = "错误！用户组名称不能为空")]
        [StringLength(15, ErrorMessage = "用户组名称最大为15个汉字")] 
        public string PositionName
        {
            get { return positionName; }
            set { positionName = value; }
        }

       
        private int isDisplay;//是否显示
        public int IsDisplay
        {
            get { return isDisplay; }
            set { isDisplay = value; }
        }

        private string positionRemark; //用户组描述
        public string PositionRemark
        {
            get { return positionRemark; }
            set { positionRemark = value; }
        }
         
        #endregion
    }
}
