
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
using WeiXin.DAL;
using WeiXin.BO;
using WeiXin.Core;
using System.Data;
using WeiXin.Model;
using WeiXin.Core.Transfer;
namespace WeiXin.BLL
{
    public class ModuleAuthorityListServices
    {
        ModuleAuthorityListRepository moduleAuthorityListRepository = new ModuleAuthorityListRepository();

        /// <summary>
        /// 根据菜单ID查询当前菜单拥有的页面功能
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public List<ModuleAuthorityListContract> GetAuthorityByModuleID(int moduleId)
        {
            DataSet ds = moduleAuthorityListRepository.GetAuthorityByModuleID(moduleId);
            return DataConvert.DataSetToIList<ModuleAuthorityList>(ds, "ModuleAuthorityList").Select(m => m.ToBO<ModuleAuthorityListContract>()).ToList();
        }
    }
}