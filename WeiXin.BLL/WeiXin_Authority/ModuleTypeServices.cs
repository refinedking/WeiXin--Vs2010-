
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
using System.Data;
using WeiXin.BO;
using WeiXin.Core;
using WeiXin.Model;
using WeiXin.Core.Transfer;
using System.Xml;

namespace WeiXin.BLL
{
    public class ModuleTypeServices
    {
        ModuleTypeRepository moduleTypeRepository = new ModuleTypeRepository();

        /// <summary>
        /// 根据用户ID和角色ID查询当前登录用户拥有的菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public DataSet GetModuleTyepIdByUser(int userId, int roleId)
        {
            return moduleTypeRepository.GetModuleTyepIdByUser(userId, roleId);
        }

        /// <summary>
        /// 获取所有菜单类型
        /// </summary>
        /// <returns></returns>
        public List<ModuleTypeContract> GetAllModuleType()
        {
            DataSet ds = moduleTypeRepository.GetAllModuleType();
            return DataConvert.DataSetToIList<ModuleType>(ds, 0).Select(m => m.ToBO<ModuleTypeContract>()).ToList();
        }

        /// <summary>
        /// 获取所有的菜单（用于Index页面切换系统）
        /// </summary>
        /// <returns></returns>
        public string GetModuleTypeMenu(string filePath, string moduleTypeId)
        {
            //系统列表
            StringBuilder sbString = new StringBuilder();
            //判断XML文件是否存在 不存在抛出异常
            if (!System.IO.File.Exists(filePath))
            {
                throw new Exception("出现异常，请联系管理员");
            };
            //读取节点的条件
            string xPath = "//moduleType";
            XmlNodeList nodeList = XMLHelper.GetXmlNodeListByXpath(filePath, xPath);
            //生成系统列表
            for (int i = 0; i < nodeList.Count; i++)
            {
                if (nodeList[i].Attributes["sysId"].Value.Equals(moduleTypeId))
                {
                    sbString.Append(string.Format("<li class='curSys' sysId='{0}' enName='{1}' style='background-image: url(../Images/SystemIcon/{2}.png);'>{3}</li>", nodeList[i].Attributes["sysId"].Value, nodeList[i].Attributes["ename"].Value, nodeList[i].Attributes["icon"].Value, nodeList[i].Attributes["name"].Value));
                }
                else
                {
                    sbString.Append(string.Format("<li sysId='{0}' enName='{1}' style='background-image: url(../Images/SystemIcon/{2}.png);'>{3}</li>", nodeList[i].Attributes["sysId"].Value, nodeList[i].Attributes["ename"].Value, nodeList[i].Attributes["icon"].Value, nodeList[i].Attributes["name"].Value));
                }
            }
            //获取当前选择系统的ID
            return sbString.ToString();
        }
   
    }
}
