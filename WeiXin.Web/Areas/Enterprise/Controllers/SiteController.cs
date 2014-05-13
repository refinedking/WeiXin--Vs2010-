
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
using WeiXin.Core;
using WeiXin.BO;
using WeiXin.Model;
using WeiXin.BLL;
using WeiXin.Web.Controllers;

namespace WeiXin.Web.Areas.Enterprise.Controllers
{
    /// <summary>
    /// 企业管理控制器：主要处理企业网站的东西
    /// </summary>
    [WeiXinException]
    public class SiteController : CommonController
    {
        normal_channelService ChannelBll = new normal_channelService();//频道
        normal_ClassService ClassBll = new normal_ClassService();//栏目
        module_articleService ArticleBll = new module_articleService();//文章
        EmployeeService empBll = new EmployeeService();//企业微信
        UsersServices userBll = new UsersServices();//用户登录表
        normal_modulesService ModulesBll = new normal_modulesService();//模型
        module_productService ProBll = new module_productService();//产品
        normal_extendsService ExtendsBll = new normal_extendsService();//插件
        normal_EmpExtendsService EmpExtendsBll = new normal_EmpExtendsService();//企业使用的插件
        normal_templateService TBll = new normal_templateService();//模版

        #region 网站管理
        #region 1、频道管理

        #region 1、频道列表


        /// <summary>
        /// 频道列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ChannelList()
        {
            ViewData["list_PageAuthority"] = GetPageAuthority("ChannelList");
            PagerList<normal_channel> list = null;
            //查询模型
            if ((Session["user"] as UsersContract).RoleID == 1)
            {
                #region 系统管理员 获取所有企业频道信息
                list = ChannelBll.GetChannelByPager(pageIndex, pageSize, "");
                #endregion
            }
            else
            {
                #region 获取自己的频道信息
                list = ChannelBll.GetChannelByPager((Session["user"] as UsersContract).EId, pageIndex, pageSize, "");
                #endregion
            }
            ViewData["Model"] = ModulesBll.GetListModules();
            return View(list);
        }
        [HttpPost]
        public ActionResult ChannelList(FormCollection fc)
        {
            ViewData["Model"] = ModulesBll.GetListModules();
            ViewData["list_PageAuthority"] = GetPageAuthority("ChannelList");
            PagerList<normal_channel> list = null;
            if ((Session["user"] as UsersContract).RoleID == 1)
            {
                #region 系统管理员 获取所有企业频道信息
                list = ChannelBll.GetChannelByPager(int.Parse(fc["CurPage"]), pageSize, "");
                #endregion
            }
            else
            {
                #region 获取自己的频道信息
                list = ChannelBll.GetChannelByPager((Session["user"] as UsersContract).EId, int.Parse(fc["CurPage"]), pageSize, "");
                #endregion
            }
            return View(list);
        }

        #endregion

        #region 2、频道添加、修改
        /// <summary>
        /// 添加频道
        /// </summary>
        /// <returns></returns>
        public ActionResult AddChannel(int id)
        {
            var operateType = Request.QueryString["operateType"];
            string module = Request.QueryString["module"];
            ViewData["module"] = module;
            string type = Request.QueryString["type"];
            normal_modules Module = null;
            if (module != null && id == 0)
            {
                Module = ModulesBll.GetListModules(int.Parse(module));
            }
            else
            {
                Module = ModulesBll.GetListModules2(type);
            }

            ViewData["operate"] = operateType;
            normal_channelContract ChannelC = new normal_channelContract();
            #region 绑定 注：如果是企业自身编辑，这些数据是企业无法修改的，只能是超级管理员才能修改。
            List<employeeInfo> listEmp = new List<employeeInfo>();
            if ((Session["user"] as UsersContract).RoleID == 1)
            {
                //系统管理员 查询所有的企业
                listEmp = empBll.GetAllEmp();
            }
            else
            {
                //企业，查询企业自己
                listEmp = empBll.GetAllEmp((Session["user"] as UsersContract).EId);
            }
            ViewData["listEmp"] = new SelectList(listEmp, "Eid", "wxName");

            #endregion


            //频道默认页
            ViewData["Template"] = new SelectList(TBll.getTemplateByList((Session["user"] as UsersContract).EId, Module.Type, "channel"), "Id", "Title");

            ViewData["listModules"] = Module.Title + "模型";
            ChannelC.Type = Module.Type;
            switch (operateType)
            {
                case "edit":

                    ViewBag.pTitle = "编辑" + Module.Title + "频道"; ;
                    //查询频道信息
                    ChannelC = ChannelBll.GetChannelContractByidAndEid((Session["user"] as UsersContract).EId, id);
                    break;
                default:
                    ViewBag.pTitle = "添加" + Module.Title + "频道";
                    ChannelC.Enabled = 1;
                    ChannelC.ItemName = Module.ItemName;
                    ChannelC.ItemUnit = Module.ItemUnit;
                    break;

            }
            return View(ChannelC);
        }

        /// <summary>
        /// 添加频道
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddChannel(normal_channelContract ChannelC, FormCollection fc, string module)
        {
            #region 绑定 注：如果是企业自身编辑，这些数据是企业无法修改的，只能是超级管理员才能修改。
            List<employeeInfo> listEmp = new List<employeeInfo>();
            if ((Session["user"] as UsersContract).RoleID == 1)
            {
                //系统管理员 查询所有的企业
                listEmp = empBll.GetAllEmp();
            }
            else
            {
                //企业，查询企业自己
                listEmp = empBll.GetAllEmp((Session["user"] as UsersContract).EId);
            }
            ViewData["listEmp"] = new SelectList(listEmp, "Eid", "wxName");

            #endregion
            ViewData["listModules"] = new SelectList(ModulesBll.GetListModules(), "Type", "Title");
            normal_modules Module = ModulesBll.GetListModules(int.Parse(module));
            //频道默认页
            ViewData["Template"] = new SelectList(TBll.getTemplateByList((Session["user"] as UsersContract).EId, Module.Type, "channel"), "Id", "Title");

            ViewData["listModules"] = Module.Title + "模型";
            var operateType = Request.QueryString["operateType"];
            if (ModelState.IsValid)
            {
                ChannelC.Type = Module.Type;
                ChannelC.Dir = "";
                ChannelC.IsTop = 0;
                ChannelC.LanguageCode = "cn";
                ChannelC.UploadPath = "";
                ChannelC.UploadType = "*.jpg;*.gif;*.png";
                ChannelC.Enabled = int.Parse(fc["Enable"]);
                switch (operateType)
                {
                    case "edit":
                        //Edit 
                        if (ChannelBll.UpdateChannel(ChannelC) > 0)
                        {
                            // Success 
                            ViewData["msg"] = "修改" + Module.Title + "模型成功!";
                            ViewData["url"] = Url.Action("ChannelList");
                            return View("Success");
                        }
                        else
                        {
                            return View(ChannelC);
                        }

                    default:
                        if (ChannelBll.InsertChannel(ChannelC) > 0)
                        {
                            // Success 
                            ViewData["msg"] = "添加" + Module.Title + "模型成功!";
                            ViewData["url"] = Url.Action("ChannelList");
                            return View("Success");
                        }
                        else
                        {
                            return View(ChannelC);
                        }
                }
            }
            else
            {
                return View(ChannelC);
            }
        }


        #endregion

        #region 3、删除频道
        /// <summary>
        /// 删除频道
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteChannel(int id)
        {
            if ((Session["user"] as UsersContract).RoleID == 1)
            {
                ViewData["msg"] = "管理员不能删除!";
                ViewData["url"] = Url.Action("ChannelList");
                return View("Error");
            }
            else
            {
                if (ChannelBll.DeleteChannel(id, (Session["user"] as UsersContract).EId) > 0)
                {
                    ViewData["msg"] = "删除成功!";
                    ViewData["url"] = Url.Action("ChannelList");
                    return View("Success");
                }
                else
                {
                    ViewData["msg"] = "删除失败!";
                    ViewData["url"] = Url.Action("ChannelList");
                    return View("Error");
                }
            }
        }
        #endregion

        #endregion

        #region  2、栏目管理

        #region 1、列表

        /// <summary>
        /// 栏目管理
        /// </summary> 
        /// <returns></returns>
        public ActionResult ClassList()
        {
            ViewData["list_PageAuthority"] = GetPageAuthority("ArticleIndex");

            int ccid = int.Parse(Request.QueryString["ccid"]);
            ViewData["ccid"] = ccid;
            //查询该频道下的栏目 
            return View(ClassBll.GetClassByPager(ccid, (Session["user"] as UsersContract).EId, pageIndex, pageSize, ""));
        }

        /// <summary>
        /// 栏目管理
        /// </summary> 
        /// <returns></returns>
        [HttpPost]
        public ActionResult ClassList(FormCollection fc)
        {
            ViewData["list_PageAuthority"] = GetPageAuthority("ArticleIndex");

            int ccid = int.Parse(Request.QueryString["ccid"]);
            ViewData["ccid"] = ccid;
 
            ViewData["keyWords"] = fc["keyWords"];

            return View(ClassBll.GetClassByPager(ccid, (Session["user"] as UsersContract).EId, int.Parse(fc["CurPage"]), pageSize, fc["keyWords"]));
        }

        #endregion

        #region 2、添加、修改
        public ActionResult AddClass(int id)
        {
            string ccid = Request.QueryString["ccid"];
            ViewData["ccid"] = ccid;
            string operateType = Request.QueryString["operateType"];
            normal_classContract classC = new normal_classContract();
            #region List
            //在“当前页位置”导航显示
            var IsTop = new string[] { "否", "是" };
            var IsTopList = new List<object> { };
            for (int i = 0; i < IsTop.Length; i++)
            {
                var item = new { text = IsTop[i], value = i };
                IsTopList.Add(item);
            }
            ViewData["IsTop"] = new SelectList(IsTopList, "value", "text");
            #endregion
            //查询ccid 频道的信息
            int eid = (Session["user"] as UsersContract).EId;
            normal_channelContract channel = ChannelBll.GetChannelContractByidAndEid(eid, int.Parse(ccid));
            //栏目模版页
            ViewData["ClassTemp"] = new SelectList(TBll.getTemplateByList(eid, channel.Type, "class"), "Id", "Title"); 
            //内容模版页
            ViewData["ContentTemp"] = new SelectList(TBll.getTemplateByList(eid, channel.Type, "content"), "Id", "Title");  
            switch (operateType)
            {
                case "add":
                    ViewBag.pTitle = "添加栏目";
                    classC.PageSize = 5;
                    break;
                default:
                    ViewBag.pTitle = "修改栏目";
                    if (id != 0)
                    {
                        //查询
                        classC = ClassBll.GetClassByIdAndEid(id, (Session["user"] as UsersContract).EId);
                    }
                    else
                    {
                        ViewBag.pTitle = "添加栏目";
                    }
                    break;
            }
            return View(classC);

        }

        [HttpPost]
        public ActionResult AddClass(normal_classContract ClassC, FormCollection fc)
        {
            string ccid = Request.QueryString["ccid"];
            ViewData["ccid"] = ccid;
            string operateType = Request.QueryString["operateType"];
            #region List
            //在“当前页位置”导航显示
            var IsTop = new string[] { "否", "是" };
            var IsTopList = new List<object> { };
            for (int i = 0; i < IsTop.Length; i++)
            {
                var item = new { text = IsTop[i], value = i };
                IsTopList.Add(item);
            }
            ViewData["IsTop"] = new SelectList(IsTopList, "value", "text");
            #endregion
            int eid = (Session["user"] as UsersContract).EId;
            normal_channelContract channel = ChannelBll.GetChannelContractByidAndEid(eid, int.Parse(ccid));
            //栏目模版页
            ViewData["ClassTemp"] = new SelectList(TBll.getTemplateByList(eid, channel.Type, "class"), "Id", "Title"); ;
            //内容模版页
            ViewData["ContentTemp"] = new SelectList(TBll.getTemplateByList(eid, channel.Type, "content"), "Id", "Title");  
            if (ModelState.IsValid)
            {
                ClassC.Info = "";
                ClassC.Keywords = "";
                ClassC.Img = "";
                ClassC.Content = "";
                ClassC.Folder = "";
                ClassC.Code = "";
                ClassC.FilePath = "";
                ClassC.FirstPage = "";
                ClassC.AliasPage = "";
                ClassC.ChannelId = int.Parse(ccid);
                switch (operateType)
                {
                    case "add":
                        if (ClassBll.InsertClass(ClassC) > 0)
                        {
                            // Success 
                            ViewData["msg"] = "添加成功!";
                            ViewData["url"] = Url.Action("ClassList", new { ccid = ccid });
                            return View("Success");
                        }
                        else
                        {
                            return View(ClassC);
                        }

                    default:
                        //Edit 
                        if (ClassBll.UpdateClass(ClassC) > 0)
                        {
                            // Success 
                            ViewData["msg"] = "修改成功!";
                            ViewData["url"] = Url.Action("ClassList", new { ccid = ccid });
                            return View("Success");
                        }
                        else
                        {
                            return View(ClassC);
                        }
                }
            }
            else
            {
                return View(ClassC);
            }
        }
        #endregion

        #region 删除


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteClass(int id)
        {
            string ccid = Request.QueryString["ccid"];
            if ((Session["user"] as UsersContract).RoleID == 1)
            {
                ViewData["msg"] = "管理员不能删除!";
                ViewData["url"] = Url.Action("ClassList", new { ccid = ccid });
                return View("Error");
            }
            else
            {

                if (ClassBll.DeleteClass(id, (Session["user"] as UsersContract).EId) > 0)
                {
                    ViewData["msg"] = "删除成功!";
                    ViewData["url"] = Url.Action("ClassList", new { ccid = ccid });
                    return View("Success");
                }
                else
                {
                    ViewData["msg"] = "删除失败!";
                    ViewData["url"] = Url.Action("ClassList", new { ccid = ccid });
                    return View("Error");
                }
            }
        }
        #endregion
        #endregion

        #region 3、内容管理
        /// <summary>
        /// 文章管理 左侧频道列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ArticleIndex()
        {
            ViewData["list_PageAuthority"] = GetPageAuthority("ArticleIndex");
            //查询该企业的频道
            List<normal_channel> list = null;
            if ((Session["user"] as UsersContract).RoleID == 1)
            {
                #region 系统管理员 获取所有企业频道信息
                list = ChannelBll.GetChannelByList();
                #endregion
            }
            else
            {
                #region 获取自己的频道信息
                list = ChannelBll.GetChannelByList((Session["user"] as UsersContract).EId);
                #endregion
            }
            return View(list);

        }
        #region Product 产品、商品

        #region 1、产品列表


        /// <summary>
        /// 产品列表
        /// </summary>
        /// <param name="ccid"></param>
        /// <returns></returns>
        public ActionResult ProductList(int ccid)
        {
            ViewData["list_PageAuthority"] = GetPageAuthority("ArticleIndex");
            ViewData["ccid"] = ccid;
            return View(ProBll.GetProductByPager(ccid, pageIndex, pageSize, ""));
        }

        [HttpPost]
        public ActionResult ProductList(FormCollection fc, int ccid)
        {
            ViewData["ccid"] = ccid;
            ViewData["list_PageAuthority"] = GetPageAuthority("ArticleIndex");

            //处理输入数据时候的异常
            if (!int.TryParse(fc["CurPage"], out pageIndex))
            {
                pageIndex = 1;
            }
            else if (int.Parse(fc["CurPage"]) < 0)
            {
                pageIndex = 1;
            }
            else
            {
                int nowPageIndex = int.Parse(fc["curPage"]);
                if (fc["keyWords"] == "")
                {
                    if (nowPageIndex != 0)
                    {
                        pageIndex = nowPageIndex;
                    }
                    else
                    {
                        pageIndex = 1;
                    }
                }
                else if (fc["keyWords"] != "" && nowPageIndex == 0)
                {
                    pageIndex = 1;
                }
            }
            ViewData["keyWords"] = fc["keyWords"];
            return View(ProBll.GetProductByPager(ccid, pageIndex, pageSize, fc["keyWords"]));
        }
        #endregion

        #region 2、添加、编辑商品
        /// <summary>
        /// 添加、编辑文章
        /// </summary>
        /// <param name="ccid">频道ID</param>
        /// <returns></returns>
        public ActionResult ProductEdit(int id, int ccid)
        {
            ViewData["ccid"] = ccid;
            string operateType = Request.QueryString["operateType"];
            module_productContract Product = new module_productContract();
            //查询该频道下的栏目
            List<normal_class> Classlist = ClassBll.GetClassByccidAndEid(ccid, (Session["user"] as UsersContract).EId);
            if (Classlist.Count == 0)
            {
                ViewData["msg"] = "该频道下还没有栏目，请先添加栏目!";
                ViewData["url"] = Url.Action("AddClass", new { ccid = ccid, id = 0, operateType = "add" });
                return View("Error");
            }
            else
            {
                ViewData["ClassList"] = new SelectList(Classlist, "id", "title");
                switch (operateType)
                {
                    case "add":
                        ViewBag.pTitle = "添加商品";
                        Product.AddDate = DateTime.Now;
                        //添加 
                        break;
                    case "edit":
                        Product = ProBll.GetProductByIdAndccid(ccid, id);
                        ViewData["content"] = Product.Content;
                        break;
                    default:
                        break;
                }
                return View(Product);
            }
        }

        /// <summary>
        /// 添加、编辑商品
        /// </summary>
        /// <param name="ArticleC"></param>
        /// <param name="fc"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)] //目的是为了防止在提交时报“检测到有潜在危险的客户端输入值”
        public ActionResult ProductEdit(module_productContract ProductC, FormCollection fc, int ccid, int id)
        {
            ViewData["ccid"] = ccid;
            string operateType = Request.QueryString["operateType"];
            if (ModelState.IsValid)
            {
                if (ProductC.Summary == null || ProductC.Summary.Trim() == "")
                    ProductC.Summary = CommonHelper.GetCutString(CommonHelper.NoHTML(ProductC.Content), 100);
                else
                    ProductC.Summary = CommonHelper.GetCutString(CommonHelper.NoHTML(ProductC.Summary), 100);
                if (ProductC.Tags == null)
                    ProductC.Tags = "";
                else
                    ProductC.Tags = CommonHelper.DelSymbol(ProductC.Tags);
                if (ProductC.TColor == null)
                    ProductC.TColor = "";
                if (ProductC.Author == null)
                    ProductC.Author = "";
                if (ProductC.Img == null)
                {
                    ProductC.Img = "";
                    ProductC.IsImg = 0;
                }
                else ProductC.IsImg = 1;
                ProductC.IsPass = 1;
                if (ProductC.SourceFrom == null)
                    ProductC.SourceFrom = "";
                ProductC.Title = CommonHelper.SafetyTitle(ProductC.Title);
                ProductC.ChannelId = ccid;
                ProductC.Editor = ProductC.Author;
                ProductC.FirstPage = "";
                ProductC.AliasPage = "";
                switch (operateType)
                {
                    case "add":
                        #region Add
                        ProductC.AddDate = DateTime.Now;
                        if (ProBll.InsertProduct(ProductC) > 0)
                        {

                            ViewData["msg"] = "添加成功!";
                            ViewData["url"] = Url.Action("ProductList", new { ccid = ccid });
                            return View("Success");
                        }
                        else
                        {
                            return View(ProductC);
                        }
                        #endregion
                    case "edit":
                        if (ProBll.UpdateProduct(ProductC) > 0)
                        {
                            // Success 
                            ViewData["msg"] = "修改成功!";
                            ViewData["url"] = Url.Action("ProductList", new { ccid = ccid });
                            return View("Success");
                        }
                        else
                        {
                            return View(ProductC);
                        }

                    default:
                        break;
                }
                return View(ProductC);
            }
            else
            {
                return View(ProductC);
            }

        }
        #endregion

        #region 3、删除文章
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <param name="ccid">频道ID</param>
        /// <returns></returns>
        public ActionResult DeleteProduct(int id, int ccid)
        {
            if (ProBll.DeleteProduct(id, ccid) > 0)
            {
                ViewData["msg"] = "删除成功!";
                ViewData["url"] = Url.Action("ProductList", new { ccid = ccid });
                return View("Success");
            }
            else
            {
                ViewData["msg"] = "删除失败!";
                ViewData["url"] = Url.Action("ArticleList", new { ccid = ccid });
                return View("Error");
            }
        }
        #endregion

        #region 4、克隆
        public ActionResult CopyProduct(int ccid, int id)
        {
            //1、查询
            module_productContract Product = new module_productContract();
            Product = ProBll.GetProductByIdAndccid(ccid, id);
            Product.AddDate = DateTime.Now;
            Product.IsPass = 1;
            if (ProBll.InsertProduct(Product) > 0)
            {
                // Success 
                ViewData["msg"] = "克隆成功!";
                ViewData["url"] = Url.Action("ProductList", new { ccid = ccid });
                return View("Success");
            }
            else
            {
                ViewData["msg"] = "克隆失败!";
                ViewData["url"] = Url.Action("ProductList", new { ccid = ccid });
                return View("Error");
            }
        }
        #endregion

        #region 5、执行批量操作
        public ActionResult BatchOperProduct()
        {
            string act = Request.QueryString["act"];
            string ids = Request.QueryString["ids"];
            string ccid = Request.QueryString["ccid"];
            switch (act)
            {
                case "top":
                    ProBll.UpdateProduct("IsTop", 1, int.Parse(ids));
                    break;
                case "notop":

                    ProBll.UpdateProduct("IsTop", 0, int.Parse(ids));
                    break;
                case "focus":

                    ProBll.UpdateProduct("IsFocus", 1, int.Parse(ids));
                    break;
                case "nofocus":

                    ProBll.UpdateProduct("IsFocus", 0, int.Parse(ids));

                    break;
                case "head":
                    ProBll.UpdateProduct("IsHead", 1, int.Parse(ids));

                    break;
                case "nohead":
                    ProBll.UpdateProduct("IsHead", 0, int.Parse(ids));
                    break;
                case "sdel":
                    ProBll.UpdateProduct("IsPass", -1, int.Parse(ids));
                    break;
                default:
                    break;
            }
            ViewData["msg"] = "更新成功!";
            ViewData["url"] = Url.Action("ProductList", new { ccid = ccid });
            return View("Success");

        }
        #endregion
        #endregion

        #region Article 文章、新闻


        #region 1、文章列表


        /// <summary>
        /// 文章管理 
        /// </summary>
        /// <returns></returns>
        public ActionResult ArticleList(int ccid)
        {
            ViewData["list_PageAuthority"] = GetPageAuthority("ArticleIndex");
            ViewData["ccid"] = ccid;
            return View(ArticleBll.GetArticleByPager(ccid, pageIndex, pageSize, ""));

        }

        [HttpPost]
        public ActionResult ArticleList(FormCollection fc, int ccid)
        {
            ViewData["ccid"] = ccid;
            //  return View(ArticleBll.GetArticleByPager(ccid, pageIndex, pageSize, ""));
            ViewData["list_PageAuthority"] = GetPageAuthority("ArticleIndex");

            //处理输入数据时候的异常
            if (!int.TryParse(fc["CurPage"], out pageIndex))
            {
                pageIndex = 1;
            }
            else if (int.Parse(fc["CurPage"]) < 0)
            {
                pageIndex = 1;
            }
            else
            {
                int nowPageIndex = int.Parse(fc["curPage"]);
                if (fc["keyWords"] == "")
                {
                    if (nowPageIndex != 0)
                    {
                        pageIndex = nowPageIndex;
                    }
                    else
                    {
                        pageIndex = 1;
                    }
                }
                else if (fc["keyWords"] != "" && nowPageIndex == 0)
                {
                    pageIndex = 1;
                }
            }
            ViewData["keyWords"] = fc["keyWords"];
            return View(ArticleBll.GetArticleByPager(ccid, pageIndex, pageSize, fc["keyWords"]));
        }
        #endregion

        #region 2、添加、编辑文章
        /// <summary>
        /// 添加、编辑文章
        /// </summary>
        /// <param name="ccid">频道ID</param>
        /// <returns></returns>
        public ActionResult ArticleEdit(int id, int ccid)
        {
            ViewData["ccid"] = ccid;
            string operateType = Request.QueryString["operateType"];
            module_articleContract Article = new module_articleContract();
            //查询该频道下的栏目
            List<normal_class> Classlist = ClassBll.GetClassByccidAndEid(ccid, (Session["user"] as UsersContract).EId);
            if (Classlist.Count == 0)
            {
                ViewData["msg"] = "该频道下还没有栏目，请先添加栏目!";
                ViewData["url"] = Url.Action("AddClass", new { ccid = ccid, id = 0, operateType = "add" });
                return View("Error");
            }
            else
            {
                ViewData["ClassList"] = new SelectList(Classlist, "id", "title");
                switch (operateType)
                {
                    case "add":
                        ViewBag.pTitle = "添加文章";
                        Article.AddDate = DateTime.Now;
                        //添加 
                        break;
                    case "edit":
                        Article = ArticleBll.GetArticleByIdAndccid(ccid, id);
                        ViewData["content"] = Article.Content;
                        break;
                    default:
                        break;
                }
                return View(Article);
            }
        }

        /// <summary>
        /// 添加、编辑文章
        /// </summary>
        /// <param name="ArticleC"></param>
        /// <param name="fc"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)] //目的是为了防止在提交时报“检测到有潜在危险的客户端输入值”
        public ActionResult ArticleEdit(module_articleContract ArticleC, FormCollection fc, int ccid, int id)
        {
            ViewData["ccid"] = ccid;
            string operateType = Request.QueryString["operateType"];
            if (ModelState.IsValid)
            {
                if (ArticleC.Summary == null || ArticleC.Summary.Trim() == "")
                    ArticleC.Summary = CommonHelper.GetCutString(CommonHelper.NoHTML(ArticleC.Content), 100);
                else
                    ArticleC.Summary = CommonHelper.GetCutString(CommonHelper.NoHTML(ArticleC.Summary), 100);
                if (ArticleC.Tags == null)
                    ArticleC.Tags = "";
                else
                    ArticleC.Tags = CommonHelper.DelSymbol(ArticleC.Tags);
                if (ArticleC.TColor == null)
                    ArticleC.TColor = "";
                if (ArticleC.Author == null)
                    ArticleC.Author = "";
                if (ArticleC.Img == null)
                {
                    ArticleC.Img = "";
                    ArticleC.IsImg = 0;
                }
                else ArticleC.IsImg = 1;
                ArticleC.IsPass = 1;
                if (ArticleC.SourceFrom == null)
                    ArticleC.SourceFrom = "";
                ArticleC.Title = CommonHelper.SafetyTitle(ArticleC.Title);
                ArticleC.ChannelId = ccid;
                ArticleC.Editor = ArticleC.Author;
                ArticleC.FirstPage = "";
                ArticleC.AliasPage = "";
                switch (operateType)
                {
                    case "add":
                        #region Add
                        ArticleC.AddDate = DateTime.Now;
                        if (ArticleBll.InsertArticle(ArticleC) > 0)
                        {
                            // Success 
                            ViewData["msg"] = "添加成功!";
                            ViewData["url"] = Url.Action("ArticleList", new { ccid = ccid });
                            return View("Success");
                        }
                        else
                        {
                            return View(ArticleC);
                        }
                        #endregion
                    case "edit":
                        if (ArticleBll.UpdateArticle(ArticleC) > 0)
                        {
                            // Success 
                            ViewData["msg"] = "修改成功!";
                            ViewData["url"] = Url.Action("ArticleList", new { ccid = ccid });
                            return View("Success");
                        }
                        else
                        {
                            return View(ArticleC);
                        }

                    default:
                        break;
                }
                return View();
            }
            else
            {
                return View(ArticleC);
            }

        }
        #endregion

        #region 3、删除文章


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ccid"></param>
        /// <returns></returns>
        public ActionResult DeleteArticle(int id, int ccid)
        {
            if (ArticleBll.DeleteArticle(id, ccid) > 0)
            {
                ViewData["msg"] = "删除成功!";
                ViewData["url"] = Url.Action("ArticleList", new { ccid = ccid });
                return View("Success");
            }
            else
            {
                ViewData["msg"] = "删除失败!";
                ViewData["url"] = Url.Action("ArticleList", new { ccid = ccid });
                return View("Error");
            }
        }
        #endregion

        #region 4、克隆
        public ActionResult CopyArticle(int ccid, int id)
        {
            //1、查询
            module_articleContract article = new module_articleContract();
            article = ArticleBll.GetArticleByIdAndccid(ccid, id);
            article.AddDate = DateTime.Now;
            article.IsPass = 1;
            if (ArticleBll.InsertArticle(article) > 0)
            {
                // Success 
                ViewData["msg"] = "克隆成功!";
                ViewData["url"] = Url.Action("ArticleList", new { ccid = ccid });
                return View("Success");
            }
            else
            {
                ViewData["msg"] = "克隆成功!";
                ViewData["url"] = Url.Action("ArticleList", new { ccid = ccid });
                return View("Error");
            }
        }
        #endregion

        #region 5、执行批量操作
        public ActionResult BatchOper()
        {
            string act = Request.QueryString["act"];
            string ids = Request.QueryString["ids"];
            string ccid = Request.QueryString["ccid"];
            switch (act)
            {
                case "top":

                    ArticleBll.UpdateArticle("IsTop", 1, int.Parse(ids));

                    break;
                case "notop":

                    ArticleBll.UpdateArticle("IsTop", 0, int.Parse(ids));
                    break;
                case "focus":

                    ArticleBll.UpdateArticle("IsFocus", 1, int.Parse(ids));
                    break;
                case "nofocus":

                    ArticleBll.UpdateArticle("IsFocus", 0, int.Parse(ids));

                    break;
                case "head":

                    ArticleBll.UpdateArticle("IsHead", 1, int.Parse(ids));

                    break;
                case "nohead":

                    ArticleBll.UpdateArticle("IsHead", 0, int.Parse(ids));

                    break;
                case "sdel":

                    ArticleBll.UpdateArticle("IsPass", -1, int.Parse(ids));
                    break;
                default:
                    break;
            }
            ViewData["msg"] = "更新成功!";
            ViewData["url"] = Url.Action("ArticleList", new { ccid = ccid });
            return View("Success");

        }
        #endregion
        #endregion

        #endregion

        #region 4、模版管理

        #region 1、模版列表
        /// <summary>
        /// 模版列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Template()
        {
            ViewData["list_PageAuthority"] = GetPageAuthority("Template");
            ViewData["Model"] = ModulesBll.GetListModules();
            //查询所有的模版（自身模版+公用模版：公用模版不允许编辑） 
            return View(TBll.GetTemplateByPage(pageIndex, pageSize, (Session["user"] as UsersContract).EId));
        }

        [HttpPost]
        public ActionResult Template(FormCollection fc)
        {
            ViewData["list_PageAuthority"] = GetPageAuthority("Template");
            ViewData["Model"] = ModulesBll.GetListModules();
            //查询所有的模版（自身模版+公用模版：公用模版不允许编辑） 
            return View(TBll.GetTemplateByPage(int.Parse(fc["CurPage"]), pageSize, (Session["user"] as UsersContract).EId));
        }

        #endregion
        #region 2、添加模版
        /// <summary>
        /// 添加模版
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="stype"></param>
        /// <returns></returns>
        public ActionResult Template_Edit(int id, string type, string stype)
        {
            normal_templateContract NTC = new normal_templateContract();
            if (id == 0)
            {
                //添加
                NTC.SType = stype;
                NTC.Type = type;

            }
            else
            {
                //修改
                NTC = TBll.GetTemplateById(id, (Session["user"] as UsersContract).EId);
            }
            return View(NTC);
        }

        [HttpPost]
        [ValidateInput(false)] //目的是为了防止在提交时报“检测到有潜在危险的客户端输入值”
        public ActionResult Template_Edit(FormCollection fc, normal_templateContract t, int id, string type, string stype)
        {
            if (ModelState.IsValid)
            {
                t.Type = type;
                t.SType = stype;
                t.IsDefault = 0;
                t.content = t.content.Replace("'", "\"");
                t.eid = (Session["user"] as UsersContract).EId;
                if (id == 0)
                {
                    //添加 
                    if (TBll.Insert(t) > 0)
                    {
                        // Success 
                        ViewData["msg"] = "添加成功!";
                        ViewData["url"] = Url.Action("Template");
                        return View("Success");
                    }
                    else
                    {
                        return View(t);
                    }
                }
                else
                {
                    if (TBll.Update(t) == 1)
                    {
                        ViewData["msg"] = "修改成功!";
                        ViewData["url"] = Url.Action("Template");
                        return View("Success");
                    }
                    else
                    {
                        return View(t);
                    }
                }
            }

            return View(t);
        }
        #endregion
        #region 3、设置默认模版
        /// <summary>
        /// 设置模版为默认
        /// </summary>
        /// <param name="id">模版ID</param>
        /// <param name="type"></param>
        /// <param name="stype"></param>
        /// <returns></returns>
        public ActionResult SetDef(int id, string type, string stype)
        {
            //1、该模版类型的其他模版为非默认，状态改为0
            //默认为1，非默认为0
            try
            {
                TBll.UpdateTemplateSetDef(type, stype, (Session["user"] as UsersContract).EId);
                TBll.UpdateTemplateSetDef(id, (Session["user"] as UsersContract).EId);
                ViewData["msg"] = "设置成功!";
                ViewData["url"] = Url.Action("Template");
                return View("Success");
            }
            catch (Exception)
            {

                ViewData["msg"] = "设置失败!";
                ViewData["url"] = Url.Action("Template");
                return View("Error");

            }
            return View();
        }
        #endregion

        public ActionResult Delete(int id)
        {
            try
            {
                TBll.Delete((Session["user"] as UsersContract).EId, id);
                ViewData["msg"] = "删除成功!";
                ViewData["url"] = Url.Action("Template");
                return View("Success");
            }
            catch (Exception)
            {
                ViewData["msg"] = "删除失败!";
                ViewData["url"] = Url.Action("Template");
                return View("Error");
            }
        }
        #endregion
        #endregion

        #region 后台管理

        #region 1、修改密码


        /// <summary>
        /// 修改我的密码
        /// </summary>
        /// <returns></returns>
        public ActionResult myinfo_password()
        {
            return View();
        }

        /// <summary>
        /// 修改我的密码
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult myinfo_password(FormCollection fc)
        {
            string _oldPass = fc["txtOldPass"];
            string _NewPass = fc["txtNewPass1"];
            UsersContract uc = Session["user"] as UsersContract;
            uc = userBll.GetUserEntity(uc.UserID.ToString());
            //1.判断旧密码是否正确
            //2.修改密码
            if (uc.Password == WeiXin.Core.SecurityEncryption.DESEncrypt(_oldPass))
            {
                if (userBll.UpdatePwd(uc.UserName, _NewPass))
                {
                    ViewData["msg"] = "修改成功!";
                    ViewData["url"] = Url.Action("myinfo_password");
                    return View("Success");
                }
                else
                {

                    ViewData["msg"] = "修改失败!";
                    ViewData["url"] = Url.Action("myinfo_password");
                    return View("Error");
                }

            }
            else
            {
                //密码错误
                ViewData["msg"] = "旧密码错误!";
                ViewData["url"] = Url.Action("myinfo_password");
                return View("Error");
            }

        }
        #endregion

        #region 2、插件管理
        #region 1、插件列表

        /// <summary>
        ///  插件列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Extends()
        {
            ViewData["list_PageAuthority"] = GetPageAuthority("Extends");
            return View(ExtendsBll.GetExtendsByPager(pageIndex, pageSize, ""));
        }
        /// <summary>
        /// 插件列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Extends(FormCollection fc)
        {
            ViewData["list_PageAuthority"] = GetPageAuthority("Extends");
            return View(ExtendsBll.GetExtendsByPager(int.Parse(fc["curPage"]), pageSize, fc["keyWords"]));
        }

        #endregion

        #region 2、添加、编辑插件

        /// <summary>
        /// 添加插件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ExtendsEdit(int id)
        {
            string operateType = Request.QueryString["operateType"];
            normal_extendsContract NEC = new normal_extendsContract();

            var typeName = new string[] { "生活服务", "休闲娱乐", "教育学习", "营销活动", "即将推出" };
            var typeNameList = new List<object> { };
            for (int i = 0; i < typeName.Length; i++)
            {
                var item = new { text = typeName[i], value = typeName[i] };
                typeNameList.Add(item);
            }

            switch (operateType)
            {
                case "add":
                    NEC.Enabled = 1; 
                    ViewData["Name"] = new SelectList(typeNameList, "value", "text");
                    break;
                default:
                    NEC = ExtendsBll.GetExtendsById(id); ViewData["Name"] = new SelectList(typeNameList, "value", "text", NEC.Name);
                    break;
            }
            return View(NEC);
        }

        [HttpPost]
        public ActionResult ExtendsEdit(int id, normal_extendsContract nec, FormCollection fc)
        {
            string operateType = Request.QueryString["operateType"];
            switch (operateType)
            {
                case "add":
                    if (ExtendsBll.Insert(nec) > 0)
                    {
                        ViewData["msg"] = "添加成功!";
                        ViewData["url"] = Url.Action("Extends");
                        return View("Success");
                    }
                    else
                    {
                        return View(nec);
                    }
                default:
                    if (ExtendsBll.Update(nec) > 0)
                    {
                        ViewData["msg"] = "修改成功!";
                        ViewData["url"] = Url.Action("Extends");
                        return View("Success");
                    }
                    else
                    {
                        return View(nec);
                    }
            }
        }

        #endregion

        #region 3、删除插件


        public ActionResult DeleteExtends(int id)
        {
            try
            {
                ExtendsBll.Delete(id); ViewData["msg"] = "删除成功!";
                ViewData["url"] = Url.Action("Extends");
                return View("Success");

            }
            catch (Exception)
            {

                ExtendsBll.Delete(id); ViewData["msg"] = "删除失败!";
                ViewData["url"] = Url.Action("Extends");
                return View("Error");
            }

        }
        #endregion

        #region 4、插件配置
        /// <summary>
        /// 配置插件的使用方式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PeiZhiExtends(int extid)
        {
            //查询该插件的信息
            normal_extends ne = ExtendsBll.GetNormal_ExtendsById(extid);
            normal_EmpExtendsContract neec = new normal_EmpExtendsContract();
            normal_EmpExtends nee = EmpExtendsBll.GetEmpExtendsByExtendsID2(extid, (Session["user"] as UsersContract).EId);
            if (nee != null)
            {
                neec.Id = nee.Id;
                neec.Info = nee.Info;
                neec.Enabled = int.Parse(nee.Enabled.ToString());//1启用，0禁用
            }
            else
            {
                neec.Info = ne.Info;
                neec.Enabled = 0;//1启用，0禁用
                neec.Id = 0;
            }

            ViewData["title"] = ne.Title;
            neec.extendID = extid;

            return View(neec);
        }

        [HttpPost]
        public ActionResult PeiZhiExtends(int extid, normal_EmpExtendsContract neec)
        {
            neec.eid = (Session["user"] as UsersContract).EId;
            if (neec.Id == 0)
            {
                //添加
                if (EmpExtendsBll.Insert(neec) > 0)
                {
                    ViewData["msg"] = "保存成功!";
                    ViewData["url"] = Url.Action("Extends");
                    return View("Success");
                }
                else
                {
                    return View(neec);
                }
            }
            else
            {
                //修改 
                if (EmpExtendsBll.Update(neec) > 0)
                {
                    ViewData["msg"] = "保存成功!";
                    ViewData["url"] = Url.Action("Extends");
                    return View("Success");
                }
                else
                {
                    return View(neec);
                }
            }

        }
        #endregion
        #endregion
        #endregion
    }
}
