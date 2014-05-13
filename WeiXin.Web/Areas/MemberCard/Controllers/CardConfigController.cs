using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeiXin.BLL;
using WeiXin.BO;
using WeiXin.Model;
using WeiXin.Web.Controllers;
using WeiXin.Core; 
using System.Data; 
using System.Collections;
using System.IO;
using System.Globalization; 

namespace WeiXin.Web.Areas.MemberCard.Controllers
{
    [WeiXinException]
    public class CardConfigController : CommonController
    {
        CardConfigService CardConfigBll = new CardConfigService();//会员卡配置
        EmployeeService EmpBll = new EmployeeService();//企业信息
        FansToCardService ftcService = new FansToCardService();  //会员的会员卡业务逻辑对象


        public ActionResult CardConfig()
        {
            int eid = (Session["user"] as UsersContract).EId;
            CardConfigContract cardconfig = CardConfigBll.GetCardConfigByEid(eid);
            #region List


            var typeStete = new string[] { "否", "是" };
            var typeSteteList = new List<object> { };
            for (int i = 0; i < typeStete.Length; i++)
            {
                var item = new { text = typeStete[i], value = typeStete[i] };
                typeSteteList.Add(item);
            }
            ViewData["CardState"] = new SelectList(typeSteteList, "value", "text");

            #endregion
            if (cardconfig.id == 0)
            {
                EmployeeInfoContract Emp = EmpBll.GetEmpInfoByEId(eid);
                //1、第一次进入会员卡配置、实例化
                cardconfig = new CardConfigContract
                {
                    CardAdd = Emp.Address,
                    MinCardID = 100000,
                    CardTel = Emp.phone,
                    CardState = 0
                };
            }
            return View(cardconfig);
        }

        [HttpPost]
        public ActionResult CardConfig(CardConfigContract ccc, FormCollection fc)
        {
            #region List


            var typeStete = new string[] { "否", "是" };
            var typeSteteList = new List<object> { };
            for (int i = 0; i < typeStete.Length; i++)
            {
                var item = new { text = typeStete[i], value = typeStete[i] };
                typeSteteList.Add(item);
            }
            ViewData["CardState"] = new SelectList(typeSteteList, "value", "text");

            #endregion
            ccc.Eid = (Session["user"] as UsersContract).EId;
            if (ccc.id == 0)
            {
                //添加
                if (CardConfigBll.Insert(ccc) > 0)
                {
                    //添加成功
                    ViewData["msg"] = "保存成功!";
                    ViewData["url"] = Url.Action("CardConfig");//  
                    return View("Success");
                }
                else
                {
                    ViewData["msg"] = "保存失败!";
                    return View("Error");
                }

            }
            else
            {
                //修改
                //添加
                if (CardConfigBll.Update(ccc) > 0)
                {
                    //添加成功
                    ViewData["msg"] = "保存成功!";
                    ViewData["url"] = Url.Action("CardConfig");//  
                    return View("Success");
                }
                else
                {
                    ViewData["msg"] = "保存失败!";
                    return View("Error");
                }

            }
        }


        #region 领取会员卡 刘强

        #region 数据列表
        /// <summary>
        /// 会员卡信息主页（首次加载）
        /// </summary>
        /// <returns></returns>
        public ActionResult FansToCardMain()
        {
            ViewData["list_PageAuthority"] = GetPageAuthority("FansToCardMain");
            PagerList<FansToCard> list = null;
            if ((Session["user"] as UsersContract).RoleID == 1)
            {
                #region 系统管理员 获取所有会员卡信息
                list = ftcService.GetFansToCardByPager(pageIndex, pageSize, "");
                #endregion
            }
            else
            {
                #region 获取自己企业所属的会员卡信息
                list = ftcService.GetFansToCardByPager((Session["user"] as UsersContract).EId, pageIndex, pageSize, "");
                #endregion
            }
            return View(list);
        }

        /// <summary>
        /// 会员卡数据显示(分页数据)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FansToCardMain(FormCollection fc)
        {
            ViewData["list_PageAuthority"] = GetPageAuthority("FansToCardMain");
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
            ViewData["keyWords"] = fc["keyWords"];

            PagerList<FansToCard> list = null;
            if ((Session["user"] as UsersContract).RoleID == 1)
            {
                #region 系统管理员 获取所有企业信息
                list = ftcService.GetFansToCardByPager(pageIndex, pageSize, fc["keyWords"]);
                #endregion
            }
            else
            {
                #region 获取自己的关键字
                list = ftcService.GetFansToCardByPager((Session["user"] as UsersContract).EId, pageIndex, pageSize, fc["keyWords"]);
                #endregion
            }
            return View(list);
        }

        #endregion 数据列表

        #region 冻结会员卡

        /// <summary>
        /// 冻结会员卡
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteFansToCard(string id, string state)
        {
            var operate = Request.QueryString["operate"]; //批量冻结或解冻标示 

            if (operate != null)  //如果不为空，说明是批量冻结或者解冻
            {
                string oper = ftcService.GetCardStateById(operate); //查询会员卡的状态
                if (oper != "")
                {
                    state = oper;  //会员卡状态
                    id = operate.Split('/')[1]; //会员卡ID
                }
                else
                {
                    //执行成功信息
                    ViewData["msg"] = "您选择要冻结/解冻的会员卡中的会员状态不一致，即：只能选择为“可用”或者“已冻结”的会员卡!";
                    //成功视图
                    return View("Error");
                }
            } 

            //操作成功
            if (ftcService.DeleteFansToCard(id, state) > 0)
            {
                //执行成功信息
                ViewData["msg"] = "操作成功!";
                //成功后要跳转的路径（如果无须跳转，直接留在当前页面，不写即可）
                ViewData["url"] = new UrlHelper(Request.RequestContext).Action("FansToCardMain");
                //成功视图
                return View("Success");
            }
            else
            {
                //执行成功信息
                ViewData["msg"] = "操作失败!";
                //成功视图
                return View("Error");
            }
        }
        #endregion

        #endregion 领取会员卡 刘强
    }
}
