
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
using WeiXin.BO;
using System.Data;
using WeiXin.DAL;
using WeiXin.Model;
using WeiXin.Core;
using WeiXin.Core.Transfer;
using System.Xml;
namespace WeiXin.BLL
{
   public class ModulesServices
    {

       ModulesRepository modulesRepository = new ModulesRepository();

            /// <summary>
        /// 根据类型ID获取全部的菜单
        /// </summary>
        /// <param name="moduleTypeID"></param>
        /// <returns></returns>
        public List<ModulesContract> GetModulesByTypeId(int moduleTypeID)
        {
            DataSet ds = modulesRepository.GetModulesByTypeId(moduleTypeID);
            IList<Modules> list = DataConvert.DataSetToIList<Modules>(ds, "Modules");
            return list.Select(m => m.ToBO<ModulesContract>()).ToList();
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PageHelper<ModulesContract> GetPageData(int pageIndex, int pageSize, int moduleParentID = 0, int moduleTypeID = 0, string moduleName = "")
        {
            //获取数据
            object[] obj = modulesRepository.GetPageDataModules(pageIndex, pageSize, moduleParentID: moduleParentID, moduleTypeID: moduleTypeID, moduleName: moduleName);
            List<ModulesContract> list = DataConvert.DataSetToIList<ModulesContract>((DataSet)obj[0], 0).ToList();
            return new PageHelper<ModulesContract>(list, (int)obj[1], (int)obj[2], (int)obj[3]);
        }


        /// <summary>
        /// 根据菜单ID查询单个菜单信息
        /// </summary>
        /// <returns></returns>
        public ModulesContract GetModuleEntity(string moduleId)
        {
            DataSet ds = modulesRepository.GetModules(moduleId);
            return DataConvert.DataSetToEntity<Modules>(ds, 0).ToBO<ModulesContract>();
        }

        /// <summary>
        /// 根据类型和父级ID查询菜单信息(用于新增或者编辑)
        /// </summary>
        /// <param name="moduleTypeId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<ModulesContract> GetModulesByIdAndTypeId(int moduleTypeId, int parentId)
        {
            DataSet ds = modulesRepository.GetModulesByIdAndTypeId(moduleTypeId, parentId);
            return DataConvert.DataSetToIList<Modules>(ds, 0).Select(m => m.ToBO<ModulesContract>()).ToList();
        }


        /// <summary>
        /// 添加或者修改数据
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="authorityStr"></param>
        /// <param name="boo"></param>
        /// <returns></returns>
        public bool InsertModules(ModulesContract modulesContract, string authorityStr)
        {
            Modules modules = modulesContract.ToPO<Modules>();
            return modulesRepository.InsertModules(modules: modules, authorityStr: authorityStr);
        }


        /// <summary>
        /// 更新数据（执行的存储过程）
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="authorityStr"></param>
        /// <returns></returns>
        public bool UpdateModules(ModulesContract modulesContract, string authorityStr)
        {
            Modules modules = modulesContract.ToPO<Modules>();
            return modulesRepository.UpdateModules(modules: modules, authorityStr: authorityStr);
        }


        /// <summary>
        /// 更新模块状态
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public bool UpdateModuleStatus(string moduleIds)
        {
            return modulesRepository.UpdateModuleStatus(moduleIds);
        }
        /// <summary>
        /// 删除菜单（数据库使用的是级联删除）
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteModules(string ids)
        {
            return modulesRepository.DeleteModules(ids);
        }

        #region 读取XML文档

        public string GetMenu(string filePath, int moduleTypeId)
        {
            //判断XML文件是否存在 不存在抛出异常
            if (!XMLHelper.IsExists(filePath))
            {
                throw new Exception("出现异常，请联系管理员");
            };
            //菜单字符串
            StringBuilder sbString = new StringBuilder();
            //sbString.Append("<ul id='ulMenu'><li href='http://www.baidu.com' menuid='0' class='liBegin'><div>首页</div> </li>");
            //加载的系统下的一级菜单
            string moduleType = "moduleType_" + moduleTypeId;
            XmlNodeList xn_list = XMLHelper.GetXmlNodeListByXpath(filePath, string.Format("//moduleType[@id='{0}']/tree", moduleType));
            for (int nodeNum = 0; nodeNum < xn_list.Count; nodeNum++)
            {
                string className = nodeNum != xn_list.Count - 1 ? "liItem" : "liEnd";
                sbString.Append(string.Format("<li>|</li><li menuid='{0}' class='{1}'  {2}><div>{3}</div>", xn_list[nodeNum].Attributes["id"].Value, className, string.IsNullOrEmpty(xn_list[nodeNum].Attributes["controller"].Value) ? "" : "href='/" + xn_list[nodeNum].Attributes["areas"].Value + "/" + xn_list[nodeNum].Attributes["controller"].Value + "/" + xn_list[nodeNum].Attributes["action"].Value + "'", xn_list[nodeNum].Attributes["name"].Value));
                GetMenu(filePath, xn_list[nodeNum], sbString);
                sbString.Append("</li>");
            }
            sbString.Append("</ul>");
            return sbString.ToString();
        }

        private string GetMenu(string filePath, XmlNode node, StringBuilder sbString)
        {
            string splitLi = "";
            XmlNodeList xNode_list = XMLHelper.GetXmlNodeListByXpath(filePath, string.Format("//tree[@id='{0}']/tree", node.Attributes["id"].Value));
            if (xNode_list.Count != 0)
            {
                sbString.Append(string.Format("<ul parentmenuid='{0}'>", node.Attributes["id"].Value));
                for (int nodeNum = 0; nodeNum < xNode_list.Count; nodeNum++)
                {
                    sbString.Append(string.Format("{0}<li menuid='{1}' {2}><div>{3}</div>", splitLi, xNode_list[nodeNum].Attributes["id"].Value, string.IsNullOrEmpty(xNode_list[nodeNum].Attributes["controller"].Value) ? "" : "href='/" + xNode_list[nodeNum].Attributes["areas"].Value + "/" + xNode_list[nodeNum].Attributes["controller"].Value + "/" + xNode_list[nodeNum].Attributes["action"].Value + "'", xNode_list[nodeNum].Attributes["name"].Value));
                    GetMenu(filePath, xNode_list[nodeNum], sbString);
                    sbString.Append("</li>");
                }
                sbString.Append("</ul>");
            }
            return null;
        }

        #endregion 读取XML文档
    }
}
