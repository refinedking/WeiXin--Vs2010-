
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
using System.Web;
using System.Web.Mvc;
using WeiXin.BLL;
using WeiXin.Core;
using WeiXin.BO;
using System.Text;
using System.IO;
using System.Data;
using WeiXin.Web.Controllers;

namespace WeiXin.Web.Areas.WeiXin_Authority.Controllers
{
    [WeiXinException]
    public class AuthorityManagerController : Controller
    {
        #region 初始化实例

        AuthorityServices authorityServices = new AuthorityServices();
        ModulesServices modulesServices = new ModulesServices();
        ModuleTypeServices moduleTypeService = new ModuleTypeServices();
        ModuleAuthorityListServices moduleAuthorityListServices = new ModuleAuthorityListServices();
        RoleAuthorityListServices roleAuthorityListServices = new RoleAuthorityListServices();
        RolesServices rolesServices = new RolesServices();
        UsersServices usersServices = new UsersServices();
        EmployeeService employeeService = new EmployeeService();
        BranchService branchService = new BranchService();

        #endregion 初始化实例

        #region 初始化变量

        int pageSize = 10;
        int pageIndex = 1;

        #endregion 初始化变量

        #region 针对功能点的操作或者显示

        #region 功能点视图

        /// <summary>
        /// 获取功能点（默认的数据列表）
        /// </summary>
        /// <returns></returns>
        public ActionResult AuthorityList(string pIndex)
        {
            pageIndex = Session["pageIndex"] == null ? pageIndex : (int)Session["pageIndex"];
            //UsersContract usersContract = Session["user"] as UsersContract;
            PageHelper<AuthorityContract> pageData = authorityServices.GetPageData(pageIndex: pageIndex, pageSize: pageSize);
            ViewData["list_PageAuthority"] = GetPageAuthority("AuthorityList");
            return View(pageData);
        }

        /// <summary>
        /// 功能节点管理页面（新增或者编辑）
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddOrUpdateAuthority(string id)
        {
            AuthorityContract authorityContract = new AuthorityContract();
            if (!string.IsNullOrEmpty(id))
            {
                authorityContract = authorityServices.GetAuthortityEntityById(id);
            }
            return View(authorityContract);
        }

        #endregion 功能点视图

        #region 功能点操作

        /// <summary>
        /// 获取功能点（分页的数据）
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AuthorityList(FormCollection formCollection)
        {
            #region 定义变量

            string authorityName = formCollection["select_AuthorityName"].Trim(); ;
            PageHelper<AuthorityContract> pageData = null;

            #endregion 定义变量

            //处理输入数据时候的异常
            if (int.TryParse(formCollection["CurPage"], out pageIndex))
            {
                //throw new Exception("输入的页码有误！");
                Session["pageIndex"] = pageIndex;
                //获取分页数据
                pageData = authorityServices.GetPageData(pageIndex: pageIndex, pageSize: pageSize, authorityName: authorityName);
            }

            //用于保存查询条件
            ViewData["authorityName"] = authorityName;
            //设置权限
            ViewData["list_PageAuthority"] = GetPageAuthority("AuthorityList");
            return View(pageData);
        }

        /// <summary>
        /// 新增或者编辑功能节点
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOrUpdateAuthority(FormCollection formCollection, AuthorityContract authorityContract)
        {
            if (ModelState.IsValid)
            {
                //定义变量存储结果
                bool boo = true;
                //验证数据是否重复
                if (!authorityServices.IsExists(authorityTag: authorityContract.AuthorityTag, id: authorityContract.AuthorityID.ToString()))
                {
                    //执行失败信息
                    ViewData["msg"] = "功能点标识数据重复！请重新填写功能点标识!";
                    //失败视图
                    return View("Error");
                }
                boo = formCollection["AuthorityID"] != null ? authorityServices.UpdateAuthority(authorityContract: authorityContract) : authorityServices.AddAuthority(authorityContract: authorityContract);
                //执行结果判断
                if (boo)
                {
                    //执行成功信息
                    ViewData["msg"] = "功能点操作成功!";
                    //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                    ViewData["url"] = new UrlHelper(Request.RequestContext).Action("AuthorityList");
                    //成功视图
                    return View("Success");
                }
                else
                {
                    //执行失败信息
                    ViewData["msg"] = "功能点操作失败!";
                    //失败视图
                    return View("Error");
                }
            }
            return View(authorityContract);
        }

        /// <summary>
        /// 更新功能点状态
        /// </summary>
        /// <param name="ids">功能点ID</param>
        /// <returns></returns>
        public ActionResult UpdateAuthorityStatus(string id)
        {
            if (authorityServices.UpdateAuthorityStatus(id))
            {
                //执行成功信息
                ViewData["msg"] = "更改功能点状态成功!";
                //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                ViewData["url"] = new UrlHelper(Request.RequestContext).Action("AuthorityList");
                //成功视图
                return View("Success");
            }
            else
            {
                //执行成功信息
                ViewData["msg"] = "更改功能点状态失败!";
                //成功视图
                return View("Error");
            }
        }

        /// <summary>
        /// 删除功能点(同步操作)
        /// </summary>
        /// <param name="ids">需要删除的功能点ID</param>
        /// <returns></returns>
        public ActionResult DeleteAuthority(string id)
        {
            //执行删除操作
            if (authorityServices.DeleteAuthority(id))
            {
                //执行成功信息
                ViewData["msg"] = "删除功能点成功!";
                //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                ViewData["url"] = new UrlHelper(Request.RequestContext).Action("AuthorityList");
                //成功视图
                return View("Success");
            }
            else
            {
                //执行成功信息
                ViewData["msg"] = "删除功能点失败!";
                //成功视图
                return View("Error");
            }
        }

        /// <summary>
        /// 功能点详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AuthorityDetails(string id)
        {
            return View(authorityServices.GetAuthortityEntityById(id));
        }

        #endregion 功能点操作

        #region 验证重复

        /// <summary>
        /// 判断用户名是否重复
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string IsExistsAuthorityTag(string authorityTag, string id)
        {
            if (authorityServices.IsExists(authorityTag: authorityTag, id: id))
            {
                return "OK";
            }
            return "Error";
        }

        #endregion 验证重复

        #endregion 针对功能点的操作或者显示

        #region 针对模块的操作或者显示

        #region 模块视图

        /// <summary>
        /// 分页数据（默认）
        /// </summary>
        /// <returns></returns>
        public ActionResult ModuleList()
        {
            pageIndex = Session["Auhority_pageIndex"] == null ? pageIndex : (int)Session["pageIndex"];
            //查询菜单类型
            ViewData["ModulesType"] = new SelectList(moduleTypeService.GetAllModuleType(), "ModuleTypeID", "ModuleTypeName");
            //分页数据
            PageHelper<ModulesContract> pageData = modulesServices.GetPageData(pageIndex: pageIndex, pageSize: pageSize);
            //获取菜单功能点权限
            ViewData["list_PageAuthority"] = GetPageAuthority("ModuleList");
            return View(pageData);
        }

        /// <summary>
        /// 更新或者添加视图
        /// </summary>
        /// <returns></returns>
        public ActionResult AddOrUpdateModules(string actionTag, string id)
        {
            ModulesContract modulesContract = new ModulesContract();

            //用于查询当前菜单拥有的功能点
            int moduleId;
            int.TryParse(id, out moduleId);
            //判断为更新页面
            if (!"add".Equals(actionTag))
            {
                modulesContract = modulesServices.GetModuleEntity(id);

                #region 判断默认菜单选项

                StringBuilder sbOneStr = new StringBuilder();
                StringBuilder sbTwoStr = new StringBuilder();
                sbOneStr.Append("<option value='0'>--请选择--</option>");
                sbTwoStr.Append("<option value='0'>--请选择--</option>");
                //获取所有的一级菜单
                List<ModulesContract> list = modulesServices.GetModulesByIdAndTypeId(modulesContract.ModuleTypeID, 0);
                if (modulesContract.ModuleParentID == 0)
                {
                    foreach (var item in list)
                    {
                        sbOneStr.Append(string.Format("<option  value={0}>{1}</option>", item.ModuleID, item.ModuleName));
                    }
                    ViewData["OneMenu"] = sbOneStr.ToString();
                    ViewData["TwoMenu"] = sbTwoStr.ToString();
                }
                else
                {
                    //判断当前节点是否是3级菜单(当前菜单的父级信息的ModuleParentID!=0则当前菜单为3级菜单)
                    ModulesContract modules_One_Entity = modulesServices.GetModuleEntity(modulesContract.ModuleParentID.ToString());
                    if (modules_One_Entity.ModuleParentID != 0)
                    {
                        foreach (var item in list)
                        {
                            sbOneStr.Append(string.Format("<option {0} value={1}>{2}</option>", item.ModuleID == modules_One_Entity.ModuleParentID ? "selected" : "", item.ModuleID, item.ModuleName));
                        }
                        ViewData["OneMenu"] = sbOneStr.ToString();
                        //循环二级菜单
                        foreach (var item in modulesServices.GetModulesByIdAndTypeId(modulesContract.ModuleTypeID, modules_One_Entity.ModuleParentID))
                        {
                            sbTwoStr.Append(string.Format("<option {0} value={1}>{2}</option>", item.ModuleID == modules_One_Entity.ModuleID ? "selected" : "", item.ModuleID, item.ModuleName));
                        }
                        ViewData["TwoMenu"] = sbTwoStr.ToString();
                    }
                    else
                    {
                        foreach (var item in list)
                        {
                            sbOneStr.Append(string.Format("<option {0} value={1}>{2}</option>", item.ModuleID == modules_One_Entity.ModuleID ? "selected" : "", item.ModuleID, item.ModuleName));
                        }
                        ViewData["OneMenu"] = sbOneStr.ToString();
                        //循环二级菜单
                        foreach (var item in modulesServices.GetModulesByIdAndTypeId(modulesContract.ModuleTypeID, modules_One_Entity.ModuleID))
                        {
                            sbTwoStr.Append(string.Format("<option  value={0}>{1}</option>", item.ModuleID, item.ModuleName));
                        }
                        ViewData["TwoMenu"] = sbTwoStr.ToString();
                    }
                }

                #endregion 判断默认菜单选项
            }
            //页面拥有的功能点
            ViewData["Module_Authority"] = moduleAuthorityListServices.GetAuthorityByModuleID(moduleId);
            //查询菜单类型
            ViewData["ModulesType"] = new SelectList(moduleTypeService.GetAllModuleType(), "ModuleTypeID", "ModuleTypeName", modulesContract.ModuleTypeID);
            //页面功能(所有功能点)
            ViewData["Authority"] = authorityServices.GetAllAuthority();
            //返回视图
            return View(modulesContract);
        }

        /// <summary>
        /// 菜单的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ModulesDetails(string id)
        {
            ModulesContract modulesContract = modulesServices.GetModuleEntity(id);
            //页面拥有的功能
            ViewData["Module_Authority"] = moduleAuthorityListServices.GetAuthorityByModuleID(int.Parse(id));

            #region 判断默认菜单选项

            StringBuilder sbOneStr = new StringBuilder();
            StringBuilder sbTwoStr = new StringBuilder();
            sbOneStr.Append("<option value='0'>--请选择--</option>");
            sbTwoStr.Append("<option value='0'>--请选择--</option>");
            //获取所有的一级菜单
            List<ModulesContract> list = modulesServices.GetModulesByIdAndTypeId(modulesContract.ModuleTypeID, 0);
            if (modulesContract.ModuleParentID == 0)
            {
                foreach (var item in list)
                {
                    sbOneStr.Append(string.Format("<option  value={0}>{1}</option>", item.ModuleID, item.ModuleName));
                }
                ViewData["OneMenu"] = sbOneStr.ToString();
                ViewData["TwoMenu"] = sbTwoStr.ToString();
            }
            else
            {
                //判断当前节点是否是3级菜单(当前菜单的父级信息的ModuleParentID!=0则当前菜单为3级菜单)
                ModulesContract modules_One_Entity = modulesServices.GetModuleEntity(modulesContract.ModuleParentID.ToString());
                if (modules_One_Entity.ModuleParentID != 0)
                {
                    foreach (var item in list)
                    {
                        sbOneStr.Append(string.Format("<option {0} value={1}>{2}</option>", item.ModuleID == modules_One_Entity.ModuleParentID ? "selected" : "", item.ModuleID, item.ModuleName));
                    }
                    ViewData["OneMenu"] = sbOneStr.ToString();
                    //循环二级菜单
                    foreach (var item in modulesServices.GetModulesByIdAndTypeId(modulesContract.ModuleTypeID, modules_One_Entity.ModuleParentID))
                    {
                        sbTwoStr.Append(string.Format("<option {0} value={1}>{2}</option>", item.ModuleID == modules_One_Entity.ModuleID ? "selected" : "", item.ModuleID, item.ModuleName));
                    }
                    ViewData["TwoMenu"] = sbTwoStr.ToString();
                }
                else
                {
                    foreach (var item in list)
                    {
                        sbOneStr.Append(string.Format("<option {0} value={1}>{2}</option>", item.ModuleID == modules_One_Entity.ModuleID ? "selected" : "", item.ModuleID, item.ModuleName));
                    }
                    ViewData["OneMenu"] = sbOneStr.ToString();
                    //循环二级菜单
                    foreach (var item in modulesServices.GetModulesByIdAndTypeId(modulesContract.ModuleTypeID, modules_One_Entity.ModuleID))
                    {
                        sbTwoStr.Append(string.Format("<option  value={0}>{1}</option>", item.ModuleID, item.ModuleName));
                    }
                    ViewData["TwoMenu"] = sbTwoStr.ToString();
                }
            }

            #endregion 判断默认菜单选项

            //查询菜单类型
            ViewData["ModulesType"] = new SelectList(moduleTypeService.GetAllModuleType(), "ModuleTypeID", "ModuleTypeName", modulesContract.ModuleTypeID);
            //页面功能(所有功能)
            ViewData["Authority"] = authorityServices.GetAllAuthority();
            //返回视图
            return View(modulesContract);
        }

        #endregion 模块视图

        #region 模块操作

        /// <summary>
        /// 分页数据（提交后的数据）
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModuleList(FormCollection formCollection)
        {
            #region 定义变量

            int pageIndex;
            int moduleParentID;
            int moduleTypeID;
            PageHelper<ModulesContract> pageData = null;
            string moduleName;

            #endregion 定义变量

            #region 查询所需数据

            //获取当前添加的节点归属（属于子节点还是父节点）
            string[] str1 = formCollection["ModuleMenuID"].Split(',');
            string[] str = formCollection["ModuleMenuID"].Split(',');
            //一级菜单ID
            int numOne = int.Parse(str[0]);
            //二级菜单ID
            int numTwo = int.Parse(str[1]);
            if (numOne == 0)
            {
                //一级菜单的ID为0说明当前查询的是节点下面的
                moduleParentID = 0;
            }
            else
            {
                //如果二级菜单是0说明当前添加的菜单归属于一级菜单，否则归属于二级菜单
                moduleParentID = numTwo == 0 ? numOne : numTwo;
            }
            //获取菜单名称
            moduleName = formCollection["select_ModuleName"];
            //获取菜单类型
            moduleTypeID = string.IsNullOrEmpty(formCollection["ModuleTypeID"]) ? 0 : int.Parse(formCollection["ModuleTypeID"]);

            #endregion 查询所需数据

            if (int.TryParse(formCollection["curPage"], out pageIndex))
            {
                pageData = modulesServices.GetPageData(pageIndex: pageIndex, pageSize: pageSize, moduleParentID: moduleParentID, moduleTypeID: moduleTypeID, moduleName: moduleName);
            }

            #region 设置默认中的下拉列表

            //判断不是父节点
            StringBuilder sbOneStr = new StringBuilder();
            StringBuilder sbTwoStr = new StringBuilder();
            //判断当前节点是否是3级菜单(当前菜单的父级信息的ModuleParentID!=0则当前菜单为3级菜单)
            //获取所有的一级菜单
            List<ModulesContract> list = modulesServices.GetModulesByIdAndTypeId(moduleTypeID, 0);
            foreach (var item in list)
            {
                sbOneStr.Append(string.Format("<option {0} value={1}>{2}</option>", item.ModuleID == numOne ? "selected" : "", item.ModuleID, item.ModuleName));
            }
            ViewData["OneMenu"] = sbOneStr.ToString();
            //循环二级菜单
            foreach (var item in modulesServices.GetModulesByIdAndTypeId(moduleTypeID, numOne))
            {
                sbTwoStr.Append(string.Format("<option {0} value={1}>{2}</option>", item.ModuleID == numTwo ? "selected" : "", item.ModuleID, item.ModuleName));
            }
            ViewData["TwoMenu"] = sbTwoStr.ToString();

            #endregion 设置默认中的下拉列表

            //绑定查询菜单的名称
            ViewData["ModuleName"] = moduleName;

            //获取菜单功能点权限
            ViewData["list_PageAuthority"] = GetPageAuthority("ModuleList");
            //查询菜单类型
            ViewData["ModulesType"] = new SelectList(moduleTypeService.GetAllModuleType(), "ModuleTypeID", "ModuleTypeName", moduleTypeID);
            return View(pageData);
        }

        /// <summary>
        /// 更新或者添加操作
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOrUpdateModules(FormCollection formCollection, ModulesContract modulesContract)
        {
            //验证通过
            if (ModelState.IsValid)
            {
                #region 定义变量

                //用于判断操作是否成功
                bool boo = false;
                //获取当前添加的节点归属（属于子节点还是父节点）
                string[] str;
                //一级菜单ID
                int numOne;
                //二级菜单ID
                int numTwo;
                //获取上传的名字
                string fileName = "";
                //设置文件上传的路径
                string savaPath = "";

                #endregion 定义变量

                #region 文件上传

                for (var i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase HttpPostedFileBase = Request.Files[i];
                    //设置文件上传的路径
                    savaPath = AppDomain.CurrentDomain.BaseDirectory + @"\Images\ModuleImage\";
                    //获取上传的名字
                    fileName = Path.GetFileName(HttpPostedFileBase.FileName);
                    if (HttpPostedFileBase != null && !string.IsNullOrEmpty(fileName))
                    {
                        //判断当前文件是否存在已经重名图片
                        string filePath = savaPath + fileName;
                        if (System.IO.File.Exists(filePath))
                        {
                            DateTime dateTime = DateTime.Now;
                            string[] strFile = fileName.Split('.');
                            StringBuilder SbString = new StringBuilder();
                            SbString.Append(strFile[0]);
                            SbString.Append(dateTime.Year.ToString());
                            SbString.Append(dateTime.Month.ToString());
                            SbString.Append(dateTime.Day.ToString());
                            SbString.Append(dateTime.Hour.ToString());
                            SbString.Append(dateTime.Minute.ToString());
                            SbString.Append(dateTime.Second.ToString());
                            SbString.Append(".");
                            SbString.Append(strFile[1]);
                            fileName = SbString.ToString();
                        }
                        HttpPostedFileBase.SaveAs(Path.Combine(savaPath, fileName));
                    }
                }

                #endregion 文件上传

                #region 菜单归属

                str = formCollection["ModuleMenuID"].Split(',');
                int.TryParse(str[0], out numOne);
                int.TryParse(str[1], out numTwo);
                if (numOne == 0)
                {
                    //一级菜单的ID为0说明当前添加的是根菜单
                    modulesContract.ModuleParentID = 0;
                }
                else
                {
                    //如果二级菜单是0说明当前添加的菜单归属于一级菜单，否则归属于二级菜单
                    modulesContract.ModuleParentID = numTwo == 0 ? numOne : numTwo;
                }

                #endregion 菜单归属

                //设置当前菜单图片名称
                modulesContract.ModuleIcon = fileName;
                //获取当前页面拥有的操作
                string authority = formCollection["ModuleAuthority"] == null ? "" : formCollection["ModuleAuthority"];
                //执行添加操作
                if (string.IsNullOrEmpty(formCollection["ModuleID"]))
                {
                    boo = modulesServices.InsertModules(modulesContract, authority);
                }
                //执行修改操作
                else
                {
                    boo = modulesServices.UpdateModules(modulesContract, authority);
                }
                if (boo)
                {
                    //执行成功信息
                    ViewData["msg"] = "更新菜单成功!";
                    //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                    ViewData["url"] = new UrlHelper(Request.RequestContext).Action("ModuleList");
                    //成功视图
                    return View("Success");
                }
                else
                {
                    //执行失败信息
                    ViewData["msg"] = "更新菜单失败!";
                    return View("Error");
                }
            }

            //验证失败

            #region 判断默认菜单选项

            StringBuilder sbOneStr = new StringBuilder();
            StringBuilder sbTwoStr = new StringBuilder();
            sbOneStr.Append("<option value='0'>--请选择--</option>");
            sbTwoStr.Append("<option value='0'>--请选择--</option>");

            //获取所有的一级菜单
            List<ModulesContract> list = modulesServices.GetModulesByIdAndTypeId(modulesContract.ModuleTypeID, 0);

            if (modulesContract.ModuleParentID == 0)
            {
                foreach (var item in list)
                {
                    sbOneStr.Append(string.Format("<option  value={0}>{1}</option>", item.ModuleID, item.ModuleName));
                }

                ViewData["OneMenu"] = sbOneStr.ToString();

                ViewData["TwoMenu"] = sbTwoStr.ToString();
            }

            else
            {
                //判断当前节点是否是3级菜单(当前菜单的父级信息的ModuleParentID!=0则当前菜单为3级菜单)
                ModulesContract modules_One_Entity = modulesServices.GetModuleEntity(modulesContract.ModuleParentID.ToString());
                if (modules_One_Entity.ModuleParentID != 0)
                {
                    foreach (var item in list)
                    {
                        sbOneStr.Append(string.Format("<option {0} value={1}>{2}</option>", item.ModuleID == modules_One_Entity.ModuleParentID ? "selected" : "", item.ModuleID, item.ModuleName));
                    }

                    ViewData["OneMenu"] = sbOneStr.ToString();

                    //循环二级菜单
                    foreach (var item in modulesServices.GetModulesByIdAndTypeId(modulesContract.ModuleTypeID, modules_One_Entity.ModuleParentID))
                    {
                        sbTwoStr.Append(string.Format("<option {0} value={1}>{2}</option>", item.ModuleID == modules_One_Entity.ModuleID ? "selected" : "", item.ModuleID, item.ModuleName));
                    }

                    ViewData["TwoMenu"] = sbTwoStr.ToString();
                }
                else
                {
                    foreach (var item in list)
                    {
                        sbOneStr.Append(string.Format("<option {0} value={1}>{2}</option>", item.ModuleID == modules_One_Entity.ModuleID ? "selected" : "", item.ModuleID, item.ModuleName));
                    }
                    ViewData["OneMenu"] = sbOneStr.ToString();
                    //循环二级菜单
                    foreach (var item in modulesServices.GetModulesByIdAndTypeId(modulesContract.ModuleTypeID, modules_One_Entity.ModuleID))
                    {
                        sbTwoStr.Append(string.Format("<option  value={0}>{1}</option>", item.ModuleID, item.ModuleName));
                    }
                    ViewData["TwoMenu"] = sbTwoStr.ToString();
                }
            }

            #endregion 判断默认菜单选项

            ViewData["ModulesType"] = new SelectList(moduleTypeService.GetAllModuleType(), "ModuleTypeID", "ModuleTypeName");
            ViewData["Authority"] = authorityServices.GetAllAuthority();
            //页面拥有的功能
            ViewData["Module_Authority"] = moduleAuthorityListServices.GetAuthorityByModuleID(modulesContract.ModuleID);
            return View(modulesContract);
        }

        /// <summary>
        /// 更新模块状态(同步操作)
        /// </summary>
        /// <param name="id">需要更新的模块Id</param>
        /// <returns></returns>
        public ActionResult UpdateModuleStatus(string id)
        {
            if (modulesServices.UpdateModuleStatus(id))
            {
                //执行成功信息
                ViewData["msg"] = "更新菜单状态成功!";
                //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                ViewData["url"] = new UrlHelper(Request.RequestContext).Action("ModuleList");
                //成功视图
                return View("Success");
            }
            else
            {
                //执行成功信息
                ViewData["msg"] = "更新菜单状态失败!";
                //成功视图
                return View("Error");
            };
        }

        /// <summary>
        /// 删除模块(同步操作)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult DeleteModules(string id)
        {
            if (modulesServices.DeleteModules(id))
            {
                //执行成功信息
                ViewData["msg"] = "删除菜单成功!";
                //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                ViewData["url"] = new UrlHelper(Request.RequestContext).Action("ModuleList");
                //成功视图
                return View("Success");
            }
            else
            {
                //执行成功信息
                ViewData["msg"] = "删除菜单失败!";
                //成功视图
                return View("Error");
            };
        }

        #endregion 模块操作

        #region 验证重复

        #endregion 验证重复

        /// <summary>
        /// 获取模块根据模块类型
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMenu(string moduleTypeId, string parentId)
        {
            int moduleParentId;
            if (!string.IsNullOrEmpty(parentId) && int.Parse(parentId) == 0)
            {
                return Json("");
            }
            moduleParentId = string.IsNullOrEmpty(parentId) ? 0 : int.Parse(parentId);
            if (!string.IsNullOrEmpty(moduleTypeId))
            {
                return Json(modulesServices.GetModulesByIdAndTypeId(int.Parse(moduleTypeId), moduleParentId));
            }
            return Json("");
        }

        #endregion 针对模块的操作或者显示

        #region 针对用户的操作或者显示

        #region 用户视图

        /// <summary>
        /// 用户列表（默认）
        /// </summary>
        /// <returns></returns>
        public ActionResult UsersList()
        {
            //获取全部角色
            ViewData["role"] = rolesServices.GetRoles();
            //分页数据
            PageHelper<UsersContract> pageData = usersServices.GetPageData(pageIndex: pageIndex, pageSize: pageSize);
            //获取菜单功能点权限
            ViewData["list_PageAuthority"] = GetPageAuthority("UsersList");
            return View(pageData);
        }

        /// <summary>
        /// 用户详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UsersDetails(string id)
        {
            UsersContract usersContract = usersServices.GetUserEntity(id);
            ViewData["role"] = new SelectList(rolesServices.GetRoles(), "RoleID", "RoleName", usersContract.RoleID);
            return View(usersContract);
        }

        /// <summary>
        /// 添加或者更新用户信息
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddOrUpdateUser(string id)
        {
            UsersContract usersContract = new UsersContract();
            if (!string.IsNullOrEmpty(id))
            {
                usersContract = usersServices.GetUserEntity(id);
            }
            //查询当前用户所在部门ID
            string branchId = employeeService.GetPositionAndBranchByEmpId(usersContract.EmployeeID).Tables[0].Rows.Count == 0 ? "0" : employeeService.GetPositionAndBranchByEmpId(usersContract.EmployeeID).Tables[0].Rows[0][1].ToString();
            //所有部门

            #region 绑定

            List<BranchInfoContract> list = new List<BranchInfoContract>();
            DataSet dsBranch = branchService.FindIsFBranch(50);
            for (int i = 0; i < dsBranch.Tables[0].Rows.Count; i++)
            {
                BranchInfoContract branch = new BranchInfoContract();
                branch.BranchID = Convert.ToInt32(dsBranch.Tables[0].Rows[i]["BranchID"].ToString());
                branch.BranchName = "├－" + CommonHelper.getListName(dsBranch.Tables[0].Rows[i]["BranchName"].ToString(), dsBranch.Tables[0].Rows[i]["Temp1"].ToString());

                list.Add(branch);
            }

            ViewData["Branch"] = new SelectList(list, "BranchID", "BranchName", branchId);

            #endregion 绑定

            if (!string.IsNullOrEmpty(id))
            {
                //获取当前部门下所有的员工
                ViewData["empInfo"] = new SelectList(employeeService.GetEmployeeByBId(branchId), "EId", "EmpName", usersContract.EmployeeID);
            }
            ViewData["role"] = new SelectList(rolesServices.GetRoles(), "RoleID", "RoleName", usersContract.RoleID);

            return View(usersContract);
        }

        #endregion 用户视图

        #region 用户操作

        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UsersList(FormCollection formCollection)
        {
            #region 定义变量

            PageHelper<UsersContract> pageData = null;
            string select_userName;

            #endregion 定义变量

            #region 查询条件

            //存储页码

            select_userName = formCollection["select_userName"];

            #endregion 查询条件

            if (int.TryParse(formCollection["curPage"], out pageIndex))
            {
                pageData = usersServices.GetPageData(pageIndex: pageIndex, pageSize: pageSize, userName: select_userName);
            }
            ViewData["userName"] = select_userName;
            //获取全部角色
            ViewData["role"] = rolesServices.GetRoles();
            //获取菜单功能点权限
            ViewData["list_PageAuthority"] = GetPageAuthority("UsersList");
            return View(pageData);
        }

        /// <summary>
        /// 添加或者更新操作
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOrUpdateUser(UsersContract usersContract)
        {
            if (ModelState.IsValid)
            {
                bool boo = false;
                if (usersContract.UserID == 0)
                {
                    //检查用户帐号是否重复
                    if (!usersServices.IsExists(usersContract.UserID.ToString(), usersContract.UserName))
                    {
                        //执行成功信息
                        ViewData["msg"] = "用户帐户已经重复，请重新输入新的帐户保证正常运行!";
                        //成功视图
                        return View("Error");
                    }
                    usersContract.CreateTime = DateTime.Now;
                    DataSet ds = usersServices.InsertUsers(usersContract: usersContract);
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        //执行成功信息
                        ViewData["msg"] = string.Format("添加用户成功! 用户名：{0}  密码：{1}", ds.Tables[0].Rows[0][0].ToString(), ds.Tables[0].Rows[0][1].ToString());
                        //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                        ViewData["url"] = new UrlHelper(Request.RequestContext).Action("UsersList");
                        //成功视图
                        return View("Success");
                    }
                    else
                    {
                        //执行成功信息
                        ViewData["msg"] = "添加用户失败!";
                        //成功视图
                        return View("Error");
                    }
                }
                else
                {
                    boo = usersServices.UpdateUsers(usersContract: usersContract);
                }
                if (boo)
                {
                    //执行成功信息
                    ViewData["msg"] = "更新用户成功! ";
                    //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                    ViewData["url"] = new UrlHelper(Request.RequestContext).Action("UsersList");
                    //成功视图
                    return View("Success");
                }
            }
            ViewData["role"] = new SelectList(rolesServices.GetRoles(), "RoleID", "RoleName");
            ViewData["Branch"] = new SelectList(branchService.GetAllBranch(), "BranchId", "BranchName");
            ViewData["empInfo"] = new SelectList(employeeService.GetAllEmpInfo(), "EmpId", "EmpName", usersContract.EmployeeID);
            return View(usersContract);
        }

        /// <summary>
        /// 重置用户密码(同步操作)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult UpdateUsersPassword(string id)
        {
            if (usersServices.UpdateUsersPassword(id))
            {
                //执行成功信息
                ViewData["msg"] = "密码已经重置!";
                //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                ViewData["url"] = new UrlHelper(Request.RequestContext).Action("UsersList");
                //成功视图
                return View("Success");
            }
            else
            {
                //执行成功信息
                ViewData["msg"] = "密码重置失败!";
                //成功视图
                return View("Error");
            };
        }

        /// <summary>
        /// 更新用户状态(同步操作)
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public ActionResult UpdateUsersStatus(string id)
        {
            if (usersServices.UpdateUsersStatus(id))
            {
                //执行成功信息
                ViewData["msg"] = "更新用户状态成功!";
                //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                ViewData["url"] = new UrlHelper(Request.RequestContext).Action("UsersList");
                //成功视图
                return View("Success");
            }
            else
            {
                //执行成功信息
                ViewData["msg"] = "更新功能点状态失败!";
                //成功视图
                return View("Error");
            };
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteUser(string id)
        {
            if (usersServices.DeleteUsers(id))
            {
                //执行成功信息
                ViewData["msg"] = "删除用户成功!";
                //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                ViewData["url"] = new UrlHelper(Request.RequestContext).Action("UsersList");
                //成功视图
                return View("Success");
            }
            //执行成功信息
            ViewData["msg"] = "删除用户失败!";
            //成功视图
            return View("Success");
        }

        #endregion 用户操作

        #region 验证重复

        #endregion 验证重复

        #endregion 针对用户的操作或者显示

        #region 针对角色的操作或者显示

        #region 角色视图

        /// <summary>
        /// 角色列表(默认)
        /// </summary>
        /// <returns></returns>
        public ActionResult RolesList()
        {
            PageHelper<RolesContract> pageData = rolesServices.GetPageData(pageIndex: pageIndex, pageSize: pageSize);
            //获取菜单功能点权限
            ViewData["list_PageAuthority"] = GetPageAuthority("RolesList");
            return View(pageData);
        }

        /// <summary>
        /// 添加或者更新角色
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddOrUpdateRole(string tag, string id)
        {
            //创建空实体
            RolesContract rolesContract = new RolesContract();
            if (!("add").Equals(tag))
            {
                rolesContract = rolesServices.GetRoleEntity(id);
            }
            return View(rolesContract);
        }

        /// <summary>
        /// 更新角色权限视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Update_Role_Authority(string id)
        {
            //获取所有系统
            List<ModuleTypeContract> list_ModuleType = moduleTypeService.GetAllModuleType();
            ViewData["ModuleType"] = new SelectList(list_ModuleType, "ModuleTypeID", "ModuleTypeName");
            ViewData["Roles"] = new SelectList(rolesServices.GetRoles(), "RoleID", "RoleName", int.Parse(id));
            ViewData["Menu"] = GetHtmlString(list_ModuleType == null ? 0 : list_ModuleType[0].ModuleTypeID, roleID: id);
            return View();
        }

        /// <summary>
        /// 更新用户权限视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Update_User_Authority(string id)
        {
            //ViewData["userId"] = id;
            //获取所有系统
            List<ModuleTypeContract> list_ModuleType = moduleTypeService.GetAllModuleType();
            ViewData["ModuleType"] = new SelectList(list_ModuleType, "ModuleTypeID", "ModuleTypeName");
            ViewData["Roles"] = new SelectList(rolesServices.GetRoles(), "RoleID", "RoleName");
            ViewData["Menu"] = GetHtmlString(list_ModuleType == null ? 0 : list_ModuleType[0].ModuleTypeID, userID: id);
            ViewData["user"] = usersServices.GetUserEntity(usersId: id);
            
            //查询所有人员
            ViewData["Users"] = new SelectList(usersServices.GetAllUsers(), "UserID", "UserName", id);
            return View();
        }

        #endregion 角色视图

        #region 角色操作

        /// <summary>
        /// 角色列表(分页数据)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RolesList(FormCollection formCollection, string tag)
        {
            #region 定义变量

            //查询条件（角色名称）
            string roleName = formCollection["select_roleName"].Trim(); ;
            PageHelper<RolesContract> pageData = null;
            int pageIndex;
            string filePath = "/FileXML/" + (Session["user"] as UsersContract).UserName + "_xml";

            #endregion 定义变量

            if (!int.TryParse(formCollection["curPage"], out pageIndex))
            {
                pageIndex = 1;
            }
            //分页数据
            pageData = rolesServices.GetPageData(pageIndex: pageIndex, pageSize: pageSize, roleName: roleName);
            //获取菜单功能点权限
            ViewData["list_PageAuthority"] = GetPageAuthority("RolesList");
            //保存数据
            ViewData["roleName"] = roleName;
            return View(pageData);
        }

        /// <summary>
        /// 添加或者更新操作
        /// </summary>
        /// <param name="rolesContract"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOrUpdateRole(FormCollection formCollection, RolesContract rolesContract)
        {
            if (ModelState.IsValid)
            {
                //用于判断是否立即编辑权限
                int num;
                int.TryParse(formCollection["IsMenu"], out num);
                bool boo = false;
                //获取角色ID用于设置权限
                int roleId = 0;
                if (rolesContract.RoleID == 0)
                {
                    boo = int.TryParse(rolesServices.AddRole(rolesContract), out roleId);
                }
                else
                {
                    boo = rolesServices.UpdateRole(rolesContract);
                }
                if (boo)
                {
                    roleId = rolesContract.RoleID == 0 ? roleId : rolesContract.RoleID;

                    if (num != 0)
                    {
                        //执行成功信息
                        ViewData["msg"] = "角色信息操作成功!";
                        //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                        ViewData["url"] = new UrlHelper(Request.RequestContext).Action("RolesList");
                        //成功视图
                        return View("Success");
                    }
                    return RedirectToAction("Update_Role_Authority", "AuthorityManager", new { id = roleId });
                }
                //执行成功信息
                ViewData["msg"] = "角色信息操作失败!";
                return View("Error");
            }
            return View(rolesContract);
        }

        /// <summary>
        /// 更新角色状态
        /// </summary>
        /// <param name="ids">角色ID  比如 1，2，3 或者 1</param>
        /// <returns></returns>
        public ActionResult UpdateRoleStatus(string id)
        {
            if (rolesServices.UpdateRoleStatus(id))
            {
                //执行成功信息
                ViewData["msg"] = "更新角色状态成功!";
                //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                ViewData["url"] = new UrlHelper(Request.RequestContext).Action("RolesList");
                //成功视图
                return View("Success");
            }
            else
            {
                //执行成功信息
                ViewData["msg"] = "更新角色状态失败!";
                //成功视图
                return View("Error");
            };
        }

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult DeleteRole(string id)
        {
            if (rolesServices.DeleteRole(id))
            {
                //执行成功信息
                ViewData["msg"] = "删除角色成功!";
                //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                ViewData["url"] = new UrlHelper(Request.RequestContext).Action("RolesList");
                //成功视图
                return View("Success");
            }
            //执行成功信息
            ViewData["msg"] = "删除角色失败!";
            //成功视图
            return View("Error");
        }

        /// <summary>
        /// 更新角色权限视图
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update_Role_Authority(FormCollection formCollection)
        {
            #region 定义变量

            string role_Authority;
            string roleId;
            int moduleTypeID;

            #endregion 定义变量

            role_Authority = formCollection["RoleAuthority"];
            roleId = formCollection["RoleID"];
            if (!int.TryParse(formCollection["ModuleType"], out moduleTypeID))
            {
                throw new Exception("数据出错，请联系管理员");
            };
            bool boo = roleAuthorityListServices.UpdateRoleAuthority(role_Authority: role_Authority, moduleTypeID: moduleTypeID, roleId: roleId);
            if (boo)
            {
                //执行成功信息
                ViewData["msg"] = "更新角色权限成功!";
                //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                ViewData["url"] = new UrlHelper(Request.RequestContext).Action("RolesList");
                //成功视图
                return View("Success");
            }
            else
            {
                //执行失败信息
                ViewData["msg"] = "更新角色权限失败!";
                return View("Error");
            }
        }

        #endregion 角色操作

        #region 验证重复

        /// <summary>
        /// 检查角色名称是否重复
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public JsonResult IsExistsRoleName(string name)
        {
            bool isValidation = true;
            if (!rolesServices.IsExists(name))
            {
                isValidation = false;
            }
            return Json(isValidation, JsonRequestBehavior.AllowGet);
        }

        #endregion 验证重复

        #region 公共方法

        #endregion 公共方法

        /// <summary>
        /// 更新用户权限视图操作
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update_User_Authority(FormCollection formCollection)
        {
            #region 定义变量

            string role_Authority;
            int userId;
            int moduleTypeID;

            #endregion 定义变量

            //ViewData["userId"] = formCollection["userId"];
            role_Authority = formCollection["RoleAuthority"];

            if (!int.TryParse(formCollection["UserIdDDL"], out userId))
            {
                throw new Exception("用户数据出错");
            }

            if (!int.TryParse(formCollection["ModuleType"], out moduleTypeID))
            {
                throw new Exception("数据出错");
            };
            bool boo = roleAuthorityListServices.UpdateRoleAuthority(role_Authority: role_Authority, moduleTypeID: moduleTypeID, userId: userId.ToString());
            if (boo)
            {
                //执行成功信息
                ViewData["msg"] = "用户更新权限成功!";
                //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                ViewData["url"] = new UrlHelper(Request.RequestContext).Action("UsersList");
                //成功视图
                return View("Success");
            }
            else
            {
                //执行失败信息
                ViewData["msg"] = "用户权限更新失败!";
                return View("Error");
            }
            ////获取所有系统
            //List<ModuleTypeContract> list_ModuleType = moduleTypeService.GetAllModuleType();
            //ViewData["ModuleType"] = new SelectList(list_ModuleType, "ModuleTypeID", "ModuleTypeName");
            //ViewData["Roles"] = new SelectList(rolesServices.GetRoles(), "RoleID", "RoleName");
            //ViewData["Menu"] = GetHtmlString(list_ModuleType.Count == 0 ? 0 : list_ModuleType[0].ModuleTypeID, userID: formCollection["userId"]);
            ////查询所有部门
            //ViewData["Branch"] = new SelectList(branchService.GetAllBranch(), "BranchID", "BranchName");
            ////查询所有人员
            //ViewData["Users"] = new SelectList(usersServices.GetAllUsers(), "UserID", "UserName", userId);
            //return View();
        }

        /// <summary>
        /// 根据系统ID查询当前系统菜单及功能点生成HTML
        /// </summary>
        /// <param name="moduleTypeId"></param>
        /// <param name="roleID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetHtmlString(int moduleTypeId, string roleID = "", string userID = "")
        {
            //获得StringBuilder实例
            StringBuilder sbString = new StringBuilder();
            sbString.Append("<table id='ModuleType_" + moduleTypeId + "' style='width:100%;'><tr><td>父级</td><td>菜单名称</td><td>功能点</td></tr>");
            //根据菜单类型ID查询所有的菜单
            List<ModulesContract> list_Modules = modulesServices.GetModulesByTypeId(moduleTypeId);
            //查询所有的功能点
            List<AuthorityContract> list_Authority = authorityServices.GetAllAuthority();
            GetMenu(list_Modules, list_Authority, 0, sbString, roleID: roleID, userID: userID);
            sbString.Append("</table>");
            return sbString.ToString();
        }

        /// <summary>
        /// 权限配置
        /// </summary>
        /// <param name="list_Modules"></param>
        /// <param name="list_Authority"></param>
        /// <param name="moduleId"></param>
        /// <param name="id"></param>
        /// <param name="sbString"></param>
        private void GetMenu(List<ModulesContract> list_Modules, List<AuthorityContract> list_Authority, int moduleParentId, StringBuilder sbString, string roleID = "", string userID = "")
        {
            if (moduleParentId == 0)
            {
                List<ModulesContract> list_Module = list_Modules.Where(m => m.ModuleParentID == moduleParentId).ToList();
                foreach (ModulesContract item in list_Module)
                {
                    sbString.Append(string.Format("<tr><td class='alt_Left' style='text-align:left;'><font color='red'>根菜单</font></td><td class='alt_Left' style='text-align:left;'>{0}</td><td style='text-align:left;'>{1}</td></tr>", item.ModuleName, GetRoleAuthority(list_Authority, item.ModuleID, roleID: roleID, userID: userID)));
                    GetMenu(list_Modules, list_Authority, item.ModuleID, sbString, roleID: roleID, userID: userID);
                }
            }
            else
            {
                List<ModulesContract> list_Module = list_Modules.Where(m => m.ModuleParentID == moduleParentId).ToList();
                foreach (ModulesContract item in list_Module)
                {
                    sbString.Append(string.Format("<tr><td class='alt_Left' style='text-align:left;'>{0}</td><td class='alt_Left' style='text-align:left;'>{1}</td><td style='text-align:left;'>{2}</td></tr>", list_Modules.Where(m => m.ModuleID == moduleParentId).Single().ModuleName, item.ModuleName, GetRoleAuthority(list_Authority, item.ModuleID, roleID: roleID, userID: userID)));
                    GetMenu(list_Modules, list_Authority, item.ModuleID, sbString, roleID: roleID, userID: userID);
                }
            }
        }

        /// <summary>
        /// 查询当前用户或者角色默认绑定的功能点
        /// </summary>
        /// <param name="list_Authority"></param>
        /// <param name="moduleId"></param>
        /// <param name="roleID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        private string GetRoleAuthority(List<AuthorityContract> list_Authority, int moduleId, string roleID = "", string userID = "")
        {
            StringBuilder sbString = new StringBuilder();
            //获取页面拥有的功能点
            List<ModuleAuthorityListContract> list_ModuleAuthorityList = moduleAuthorityListServices.GetAuthorityByModuleID(moduleId);
            //获取当前角色或者用户拥有的功能点
            List<RoleAuthorityListContract> list_RoleAuthority = null;
            if (string.IsNullOrEmpty(userID))
            {
                list_RoleAuthority = roleAuthorityListServices.GetRoleAuthority(moduleId: moduleId, roleId: roleID);
            }
            else
            {
                list_RoleAuthority = roleAuthorityListServices.GetRoleAuthority(moduleId, userId: userID);
            }
            //循环生成菜单拥有的功能点，选中用户或者角色拥有的功能点
            for (int i = 0; i < list_ModuleAuthorityList.Count; i++)
            {
                if (list_Authority.Where(a => a.AuthorityID == list_ModuleAuthorityList[i].AuthorityID).Single().AuthorityState == 1)
                {
                    continue;
                }
                string chk = "";
                string enable = "";
                if (list_RoleAuthority.Where(a => a.AuthorityID == list_ModuleAuthorityList[i].AuthorityID && a.ModuleID == list_ModuleAuthorityList[i].ModuleID).Count() != 0)
                {
                    chk = "checked";
                }
                if (list_RoleAuthority.Where(a => a.AuthorityID == list_ModuleAuthorityList[i].AuthorityID && a.ModuleID == list_ModuleAuthorityList[i].ModuleID).Count() != 0 && !string.IsNullOrEmpty(userID) && list_RoleAuthority.Where(a => a.AuthorityID == list_ModuleAuthorityList[i].AuthorityID && a.ModuleID == list_ModuleAuthorityList[i].ModuleID && a.IsChange == 0).Count() != 0)
                {
                    enable = "disabled";
                }
                sbString.Append(string.Format("<input type='checkbox' id='{0}' name='{1}' value='{2}' {3} {4}/><label for='{5}' style='margin-right: 30px;margin-left: 3px;margin-bottom: 10px;'>{6}</label>", moduleId + "_" + i, "RoleAuthority", moduleId + "_" + list_ModuleAuthorityList[i].AuthorityID, chk, enable, moduleId + "_" + i, list_Authority.Where(a => a.AuthorityID == list_ModuleAuthorityList[i].AuthorityID).Single().AuthorityName));
                if (i % 8 == 0 && i != 0)
                {
                    sbString.Append("</br>");
                }
            }
            return sbString.ToString();
        }

        #endregion 针对角色的操作或者显示

        #region 公共方法

        /// <summary>
        /// 查询页面拥有的功能点权限
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public List<AuthorityContract> GetPageAuthority(string tag)
        {
            string filePath = Server.MapPath("/FileXML/" + (Session["user"] as UsersContract).UserName + "_xml");
            return new PublicBusinessMethodCore().GetPageAuthority<AuthorityContract>(filePath, tag);
        }

        #endregion 公共方法
    }
}