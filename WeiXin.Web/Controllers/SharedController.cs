
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
using System.Web.Security;
using System.Data;
using System.Xml;
using WeiXin.BO;
using WeiXin.Core;
namespace WeiXin.Web.Controllers
{
    public class SharedController : Controller
    {
        LoginLogInfoServices loginLogInfoServices = new LoginLogInfoServices();
        UsersServices usersServices = new UsersServices();
        ModuleTypeServices moduleTypeServices = new ModuleTypeServices();
        RoleAuthorityListServices roleAuthorityListServices = new RoleAuthorityListServices();

        AuthorityServices authorityServices = new AuthorityServices();
        ModulesServices modulesServices = new ModulesServices();
        EmployeeService EmpBll = new EmployeeService();//企业微信号
        FansService fansBll = new FansService();//Fans
       
        public ActionResult Index()
        {
           
            return View();
        }
        #region 系统登录

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View(new UsersContract());
        }

        /// <summary>
        /// 登录（请求）
        /// </summary>
        /// <param name="formCollection"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(FormCollection formCollection, UsersContract user)
        {
            if (!ModelState.IsValid)
            {
                ViewData["msg"] = "用户名或者密码错误";
                return View("Error");
            }
            //客户端IP
            string clientIp;
            //获取登录数据
            string userLogin = user.UserName.Trim();
            string userPwd = WeiXin.Core.SecurityEncryption.DESEncrypt(user.Password.Trim());

            //查询数据
            UsersContract usersContract = usersServices.GetUserEntity(userLogin: userLogin, userPwd: userPwd);
            if (usersContract != null)
            {
                //记录Session
                Session["user"] = usersContract;
                //创建票据
                FormsAuthentication.SetAuthCookie(userLogin, true);
                //记录数据用户登录日志

                #region 获得IP地址

                if (Request.ServerVariables["HTTP_VIA"] != null)
                {
                    clientIp = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else
                {
                    clientIp = Request.ServerVariables["REMOTE_ADDR"].ToString();
                }

                #endregion 获得IP地址

                #region 记录登录信息

                loginLogInfoContract loginLogInfoContract = new loginLogInfoContract();
                loginLogInfoContract.UserId = usersContract.UserID;
                loginLogInfoContract.LIP = clientIp;
                loginLogInfoContract.LState = 0;
                loginLogInfoContract.LTime = DateTime.Now.ToString();
                loginLogInfoServices.InsertloginLogInfo(loginLogInfoContract);

                #endregion 记录登录信息

                #region 生成XML文件

                CreateAuthorityXML();

                #region 每日新增订阅人数 每日接收消息数
                // 生成XML 
                string set = "";
                string Hd = "";
                DateTime time = DateTime.Now.AddDays(-7);
                for (int i = 0; i < 7; i++)
                {
                    string NowDate = time.ToString("yyyy-MM-dd");
                    //查询企业1天的粉丝增长数据/减少数据/互动数据
                    DataSet ds = fansBll.GetDataInfo(usersContract.EId, NowDate);
                    #region 每日新增订阅人数

                    int jnum = 0;
                    int num = 0;
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        num = int.Parse(ds.Tables[0].Rows[0]["num"].ToString());
                        if (ds.Tables[1].Rows.Count == 1)
                        {
                            //粉丝有减少  
                            jnum = int.Parse(ds.Tables[1].Rows[0]["num"].ToString());
                            num = num - jnum;
                        }

                    }
                    else
                    {

                        if (ds.Tables[1].Rows.Count == 1)
                        {
                            //粉丝有减少  
                            jnum = int.Parse(ds.Tables[1].Rows[0]["num"].ToString());
                            num = num - jnum;
                        }


                    }
                    if (num < 0)
                        set += "<set name='" + DateTime.Parse(NowDate).ToString("MM-dd") + "' value='" + num + "' color='FF0000' />";
                    else
                        set += "<set name='" + DateTime.Parse(NowDate).ToString("MM-dd") + "' value='" + num + "' color='AFD8F8' />";

                    #endregion
                    #region 每日接收消息数
                    if (ds.Tables[2].Rows.Count == 1)
                    {
                        int num2 = int.Parse(ds.Tables[2].Rows[0]["num"].ToString());
                        //当天有消息
                        Hd += "<set name='" + DateTime.Parse(NowDate).ToString("MM-dd") + "' value='" + num2 + "' color='AFD8F8' />";
                    }
                    else
                    {
                        Hd += "<set name='" + DateTime.Parse(NowDate).ToString("MM-dd") + "' value='0' color='AFD8F8' />";
                    }
                    #endregion

                    time = time.AddDays(1);
                }
                #endregion
                #region 生成每日新增订阅人数 XML文件


                string xml = "<graph caption='' bgcolor='FAFAFA' canvasbgcolor='FAFAFA' xAxisName='日期' yAxisName='数量' showNames='1' decimalPrecision='1' formatNumberScale='0'>" + set + "</graph>";
                //生成文件
                //文件路径
                string filePath = "/DataXml/" + usersContract.wxOldUser + ".xml";

                System.IO.StreamWriter sw = new System.IO.StreamWriter(Request.PhysicalApplicationPath + filePath, false, System.Text.Encoding.UTF8);
                sw.WriteLine(xml);
                sw.Close();
                sw.Dispose();
                #endregion
                #region 生成每日接受消息数量


                string hdxml = "<graph caption='' bgcolor='FAFAFA' canvasbgcolor='FAFAFA' xAxisName='日期' yAxisName='数量' showNames='1' decimalPrecision='1' formatNumberScale='0'>" + Hd + "</graph>";
                //生成文件
                //文件路径
                filePath = "/DataXml/" + usersContract.wxOldUser + "_2.xml";

                sw = new System.IO.StreamWriter(Request.PhysicalApplicationPath + filePath, false, System.Text.Encoding.UTF8);
                sw.WriteLine(hdxml);
                sw.Close();
                sw.Dispose();
                #endregion
                #endregion 生成XML文件

                if (Request.QueryString["backUrl"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect(Request.QueryString["backUrl"].ToString());
                }
            }
            else
            {
                //执行失败信息
                ViewData["msg"] = "登录失败!";
                //失败视图
                return View("Error");
            }
        }

        #endregion


        #region 公用方法

        /// <summary>
        /// 生成权限XML文件
        /// </summary>
        /// <param name="userID">用户ID</param>
        private void CreateAuthorityXML()
        {
            //文件路径
            string filePath = "/FileXML/" + (Session["user"] as UsersContract).UserName + "_xml";
            //判断XML文件是否存在 存在的话删除文件
            if (System.IO.File.Exists(Server.MapPath(filePath)))
            {
                System.IO.File.Delete(Server.MapPath(filePath));
            };
            //创建XML
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode xNode = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(xNode);
            XmlNode rootNode = xmlDoc.CreateNode(XmlNodeType.Element, "zuxia_OA", null);
            xmlDoc.AppendChild(rootNode);
            UsersContract user = Session["user"] as UsersContract;
            //获取当前登录人员的所有拥有权限的系统
            DataSet ds = moduleTypeServices.GetModuleTyepIdByUser(user.UserID, user.RoleID);
            //获取全部的系统
            //List<ModuleTypeContract> moduleTypeList = moduleTypeServices.GetAllModuleType();
            //获取角色的全部权限
            DataSet ds_roleAuthority = roleAuthorityListServices.GetRoleAuthority((Session["user"] as UsersContract).UserID);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //获取当前系统下所有的菜单
                List<ModulesContract> list = modulesServices.GetModulesByTypeId(int.Parse(dr["ModuleTypeID"].ToString()));
                if (list.Count != 0)
                {
                    //创建根节点
                    XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "moduleType", null);
                    //创建根节点描述
                    AttributeParameter[] parameter = new AttributeParameter[]
                    {
                        new AttributeParameter("id","moduleType_"+dr["ModuleTypeID"]),
                        new AttributeParameter("name",dr["ModuleTypeName"].ToString()),
                        new AttributeParameter("ename",dr["ModuletypeEName"].ToString()),
                        new AttributeParameter("icon",dr["ModuletypeImg"].ToString()),
                        new AttributeParameter("sysId",dr["ModuleTypeID"].ToString())
                    };
                    XMLHelper.AddAttribute(xmlDoc, root, null, parameter);

                    //递归
                    CreateXmlByModule(list, 0, root, xmlDoc, ds_roleAuthority);
                    //匹配用户的菜单信息
                    rootNode.AppendChild(root);
                }
            }

            xmlDoc.Save(Server.MapPath(filePath));
        }

        /// <summary>
        /// 根据模块生成XML
        /// </summary>
        /// <param name="list"></param>
        /// <param name="parentID"></param>
        /// <param name="parentNode"></param>
        /// <param name="xDoc"></param>
        /// <param name="ds"></param>
        public void CreateXmlByModule(List<ModulesContract> list, int parentID, XmlNode parentNode, XmlDocument xDoc, DataSet ds)
        {
            //查询所有父节点
            List<ModulesContract> list_parentModules = list.Where(m => m.ModuleParentID == parentID).ToList();
            foreach (ModulesContract item in list_parentModules)
            {
                //判断菜单是否可用
                if (item.IsMenu == 0)
                {
                    //获取当前菜单下所有操作动作
                    List<AuthorityContract> list_Authority = authorityServices.GetAuthorityByModuleID(item.ModuleID);
                    //判断当前菜单是否拥有浏览权限
                    if (list_Authority.Where(a => a.AuthorityTag == "GN_BROWSE").SingleOrDefault() == null)
                    {
                        continue;
                    }
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (int.Parse(dr["ModuleID"].ToString()) != item.ModuleID)
                        {
                            continue;
                        }

                        #region 生成菜单节点
                        XmlNode xNode = xDoc.CreateNode(XmlNodeType.Element, "tree", null);
                        AttributeParameter[] parameter = new AttributeParameter[]
                            {
                                new AttributeParameter("id",item.ModuleID.ToString()),
                                new AttributeParameter("name",item.ModuleName),
                                new AttributeParameter("areas",item.ModuleAreas),
                                new AttributeParameter("controller",item.ModuleController),
                                new AttributeParameter("action",item.ModuleAction),
                                new AttributeParameter("parentID",item.ModuleParentID.ToString()),
                                new AttributeParameter("icon",item.ModuleIcon),
                                new AttributeParameter("typeID",item.ModuleTypeID.ToString()),
                            };
                        XMLHelper.AddAttribute(xDoc, xNode, null, parameter);
                        parentNode.AppendChild(xNode);

                        #endregion 生成菜单节点

                        #region 生成操作权限

                        foreach (AuthorityContract authority_Item in list_Authority)
                        {
                            //获取当前菜单下角色拥有的操作
                            string auhorityIDs = dr["AuhorityIDS"].ToString();
                            string[] arr_Auhority = auhorityIDs.Split(',');
                            if (!arr_Auhority.Contains(authority_Item.AuthorityID.ToString()) || authority_Item.AuthorityState != 0 || authority_Item.AuthorityState == 1)
                            {
                                continue;
                            }
                            XmlNode actionNode = xDoc.CreateNode(XmlNodeType.Element, "action", null);
                            AttributeParameter[] actionParameter = new AttributeParameter[]
                            {
                                new AttributeParameter("id",authority_Item.AuthorityID.ToString()),
                                new AttributeParameter("name",authority_Item.AuthorityName),
                                new AttributeParameter("tag",authority_Item.AuthorityTag)
                            };
                            XMLHelper.AddAttribute(xDoc, actionNode, null, actionParameter);
                            xNode.AppendChild(actionNode);
                        }

                        #endregion 生成操作权限

                        //调用方法
                        CreateXmlByModule(list, item.ModuleID, xNode, xDoc, ds);
                    }
                }
            }
        }

        /// <summary>
        /// 错误中间页
        /// </summary>
        /// <returns></returns>
        public ActionResult Error()
        {
            return View();
        } 
        
        

        /// <summary>
        /// 成功中间页
        /// </summary>
        /// <returns></returns>
        public ActionResult Success()
        {
            return View();
        }

       

        /// <summary>
        /// 警告中间页
        /// </summary>
        /// <returns></returns>
        public ActionResult Alter()
        {
            return View();
        }

        /// <summary>
        /// Web错误中间页
        /// </summary>
        /// <returns></returns>
        public ActionResult WebError()
        {
            return View();
        }
        #endregion
    }
}
