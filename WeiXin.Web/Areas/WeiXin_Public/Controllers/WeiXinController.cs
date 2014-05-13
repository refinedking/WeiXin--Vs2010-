
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
using System.Data;
using WeiXin.Model;
using System.Collections;
using System.IO;
using System.Globalization;
using WeiXin.Web.Controllers;

namespace WeiXin.Web.Areas.WeiXin_Public.Controllers
{
    [WeiXinException]
    public class WeiXinController : CommonController
    {

        //行业逻辑对象
        BranchService bs = new BranchService();
        //职位业务逻辑对象
        PositionService ps = new PositionService();
        //员工业务逻辑对象
        EmployeeService es = new EmployeeService();

        //关键字业务逻辑对象
        GuanJianZiService gs = new GuanJianZiService();

        //关键字回复业务逻辑对象
        GuanJianZiHuiFuService hfs = new GuanJianZiHuiFuService();

        //角色
        RolesServices rolesServices = new RolesServices();
        //企业店铺信息
        employeeDataService empDataBll = new employeeDataService();
        #region 微信行业管理
        #region 数据列表
        /// <summary>
        /// 行业数据显示(首次加载)
        /// </summary>
        /// <returns></returns>
        public ActionResult BranchMain()
        {
            PageHelper<BranchInfoContract> branchList = new PageHelper<BranchInfoContract>(bs.GetBranchByPager("proc_GetBranchPager", pageIndex, pageSize, ""), pageIndex, pageSize, bs.GetAllBranchCount(""));
            ViewData["list_PageAuthority"] = GetPageAuthority("BranchMain");
            return View(branchList);
        }

        /// <summary>
        /// 行业数据显示(分页数据)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BranchMain(FormCollection fc)
        {
            ViewData["list_PageAuthority"] = GetPageAuthority("BranchMain");
            //处理输入数据时候的异常
            if (!int.TryParse(fc["CurPage"], out pageIndex))
            {
                //throw new Exception("输入的页码有误！");
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
            ViewData["BranchName"] = fc["keyWords"];
            PageHelper<BranchInfoContract> branchList =
                new PageHelper<BranchInfoContract>(bs.GetBranchByPager("proc_GetBranchPager",
                    pageIndex, pageSize, fc["keyWords"].Trim()), pageIndex, pageSize,
                    bs.GetAllBranchCount(fc["keyWords"].Trim()));

            return View(branchList);
        }

        #endregion

        #region 添加和编辑行业信息
        /// <summary>
        /// 添加和编辑行业信息
        /// </summary>
        /// <returns></returns>
        public ActionResult OperateBranch(string id)
        {
            var operateType = Request.QueryString["operateType"];

            #region 绑定
            List<BranchInfoContract> list = new List<BranchInfoContract>();
            DataSet dsBranch = bs.FindIsFBranch(20);
            for (int i = 0; i < dsBranch.Tables[0].Rows.Count; i++)
            {
                BranchInfoContract branch = new BranchInfoContract();
                branch.BranchID = Convert.ToInt32(dsBranch.Tables[0].Rows[i]["BranchID"].ToString());
                branch.BranchName = "├－" + CommonHelper.getListName(dsBranch.Tables[0].Rows[i]["BranchName"].ToString(), dsBranch.Tables[0].Rows[i]["Temp1"].ToString());

                list.Add(branch);

            }


            ViewData["FatherBranch"] = new SelectList(list, "BranchID", "BranchName");
            #endregion

            //得到所有的行业信息（名称与编号）
            switch (operateType)
            {
                case "add":
                    ViewBag.pTitle = "添加行业信息";

                    return View(new BranchInfoContract());
                default:
                    ViewBag.pTitle = "编辑行业信息";
                    BranchInfoContract bc = new BranchInfoContract();
                    if (id != null)
                    {
                        //根据渠道编号查询渠道信息
                        bc = bs.GetBranchByBranchID(int.Parse(id));
                        DataSet branchDs = bs.GetBranchNameByPBranchID(int.Parse(bc.PBranchId.ToString()));
                        ViewData["FBranch"] = branchDs.Tables[0].Rows.Count == 0 ? "重庆市足下软件学院" : branchDs.Tables[0].Rows[0]["BranchName"].ToString();
                    }

                    return View(bc);
            }
        }

        /// <summary>
        ///  添加和编辑行业信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OperateBranch(BranchInfoContract bc, FormCollection fc)
        {

            #region 绑定
            List<BranchInfoContract> list = new List<BranchInfoContract>();

            DataSet dsBranch = bs.FindIsFBranch(20);
            for (int i = 0; i < dsBranch.Tables[0].Rows.Count; i++)
            {
                BranchInfoContract branch = new BranchInfoContract();
                branch.BranchID = Convert.ToInt32(dsBranch.Tables[0].Rows[i]["BranchID"].ToString());
                branch.BranchName = "├－" + CommonHelper.getListName(dsBranch.Tables[0].Rows[i]["BranchName"].ToString(), dsBranch.Tables[0].Rows[i]["Temp1"].ToString());
                list.Add(branch);

            }

            ViewData["FatherBranch"] = new SelectList(list, "BranchID", "BranchName");
            #endregion
            //判断要添加的行业是否已经存在
            if (!bs.IsExists(bc.BranchName, bc.BranchID.ToString()))
            {
                //执行失败信息
                ViewData["msg"] = "【" + bc.BranchName + "】这个行业已经存在，请先确认!";
                //失败视图
                return View("Error");
            }
            else
            {

                switch (ModelState.IsValid)
                {
                    case true:
                        #region Code
                        string parentCode = string.Empty;
                        string leftCode = string.Empty;
                        string selfCode = string.Empty;
                        //如果付渠道ID不为0，就查询父渠道的temp1
                        if (bc.PBranchId != 0)
                        {
                            //渠道ID不为0，就查询其信息 
                            BranchInfoContract Branch = bs.GetBranchByBranchID(Convert.ToInt32(bc.PBranchId));
                            parentCode = Branch.Temp1;
                        }

                        DataTable dtCategory = bs.FindBranchByCode(parentCode, parentCode.Length).Tables[0];
                        if (dtCategory.Rows.Count > 0)
                        {
                            leftCode = dtCategory.Rows[0]["temp1"].ToString();
                        }
                        if (leftCode.Length > 0)
                            selfCode = Convert.ToString(Convert.ToInt32(leftCode.Substring(leftCode.Length - 4, 4)) + 1).PadLeft(4, '0');
                        else
                            selfCode = "0001";
                        selfCode = parentCode + selfCode;
                        bc.Temp1 = selfCode;
                        #endregion
                        //如果要修改的行业编号为空，则为添加行业信息操作
                        if (fc["BranchId"] == "0")
                        {
                            if (bs.AddBranch(bc) > 0)
                            {

                                //执行成功信息
                                ViewData["msg"] = "添加行业成功!";
                                //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                                ViewData["url"] = new UrlHelper(Request.RequestContext).Action("BranchMain");
                                //成功视图
                                return View("Success");


                            }
                            else
                            {
                                //失败  
                                ViewData["msg"] = "添加行业失败!";
                                return View("Error");
                            }
                        }
                        else
                        {
                            if (bc.BranchID == bc.PBranchId)
                            {
                                //失败
                                ViewData["msg"] = "修改行业信息失败,父行业不能为自身!";
                                return View("Error");
                            }

                            if (bs.UpdateBranch(bc) > 0)
                            {
                                //修改成功
                                //执行成功信息
                                ViewData["msg"] = "修改行业成功!";
                                //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                                ViewData["url"] = new UrlHelper(Request.RequestContext).Action("BranchMain");
                                //成功视图
                                return View("Success");
                            }
                            else
                            {
                                //失败
                                ViewData["msg"] = "修改行业信息失败!";
                                return View("Error");
                            }
                        }

                    default:
                        if (bc.BranchID != 0)
                        {
                            ViewBag.pTitle = "编辑行业信息";
                            return View(bc);

                        }
                        else
                        {
                            ViewBag.pTitle = "添加行业信息";
                            return View(new BranchInfoContract());
                        }
                }
            }

        }
        #endregion

        #region 删除行业信息的方法
        /// <summary>
        /// 删除行业信息的方法
        /// </summary>
        /// <param name="id">要删除的行业编号</param>
        /// <returns></returns>
        public ActionResult DeleteBranch(string id)
        {
            string msg = "";
            string status = "";
            if (id != null)
            {
                //分割将要删除的行业编号
                string[] strS = id.Split(',');
                for (int i = 0; i < strS.Length; i++)
                {
                    //判断要删除的行业下是否还有子行业
                    if (bs.IsHaveMicroelectronicsByBranchID(int.Parse(strS[i])) > 0)
                    {
                        msg = "您要删除的行业下是否还有子行业，请先删除子行业!";
                        status = "Error";
                    }
                    else
                    {

                        var aa = id[i];
                        //删除行业
                        if (bs.DeleteBranch(int.Parse(strS[i])) > 0)
                        {

                            msg = "删除行业成功!";
                            status = "Success";
                        }
                        else
                        {

                            msg = "删除行业失败，请先确实!";
                            status = "Error";

                        }
                    }
                }
            }
            ViewData["msg"] = msg;
            if (status == "Success")
            {
                ViewData["url"] = new UrlHelper(Request.RequestContext).Action("BranchMain");
            }
            return View(status);
        }
        #endregion

        #region 其它验证
        /// <summary>
        /// 验证行业名称是否存在的方法
        /// </summary>
        /// <param name="branchName">行业名称</param>
        /// <returns></returns>
        public string ValidBranchName(string branchName)
        {
            string flag = "";
            if (bs.GetBranchByBranchName(branchName) > 0)
            {
                //已经存在
                flag = "exist";
            }
            else
            {
                //不存在
                flag = "Noexist";
            }
            return flag;
        }
        #endregion


        #endregion

        #region 微信职位管理 职位：试用客户、VIP1 VIP2 VIP3



        #region 数据列表

        /// <summary>
        /// 职位信息主页（首次加载）
        /// </summary>
        /// <returns></returns>
        public ActionResult PositionMain()
        {
            ViewData["list_PageAuthority"] = GetPageAuthority("PositionMain");
            return View(ps.GetPositionByPager(pageIndex, pageSize, ""));
        }

        /// <summary>
        /// 职位数据显示(分页数据)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PositionMain(FormCollection fc)
        {
            ViewData["list_PageAuthority"] = GetPageAuthority("PositionMain");
            //处理输入数据时候的异常
            if (!int.TryParse(fc["CurPage"], out pageIndex))
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
            ViewData["PositionName"] = fc["keyWords"];

            return View(ps.GetPositionByPager(pageIndex, pageSize, fc["keyWords"].Trim()));
        }

        #endregion 数据列表

        #region 添加和编辑职位信息

        /// <summary>
        /// 添加和编辑部门信息
        /// </summary>
        /// <returns></returns>
        public ActionResult OperatePosition(string id)
        {
            PositionInfoContract pc = new PositionInfoContract();
            var operateType = Request.QueryString["operateType"];
            var url = Request.QueryString["url"];

            switch (operateType)
            {
                case "add":
                    ViewBag.pTitle = "添加职位信息";

                    return View(pc);
                default:
                    ViewBag.pTitle = "编辑职位信息";

                    if (id != null)
                    {
                        //根据职位编号查询职位信息的方法
                        pc = ps.GetPositionByPositionID(int.Parse(id));
                    }
                    return View(pc);
            }
        }

        /// <summary>
        /// 添加和编辑部门信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OperatePosition(PositionInfoContract pc, string id, string url)
        {


            //判断要添加的职位是否已经存在
            if (!ps.IsExists(pc.PositionName, pc.PositionId.ToString()))
            {
                //执行失败信息
                ViewData["msg"] = "【" + pc.PositionName + "】这个职位已经存在，请先确认!";
                //失败视图
                return View("Error");
            }
            else
            {
                switch (ModelState.IsValid)
                {
                    case true:

                        //如果要修改的职位编号为null，则为添加职位信息操作
                        if (id == null)
                        {
                            if (ps.AddPosition(pc) > 0)
                            {
                                if (!string.IsNullOrEmpty(url))
                                {
                                    return RedirectToAction("BranchMain", "BranchManage");
                                }
                                else
                                {
                                    //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                                    ViewData["url"] = new UrlHelper(Request.RequestContext).Action("PositionMain");
                                }
                                //执行成功信息
                                ViewData["msg"] = "添加职位成功!";
                                //成功视图
                                return View("Success");
                            }
                            else
                            {
                                //执行成功信息
                                ViewData["msg"] = "添加职位失败!";
                                return View("Error");
                            }
                        }
                        else
                        {
                            pc.PositionId = int.Parse(id);
                            if (ps.UpdatePosition(pc) > 0)
                            {
                                //执行成功信息
                                ViewData["msg"] = "修改职位成功!";
                                //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                                ViewData["url"] = new UrlHelper(Request.RequestContext).Action("PositionMain");
                                //成功视图
                                return View("Success");
                            }
                            else
                            {
                                ViewData["msg"] = "修改职位失败!";
                                return View("Error");
                            }
                        }
                    default:
                        ViewBag.pTitle = "添加职位信息";
                        return View(new PositionInfoContract());
                }
            }

        }

        #endregion 添加和编辑职位信息
        #endregion

        #region 微信客户管理
        #region 企业信息列表


        /// <summary>
        /// 企业信息列表（首次加载）
        /// </summary>
        /// <returns></returns>
        public ActionResult EmployeeMain()
        {

            ViewData["list_PageAuthority"] = GetPageAuthority("EmployeeMain");
            PagerList<employeeInfo> list = null;
            if ((Session["user"] as UsersContract).RoleID == 1)
            {
                #region 系统管理员 获取所有企业信息
                list = es.GetEmployeeByPager(pageIndex, pageSize, "");
                #endregion
            }
            else
            {
                #region 获取自己的关键字
                list = es.GetEmployeeByPager((Session["user"] as UsersContract).EId, pageIndex, pageSize, "");
                #endregion
            }
            return View(list);
        }

        /// <summary>
        /// 企业数据显示(分页数据)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EmployeeMain(FormCollection fc)
        {
            ViewData["list_PageAuthority"] = GetPageAuthority("EmployeeMain");
            PagerList<employeeInfo> list = null;

            //处理输入数据时候的异常
            if (!int.TryParse(fc["CurPage"], out pageIndex))
            {
                //throw new Exception("输入的页码有误！");
                pageIndex = 1;
            }
            else
            {
                int nowPageIndex = int.Parse(fc["curPage"]);
                if (fc["keyWords"].Trim() == "")
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
            ViewData["empKeyWords"] = fc["keyWords"].Trim();
            if ((Session["user"] as UsersContract).RoleID == 1)
            {
                #region 系统管理员 获取所有企业信息
                list = es.GetEmployeeByPager(pageIndex, pageSize, fc["keyWords"].Trim());
                #endregion
            }
            else
            {
                #region 获取自己的关键字
                list = es.GetEmployeeByPager((Session["user"] as UsersContract).EId, pageIndex, pageSize, fc["keyWords"].Trim());
                #endregion
            }

            return View();
        }
        #endregion

        #region 添加和编辑员工信息
        /// <summary>
        /// 添加和编辑员工
        /// </summary>
        /// <param name="id">员工编号</param>
        /// <param name="operate">标示，判断是否是从员工详细页面跳转到本页面的</param>
        /// <returns></returns>
        public ActionResult OperateEmployee(string id, string operate)
        {
            var operateType = Request.QueryString["operateType"];
            ViewData["operate"] = operate;
            EmployeeInfoContract ec = new EmployeeInfoContract();
            #region 绑定 注：如果是企业自身编辑，这些数据是企业无法修改的，只能是超级管理员才能修改。
            if (id != null)  //编辑员工操作
            {
                //根据员工编号查询员工信息的方法
                ec = es.GetEmpInfoByEId(int.Parse(id));
                //查询企业的店铺信息
                ViewData["empdata"] = empDataBll.GetEmpData(int.Parse(id));
            }
            //当前登录人的权限 1系统管理员，2 企业

            if ((Session["user"] as UsersContract).RoleID == 1)
            {
                #region 系统管理员
                List<BranchInfoContract> list = new List<BranchInfoContract>();
                DataSet dsBranch = bs.FindIsFBranch(20);
                for (int i = 0; i < dsBranch.Tables[0].Rows.Count; i++)
                {
                    BranchInfoContract branch = new BranchInfoContract();
                    branch.BranchID = Convert.ToInt32(dsBranch.Tables[0].Rows[i]["BranchID"].ToString());
                    branch.BranchName = "├－" + CommonHelper.getListName(dsBranch.Tables[0].Rows[i]["BranchName"].ToString(), dsBranch.Tables[0].Rows[i]["Temp1"].ToString());

                    list.Add(branch);

                }

                ViewData["FatherBranch"] = new SelectList(list, "BranchID", "BranchName");
                //查询用户组 
                ViewData["role"] = new SelectList(rolesServices.GetRoles(), "RoleID", "RoleName");

                List<positionInfo> polist = ps.GetPoList();
                ViewData["Group"] = new SelectList(polist, "positionId", "positionName");

                #endregion
            }
            else
            {
                #region 企业及其他

                List<BranchInfoContract> list = new List<BranchInfoContract>();
                BranchInfoContract Branch = bs.GetBranchByBranchID(ec.BranchID);
                list.Add(Branch);


                ViewData["FatherBranch"] = new SelectList(list, "BranchID", "BranchName");
                //查询用户组  
                List<RolesContract> listRole = new List<RolesContract>();
                ViewData["role"] = new SelectList(listRole, "RoleID", "RoleName");

                List<PositionInfoContract> polist = new List<PositionInfoContract>();
                polist.Add(ps.GetPositionByPositionID(ec.positionId));
                ViewData["Group"] = new SelectList(polist, "positionId", "positionName");

                #endregion
            }
            #endregion
            switch (operateType)
            {
                case "add":
                    ViewBag.pTitle = "添加员工信息";
                    return View(new EmployeeInfoContract());
                default:
                    ViewBag.pTitle = "编辑员工信息";
                    return View(ec);
            }
        }

        /// <summary>
        ///  添加和编辑员工信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OperateEmployee(EmployeeInfoContract ec, FormCollection fc)
        {

            #region 绑定 注：如果是企业自身编辑，这些数据是企业无法修改的，只能是超级管理员才能修改。

            //当前登录人的权限 1系统管理员，2 企业

            if ((Session["user"] as UsersContract).RoleID == 1)
            {
                #region 系统管理员
                List<BranchInfoContract> list = new List<BranchInfoContract>();
                DataSet dsBranch = bs.FindIsFBranch(20);
                for (int i = 0; i < dsBranch.Tables[0].Rows.Count; i++)
                {
                    BranchInfoContract branch = new BranchInfoContract();
                    branch.BranchID = Convert.ToInt32(dsBranch.Tables[0].Rows[i]["BranchID"].ToString());
                    branch.BranchName = "├－" + CommonHelper.getListName(dsBranch.Tables[0].Rows[i]["BranchName"].ToString(), dsBranch.Tables[0].Rows[i]["Temp1"].ToString());

                    list.Add(branch);

                }

                ViewData["FatherBranch"] = new SelectList(list, "BranchID", "BranchName");
                //查询用户组 
                ViewData["role"] = new SelectList(rolesServices.GetRoles(), "RoleID", "RoleName");

                List<positionInfo> polist = ps.GetPoList();
                ViewData["Group"] = new SelectList(polist, "positionId", "positionName");

                #endregion
            }
            else
            {
                #region 企业及其他

                List<BranchInfoContract> list = new List<BranchInfoContract>();
                BranchInfoContract Branch = bs.GetBranchByBranchID(ec.BranchID);
                list.Add(Branch);


                ViewData["FatherBranch"] = new SelectList(list, "BranchID", "BranchName");
                //查询用户组  
                List<RolesContract> listRole = new List<RolesContract>();
                ViewData["role"] = new SelectList(listRole, "RoleID", "RoleName");

                List<PositionInfoContract> polist = new List<PositionInfoContract>();
                polist.Add(ps.GetPositionByPositionID(ec.positionId));
                ViewData["Group"] = new SelectList(polist, "positionId", "positionName");

                #endregion
            }
            #endregion

            var operateType = Request.QueryString["operateType"];
            #region 获取信息

            employeeData empdata = new employeeData()
            {
                Address = fc["add"],
                Tel = fc["Telphone"],
                zuobiao = fc["zuobiao"],
                Photo = fc["photo"]
            };
            #endregion
            if (ModelState.IsValid)
            {

                if (fc["EId"] == null || fc["EId"] == "0")
                {
                    //ADD
                    #region Add　Emp Info
                    Users user = new Users();
                    user.UserName = fc["UserID"];
                    user.RoleID = int.Parse(fc["RoleID"]);
                    ec.AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    if (es.InsertEmp(ec, user, empdata) > 0)
                    {
                        // Success 
                        ViewData["msg"] = "添加成功!";
                        ViewData["url"] = Url.Action("OperateEmployee", new { id = ec.EId });
                        return View("Success");
                    }
                    else
                    {
                        return View(ec);
                    }
                    #endregion
                }
                else
                {
                    //Edit
                    #region Edit Info

                    if (es.EditEmp(ec) == 1)
                    {
                        if (empdata.Address.Length > 1)
                        {
                            empdata.eid = ec.EId;
                            empDataBll.Insert(empdata);
                        }
                        // Success 
                        ViewData["msg"] = "修改成功!";
                        ViewData["url"] = Url.Action("OperateEmployee", new { id = ec.EId });
                        return View("Success");
                    }
                    else
                    {

                        return View(ec);
                    }
                    #endregion
                }
            }
            return View();
        }

        /// <summary>
        /// 删除企业坐标点
        /// </summary>
        /// <param name="id"></param>
        /// <param name="eid"></param>
        /// <returns></returns>
        public ActionResult deleteEmployeeData(int id, string eid)
        {
            string msg = "";
            string status = "";
            if (id.ToString() != null)
            {
                if (empDataBll.DeleteEmpData(id) > 0)
                {
                    msg = "删除成功!";
                    status = "Success";
                }
                else
                {

                    msg = "删除失败";
                    status = "Error";
                }

            }
            ViewData["msg"] = msg;
            if (status == "Success")
            {
                ViewData["url"] = new UrlHelper(Request.RequestContext).Action("OperateEmployee", new { id = eid });
            }
            return View(status);
        }
        #endregion
        #endregion

        #region 企业关键字管理

        #region 数据列表
        /// <summary>
        /// 关键字信息主页（首次加载）
        /// </summary>
        /// <returns></returns>
        public ActionResult GuanJianZiMain()
        {
            ViewData["list_PageAuthority"] = GetPageAuthority("GuanJianZiMain");
            PagerList<GuanJianZi> list = null;
            if ((Session["user"] as UsersContract).RoleID == 1)
            {
                #region 系统管理员 获取所有企业信息
                list = gs.GetGuanJianZIByPager(pageIndex, pageSize, "");
                #endregion
            }
            else
            {
                #region 获取自己的关键字
                list = gs.GetGuanJianZIByPager((Session["user"] as UsersContract).EId, pageIndex, pageSize, "");
                #endregion
            }
            return View(list);
        }

        /// <summary>
        /// 关键字数据显示(分页数据)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GuanJianZiMain(FormCollection fc)
        {
            ViewData["list_PageAuthority"] = GetPageAuthority("GuanJianZiMain");
            //处理输入数据时候的异常
            if (!int.TryParse(fc["CurPage"], out pageIndex))
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
            ViewData["GJZName"] = fc["keyWords"];

            PagerList<GuanJianZi> list = null;
            if ((Session["user"] as UsersContract).RoleID == 1)
            {
                #region 系统管理员 获取所有企业信息
                list = gs.GetGuanJianZIByPager(pageIndex, pageSize, fc["keyWords"]);
                #endregion
            }
            else
            {
                #region 获取自己的关键字
                list = gs.GetGuanJianZIByPager((Session["user"] as UsersContract).EId, pageIndex, pageSize, fc["keyWords"]);
                #endregion
            }
            return View(list);
        }

        #endregion 数据列表

        #region 添加和编辑关键字信息

        /// <summary>
        /// 添加和编辑关键字信息
        /// </summary>
        /// <returns></returns>
        public ActionResult OperateGuanJianZi(string id)
        {
            WeiXin.BO.GuanJianZiContract pc = new WeiXin.BO.GuanJianZiContract();
            var operateType = Request.QueryString["operateType"];
            var url = Request.QueryString["url"];

            #region 绑定关键字所属企业
            List<employeeInfo> list = new List<employeeInfo>();
            if ((Session["user"] as UsersContract).RoleID == 1)
            {
                #region 系统管理员 获取所有企业
                list = gs.GetAllEmp();
                #endregion
            }
            else
            {
                #region 获取自己的企业
                list.Add(es.GetEmployeeinfoByEId((Session["user"] as UsersContract).EId));
                #endregion
            }

            #endregion

            #region 绑定关键字类型
            List<GuanJianZiHuiFuTypeContract> gjzType = new List<GuanJianZiHuiFuTypeContract>();
            //获取所有的关键字类型
            List<GuanJianZiType> dsGJZType = gs.GetAllGJZType();

            foreach (var item in dsGJZType)
            {
                GuanJianZiHuiFuTypeContract tt = new GuanJianZiHuiFuTypeContract();
                tt.Id = Convert.ToInt32(item.Id);
                tt.name = item.name;
                gjzType.Add(tt);
            }
            ViewData["gjzType"] = new SelectList(gjzType, "Id", "name");
            #endregion

            switch (operateType)
            {
                case "add":
                    ViewBag.pTitle = "添加关键字";

                    ViewData["Emp"] = new SelectList(list, "Eid", "wxName");
                    return View(pc);
                default:
                    ViewBag.pTitle = "编辑关键字";

                    if (id != null)
                    {
                        //根据关键字编号查询关键字的方法
                        pc = gs.GetGuanJianZiByGJZId(int.Parse(id));
                    }

                    ViewData["Emp"] = new SelectList(list, "Eid", "wxName", pc.eId);
                    return View(pc);
            }
        }

        /// <summary>
        /// 添加和编辑关键字信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OperateGuanJianZi(GuanJianZiContract pc, string id, string url)
        {
            pc.time = DateTime.Now.ToString("yyyy-MM-dd");

            #region 绑定关键字所属企业
            List<employeeInfo> list = new List<employeeInfo>();
            if ((Session["user"] as UsersContract).RoleID == 1)
            {
                #region 系统管理员 获取所有企业
                list = gs.GetAllEmp();
                #endregion
            }
            else
            {
                #region 获取自己的企业
                list.Add(es.GetEmployeeinfoByEId((Session["user"] as UsersContract).EId));
                #endregion
            }

            #endregion

            #region 绑定关键字类型
            List<GuanJianZiHuiFuTypeContract> gjzType = new List<GuanJianZiHuiFuTypeContract>();
            //获取所有的关键字类型
            List<GuanJianZiType> dsGJZType = gs.GetAllGJZType();

            foreach (var item in dsGJZType)
            {
                GuanJianZiHuiFuTypeContract tt = new GuanJianZiHuiFuTypeContract();
                tt.Id = Convert.ToInt32(item.Id);
                tt.name = item.name;
                gjzType.Add(tt);
            }
            ViewData["gjzType"] = new SelectList(gjzType, "Id", "name");
            #endregion

            //判断要添加的关键字是否已经存在
            if (gs.IsExists(pc.name, pc.eId.ToString(), id))
            {
                //执行失败信息
                ViewData["msg"] = "【" + pc.name + "】这个关键字已经存在，请先确认!";
                //失败视图
                return View("Error");
            }
            else
            {
                switch (ModelState.IsValid)
                {
                    case true:

                        //如果要修改的编号为null，则为添加关键字信息操作
                        if (id == null)
                        {

                            if (gs.AddGuanJianZi(pc) > 0)
                            {

                                //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                                ViewData["url"] = new UrlHelper(Request.RequestContext).Action("GuanJianZiMain");

                                //执行成功信息
                                ViewData["msg"] = "添加关键字成功!";
                                //成功视图
                                return View("Success");
                            }
                            else
                            {
                                //执行成功信息
                                ViewData["msg"] = "添加关键字失败!";
                                return View("Error");
                            }
                        }
                        else
                        {
                            pc.gjzId = int.Parse(id);
                            if (gs.UpdateGuanJianZi(pc) > 0)
                            {
                                //执行成功信息
                                ViewData["msg"] = "修改关键字成功!";
                                //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                                ViewData["url"] = new UrlHelper(Request.RequestContext).Action("GuanJianZiMain");
                                //成功视图
                                return View("Success");
                            }
                            else
                            {
                                ViewData["msg"] = "修改关键字失败!";
                                return View("Error");
                            }
                        }
                    default:
                        ViewBag.pTitle = "添加关键字信息";
                        return View(new GuanJianZiHuiFuContract());
                }
            }

        }

        #endregion 添加和编辑关键字信息

        #region 删除关键字信息的方法
        /// <summary>
        /// 删除关键字信息的方法
        /// </summary>
        /// <param name="id">要删除的关键字编号</param>
        /// <returns></returns>
        public ActionResult DeleteGuanJianZi(string id)
        {
            string msg = "";
            string status = "";
            if (id != null)
            {
                //分割将要删除的关键字编号
                string[] strS = id.Split(',');
                for (int i = 0; i < strS.Length; i++)
                {
                    //判断删除的关键字下面是否还有回复内容
                    if (gs.IsHaveGJZHFByGJZId(int.Parse(strS[i])) > 0)
                    {

                        msg = "您要删除的关键字下面还有回复内容，请先确认!";
                        status = "Error";
                    }
                    else
                    {
                        var aa = id[i];
                        //删除关键字
                        if (gs.DeleteGuanJianZi(int.Parse(strS[i])) > 0)
                        {
                            msg = "删除关键字成功!";
                            status = "Success";
                        }
                        else
                        {

                            msg = "删除关键字失败，请先确实!";
                            status = "Error";
                        }
                    }

                }
            }
            ViewData["msg"] = msg;
            if (status == "Success")
            {
                ViewData["url"] = new UrlHelper(Request.RequestContext).Action("GuanJianZiMain");
            }
            return View(status);
        }
        #endregion

        #endregion

        #region 关键字回复管理

        #region 数据列表
        /// <summary>
        /// 关键字回复信息主页（首次加载）
        /// </summary>
        /// <returns></returns>
        public ActionResult GuanJianZiHuiFuMain()
        {
            ViewData["list_PageAuthority"] = GetPageAuthority("GuanJianZiHuiFuMain");
            PagerList<GuanJianZiHuiFu> list = null;
            if ((Session["user"] as UsersContract).RoleID == 1)
            {
                #region 系统管理员 获取所有企业信息
                list = hfs.GetGuanJianZIHuiFuByPager(pageIndex, pageSize, "");
                #endregion
            }
            else
            {
                #region 获取自己的关键字
                list = hfs.GetGuanJianZIHuiFuByPager((Session["user"] as UsersContract).EId, pageIndex, pageSize, "");
                #endregion
            }
            return View(list);
        }

        /// <summary>
        /// 关键字回复数据显示(分页数据)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GuanJianZiHuiFuMain(FormCollection fc)
        {
            PagerList<GuanJianZiHuiFu> list = null;
            ViewData["list_PageAuthority"] = GetPageAuthority("GuanJianZiHuiFuMain");
            //处理输入数据时候的异常
            if (!int.TryParse(fc["CurPage"], out pageIndex))
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
            ViewData["GJZHFName"] = fc["keyWords"];
            if ((Session["user"] as UsersContract).RoleID == 1)
            {
                #region 系统管理员 获取所有企业信息
                list = hfs.GetGuanJianZIHuiFuByPager(pageIndex, pageSize, fc["keyWords"].Trim());
                #endregion
            }
            else
            {
                #region 获取自己的关键字
                list = hfs.GetGuanJianZIHuiFuByPager((Session["user"] as UsersContract).EId, pageIndex, pageSize, fc["keyWords"].Trim());
                #endregion
            }

            return View(list);
        }

        #endregion 数据列表

        #region 添加和编辑关键字回复信息

        /// <summary>
        /// 添加和编辑回复关键字信息
        /// </summary>
        /// <returns></returns>
        public ActionResult OperateGuanJianZiHuiFu(string id)
        {
            GuanJianZiHuiFuContract pc = new GuanJianZiHuiFuContract();
            var operateType = Request.QueryString["operateType"];
            var url = Request.QueryString["url"];

            #region 绑定关键字回复内容所属的关键字
            List<GuanJianZiHuiFuTypeContract> list = new List<GuanJianZiHuiFuTypeContract>();
            //获取所有的关键字类型
            List<GuanJianZiHuiFuType> dsGJZType = hfs.GetAllGJZType();

            foreach (var item in dsGJZType)
            {
                GuanJianZiHuiFuTypeContract emp = new GuanJianZiHuiFuTypeContract();
                emp.trueId = Convert.ToInt32(item.trueId);
                emp.name = item.name;
                list.Add(emp);
            }

            ViewData["gjz"] = new SelectList(list, "trueId", "name");
            #endregion

            #region 绑定企业
            List<employeeInfo> listEmp = null;
            if ((Session["user"] as UsersContract).RoleID == 1)
            {
                #region 系统管理员 获取所有企业信息
                listEmp = gs.GetAllEmp();
                #endregion
            }
            else
            {
                #region 获取自己的关键字
                listEmp = gs.GetAllEmp((Session["user"] as UsersContract).EId);
                #endregion
            }
            //获取所有的企业

            ViewData["emp"] = new SelectList(listEmp, "Eid", "wxName");
            #endregion

            switch (operateType)
            {
                case "add":
                    ViewBag.pTitle = "添加回复内容";


                    return View(pc);
                default:
                    ViewBag.pTitle = "编辑回复内容";

                    if (id != null && id != "0")
                    {
                        //根据关键字回复编号查询关键字的方法
                        pc = hfs.GetGuanJianZiHuiFuByHFId(int.Parse(id));
                    }


                    ViewData["nowGJZId"] = pc.gjzId;
                    ViewData["nowTypeId"] = pc.typeId;
                    ViewData["content"] = pc.content;
                    ViewData["oldImg"] = pc.img;
                    return View(pc);
            }
        }

        /// <summary>
        /// 添加和编辑回复关键字信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)] //目的是为了防止在提交时报“检测到有潜在危险的客户端输入值”
        public ActionResult OperateGuanJianZiHuiFu(FormCollection fc, GuanJianZiHuiFuContract pc, string id, string url)
        {
            #region 绑定关键字回复内容所属的关键字
            List<GuanJianZiHuiFuTypeContract> list = new List<GuanJianZiHuiFuTypeContract>();
            //获取所有的关键字类型
            List<GuanJianZiHuiFuType> dsGJZType = hfs.GetAllGJZType();

            foreach (var item in dsGJZType)
            {
                GuanJianZiHuiFuTypeContract emp = new GuanJianZiHuiFuTypeContract();
                emp.trueId = Convert.ToInt32(item.trueId);
                emp.name = item.name;
                list.Add(emp);
            }

            ViewData["gjz"] = new SelectList(list, "trueId", "name");
            #endregion

            #region 绑定企业
            List<employeeInfo> listEmp = null;
            if ((Session["user"] as UsersContract).RoleID == 1)
            {
                #region 系统管理员 获取所有企业信息
                listEmp = gs.GetAllEmp();
                #endregion
            }
            else
            {
                #region 获取自己的关键字
                listEmp = gs.GetAllEmp((Session["user"] as UsersContract).EId);
                #endregion
            }
            //获取所有的企业

            ViewData["emp"] = new SelectList(listEmp, "Eid", "wxName");
            #endregion

            var countent = new MvcHtmlString(pc.content);
            pc.content = countent.ToString();

            //图文图片
            string img = fc["willUploadImg"];

            //判断要添加的关键字是否已经存在
            //if (hfs.IsExists(pc.content, pc.gjzId.ToString(), id))
            //{
            //    //执行失败信息
            //    ViewData["msg"] = "要添加的回复内容已经存在，请先确认!";
            //    //失败视图
            //    return View("Error");
            //}
            //else
            //{
            pc.time = DateTime.Now.ToString();
            pc.img = img;
            switch (ModelState.IsValid)
            {
                case true:

                    //如果要修改的编号为null，则为添加回复信息操作
                    if (id != null && id == "0")
                    {
                        if (hfs.AddGuanJianZiHuiFu(pc) > 0)
                        {
                            //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                            ViewData["url"] = new UrlHelper(Request.RequestContext).Action("GuanJianZiHuiFuMain");

                            //执行成功信息
                            ViewData["msg"] = "添加回复内容成功!";
                            //成功视图
                            return View("Success");
                        }
                        else
                        {
                            //执行成功信息
                            ViewData["msg"] = "添加回复内容失败!";
                            return View("Error");
                        }
                    }
                    else
                    {
                        pc.gjzId = int.Parse(fc["gjzId"]);
                        if (hfs.UpdateGuanJianZiHuiFu(pc) > 0)
                        {
                            //执行成功信息
                            ViewData["msg"] = "修改回复内容成功!";
                            //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                            ViewData["url"] = new UrlHelper(Request.RequestContext).Action("GuanJianZiHuiFuMain");
                            //成功视图
                            return View("Success");
                        }
                        else
                        {
                            ViewData["msg"] = "修改关回复内容失败!";
                            return View("Error");
                        }
                    }
                default:
                    ViewBag.pTitle = "添加回复内容信息";
                    return View(new GuanJianZiHuiFuContract());
                //}
            }

        }

        #endregion 添加和编辑关键字回复信息

        #region 删除关键字回复信息的方法
        /// <summary>
        /// 删除关键字回复信息的方法
        /// </summary>
        /// <param name="id">要删除的关键字回复编号</param>
        /// <returns></returns>
        public ActionResult DeleteGuanJianZiHuiFu(string id)
        {
            string msg = "";
            string status = "";
            if (id != null)
            {
                //分割将要删除的关键字回复编号
                string[] strS = id.Split(',');
                for (int i = 0; i < strS.Length; i++)
                {
                    var aa = id[i];
                    //删除关键字回复信息
                    if (hfs.DeleteGuanJianZiHuiFu(int.Parse(strS[i])) > 0)
                    {
                        msg = "删除关键字回复信息成功!";
                        status = "Success";
                    }
                    else
                    {
                        msg = "删除关键字回复信息失败，请先确实!";
                        status = "Error";
                    }
                }
            }
            ViewData["msg"] = msg;
            if (status == "Success")
            {
                ViewData["url"] = new UrlHelper(Request.RequestContext).Action("GuanJianZiHuiFuMain");
            }
            return View(status);
        }
        #endregion

        #region 查询关键字回复详细信息
        /// <summary>
        /// 根据回复ID查询关键字回复详细信息
        /// </summary>
        /// <param name="hfId"></param>
        /// <returns></returns>    
        public ActionResult GuanJianZiHuiFangDetails(string hfId)
        {
            GuanJianZiHuiFuContract pc = new GuanJianZiHuiFuContract();
            pc = hfs.GetGuanJianZiHuiFuByHFId(int.Parse(hfId));
            return View(pc);
        }
        #endregion

        #region 其他方法

        /// <summary>
        /// 根据企业编号查询该企业下的关键字
        /// </summary>
        /// <param name="empId">企业编号</param> 
        /// <returns></returns>
        [HttpPost]
        public JsonResult AjaxGetGJZ(string empId)
        {
            //根据企业编号查询该企业下的关键字
            List<GuanJianZiContract> gjzList = gs.GetGuanJianZiByEId(int.Parse(empId));
            //将获取到的关键字信息转为json字符串
            return Json(gjzList);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UploadImage()
        {
            string savePath = "/FileUpload/" + DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("dd") + "/"; 
            //按日期归类保存 
            string saveUrl = "/FileUpload/" + DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("dd") + "/";
            
            string fileTypes = "gif,jpg,jpeg,png,bmp";
            int maxSize = 1000000;

            Hashtable hash = new Hashtable();

            HttpPostedFileBase file = Request.Files["imgFile"];
            if (file == null)
            {
                hash = new Hashtable();
                hash["error"] = 0;
                hash["url"] = "请选择文件";
                return Json(hash);
            }

            string dirPath = Server.MapPath(savePath);
           
            //检查是否有该路径  没有就创建
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string fileName = file.FileName;
            string fileExt = Path.GetExtension(fileName).ToLower();

            ArrayList fileTypeList = ArrayList.Adapter(fileTypes.Split(','));

            if (file.InputStream == null || file.InputStream.Length > maxSize)
            {
                hash = new Hashtable();
                hash["error"] = 0;
                hash["url"] = "上传文件大小超过限制";
                return Json(hash);
            }

            if (string.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                hash = new Hashtable();
                hash["error"] = 0;
                hash["url"] = "上传文件扩展名是不允许的扩展名";
                return Json(hash);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            string filePath = dirPath + newFileName;
            file.SaveAs(filePath);
            string fileUrl = saveUrl + newFileName;

            hash = new Hashtable();
            hash["error"] = 0;
            hash["url"] = fileUrl;

            return Json(hash, "text/html;charset=UTF-8"); ;
        }

        [HttpPost]
        //图片上传
        public ContentResult Import()
        {
            string result = "";
            HttpPostedFileBase file = Request.Files["FileData"];
            string uploadPath = Server.MapPath(@"/FileUpload");

            if (file != null)
            {

                FileInfo File = new FileInfo(file.FileName);
                //获得文件扩展名
                string fileNameExt = File.Extension;
                //验证合法的文件
                if (CheckFileExt(fileNameExt))
                {

                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + fileNameExt;

                    //按日期归类保存
                    string datePath = DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("dd") + "/";



                    //物理完整路径                    
                    string toFileFullPath = uploadPath + "/" + datePath;

                    //检查是否有该路径  没有就创建
                    if (!Directory.Exists(toFileFullPath))
                    {
                        Directory.CreateDirectory(toFileFullPath);
                    }

                    file.SaveAs(toFileFullPath + fileName);
                    //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
                    result = datePath + fileName;
                }
            }
            return Content(result);
        }

        /// <summary>
        /// 判断图片格式是否正确
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        private bool CheckFileExt(string ext)
        {
            if (".jpg,.gif,.png".Contains(ext))
            {
                return true;
            }
            return false;

        }

        #endregion  其他方法

        #endregion

    }
}
