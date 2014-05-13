using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml;
using System.IO;
using WeiXin.BLL;
using WeiXin.Model;
using System.Web.Security;
using System.Text;
using WeiXin.BO;
using WeiXin.Core;
using System.Net;
using System.Web.Script.Serialization;

namespace WeiXin.Web.Areas.API.Controllers
{
    public class APIController : Controller
    {
        //企业
        EmployeeService empBll = new EmployeeService();
        BllCommon CommBll = new BllCommon();
        //Fans
        FansService FansBll = new FansService();
        GuanJianZiHuiFuService HFBll = new GuanJianZiHuiFuService();//关键字回复
        FansInteractionService FIBll = new FansInteractionService();//互动
        normal_EmpExtendsService extendsBll = new normal_EmpExtendsService();//企业的插件
        string serverUser = null;
        string clientUser = null;
        string path = "";
        string sendContent = "";

        /// <summary>
        /// 微信访问Api
        /// </summary>
        /// <returns></returns>

        public ActionResult Index(int id)
        {
            // string content = "最炫民族风";
            CommonAPI api = new CommonAPI();
            api.GetCar();
            // GetMusic(content);

            path = Server.MapPath(@"/log/log.txt");
            //根据ID查询企业Token
            employeeInfo emp = empBll.GetAEmpInfoByApi(id);
            CommBll.WriteLog(emp.ToKen, path);
            Valid(emp.ToKen);
            return View();

        }

        [HttpPost]
        public ActionResult Index(string id, string type)
        {
            path = Server.MapPath(@"/log/log.txt");
            string postStr = "";
            if (Request.HttpMethod.ToLower() == "post")
            {
                try
                {
                    Stream s = System.Web.HttpContext.Current.Request.InputStream;
                    byte[] b = new byte[s.Length];
                    s.Read(b, 0, (int)s.Length);
                    postStr = Encoding.UTF8.GetString(b);
                    if (!string.IsNullOrEmpty(postStr))
                    {
                        ResponseMsg(postStr, id);
                    }

                }
                catch (Exception ex)
                {
                    CommBll.WriteLog(ex.Message, path);

                }

            }
            return View();

        }


        #region ResponseMsg Text/Image/Music

        private void ResponseMsg(string weixinXML, string id)
        {
            string strresponse = "";
            //回复消息的部分:你的代码写在这里
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(weixinXML);
            XmlNodeList list = doc.GetElementsByTagName("xml");

            XmlNode xn = list[0];
            string FromUserName = xn.SelectSingleNode("//FromUserName").InnerText;
            string ToUserName = xn.SelectSingleNode("//ToUserName").InnerText;
            serverUser = ToUserName;
            clientUser = FromUserName;
            string content = "", Event = "";
            try
            {
                Event = xn.SelectSingleNode("//Event").InnerText;
                CommBll.WriteLog("Event:" + Event, path);
            }
            catch
            { }
            try
            {
                content = xn.SelectSingleNode("//Content").InnerText;
                CommBll.WriteLog("Content:" + content, path);
            }
            catch
            { }
            //用户操作事件
            if (Event.Equals("subscribe"))
            {
                //初次关注，检查数据库中是否存在该用户 ，无，则加入进我们的数据库
                //有，则加入，公众帐号与用户的关系表
                Fans fans = new Fans();
                fans.FromUserName = FromUserName;
                UserToEmp u2e = new UserToEmp();
                u2e.EmpID = int.Parse(id);
                u2e.UserID = FromUserName;
                u2e.SubscribeDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                u2e.SubscribeState = 1;//初始为关注状态
                FansBll.InsertFansAndToEmp(fans, u2e);
                //关注的欢迎词

                #region 更改日期：2013-7-20 将初次关注回复的文本内容转换为图文内容
                //content = empBll.GetAEmpInfoByApi(int.Parse(id)).temp1;
                //strresponse = ResponseText(content);
                #endregion
                employeeInfo emp = empBll.GetAEmpInfoByApi(int.Parse(id));
                strresponse = ResponsePicTextMsg("非常感谢您能关注[" + emp.Name + "]", emp.temp1, "http://weixin.cqzuxia.com/FileUpload/" + emp.temp4, "http://weixin.cqzuxia.com/Enterprise/web/index?sessionid=zuxia_SessionID");



            }
            else if (Event.Equals("unsubscribe"))
            {
                //删除用户与公众帐号的关系表（假删除）
                FansBll.UpdateU2E(id, FromUserName);
                // CommBll.WriteLog("用户取消关注：" + FromUserName, path);
            }
            else
            {  //查询该微信公众号的关键词进行匹配
                #region 关键词进行匹配
                #region CheckFansIsCunZai 判断粉丝是否存在
                Fans fans = new Fans()
                {
                    FromUserName = FromUserName
                };
                UserToEmp u2e = new UserToEmp()
                {
                    EmpID = int.Parse(id),
                    UserID = FromUserName,
                    SubscribeDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    SubscribeState = 1//初始为关注状态
                };
                FansBll.InsertFansAndToEmp(fans, u2e);
                #endregion
                //   CommBll.WriteLog("进入关键词匹配", path);
                if (content != null)
                {
                    sendContent = content;
                    List<GuanJianZiHuiFu> hf = HFBll.HuiFu(content, int.Parse(id));
                    if (hf == null || hf.Count < 1)
                    {
                        List<normal_EmpExtends> listex = extendsBll.GetEmpExtends(int.Parse(id));
                        #region API


                        if (listex.Count != 0)
                        {
                            //判断是否有插件内容
                            foreach (var item in listex)
                            {
                                if (content.Contains(item.normal_extends.BaseTable))
                                {
                                    //查询api
                                    CommonAPI api = new CommonAPI();
                                    #region API

                                    switch (item.normal_extends.BaseTable)
                                    {
                                        case "天气":
                                            strresponse = ResponsePicTextMsg(content + "预报", api.GetWeather(content.Substring(0, content.Length - 2)), "http://weixin.cqzuxia.com/FileUpload/201307/26/20130726135955.jpg", ""); 
                                            break;
                                        case "笑话":
                                            strresponse = ResponsePicTextMsg("笑话一箩筐", api.GetXiaoHua(), "http://weixin.cqzuxia.com/FileUpload/201307/21/20130721100709.jpg", "");
                                            break;
                                        case "@":
                                            strresponse = ResponsePicTextMsg("翻译查询", api.GetFanYi(content.Substring(1, content.Length - 1)), "http://weixin.cqzuxia.com/FileUpload/201307/26/20130726135754.jpg", "");
                                            break;
                                        case "gp":
                                            strresponse = ResponsePicTextMsg("股票查询", api.GetGuPiao(content.Substring(2, content.Length - 2)), "http://weixin.cqzuxia.com/FileUpload/201307/26/20130726135557.jpg", "");

                                            break;
                                        case "会员":
                                            //判断用户是否领取我企业的会员卡
                                            FansToCard ftc = new FansToCardService().GetFansToCardByUserID(FromUserName, int.Parse(id));
                                            if (ftc == null)
                                            {
                                                //1、用户还未领取会员卡
                                                strresponse = ResponsePicTextMsg("申请微信会员卡", "您尚未领取会员卡特权，快来点击领取吧", "http://weixin.cqzuxia.com/FileUpload/201307/25/20130725154809.jpg", "http://weixin.cqzuxia.com/Enterprise/web/MemberCard?userid=" + FromUserName + "&sessionid=" + WeiXin.Core.SecurityEncryption.DESEncrypt(id));
                                            }
                                            else
                                            {
                                                //2、用户已经领取会员卡
                                                strresponse = ResponsePicTextMsg("尊敬的会员：" + ftc.UserToEmp.Fans.TrueName, "尊敬的会员：" + ftc.UserToEmp.Fans.TrueName + "，您的会员卡号为：" + ftc.Cardid + ",会员尊享特权点击进入查询！", "http://weixin.cqzuxia.com/FileUpload/201307/25/20130725154809.jpg", "http://weixin.cqzuxia.com/Enterprise/web/MemberCard?userid=" + FromUserName + "&sessionid=" + WeiXin.Core.SecurityEncryption.DESEncrypt(id));
                                            } 
                                            break;
                                        case "kd":
                                            strresponse = ResponsePicTextMsg("快递查询", api.GetKuaiDi(content.Substring(2, content.Length - 2)), "http://weixin.cqzuxia.com/FileUpload/201307/26/20130726140210.jpg", "");

                                            break;
                                        case "音乐":
                                            strresponse = GetMusic(content.Substring(2, content.Length - 2)); break;
                                        default:
                                            break;
                                    }
                                    #endregion

                                    break;
                                }
                            }
                            CommBll.WriteLog("strresponse：" + strresponse, path);
                            if (strresponse.Length < 2)
                            {
                                #region 无回复内容，更新时间：2013-7-20 更改为图文格式


                                //content = empBll.GetAEmpInfoByApi(int.Parse(id)).temp2;
                                //strresponse = ResponseText(content);
                                #endregion
                                employeeInfo emp=empBll.GetAEmpInfoByApi(int.Parse(id));
                                strresponse = ResponsePicTextMsg("没有找到你需要的信息！", emp.temp2, "http://weixin.cqzuxia.com/FileUpload/" + emp.temp5, "http://weixin.cqzuxia.com/Enterprise/web/index?sessionid=zuxia_SessionID");

                            }

                        }
                        else
                        {


                            #region 没有数据，更新时间：2013-7-20 更改为图文格式


                            //content = empBll.GetAEmpInfoByApi(int.Parse(id)).temp2;
                            //strresponse = ResponseText(content);
                            #endregion
                            employeeInfo emp = empBll.GetAEmpInfoByApi(int.Parse(id));
                            strresponse = ResponsePicTextMsg("没有找到你需要的信息！", emp.temp2, "http://weixin.cqzuxia.com/FileUpload/" + emp.temp5, "http://weixin.cqzuxia.com/Enterprise/web/index?sessionid=zuxia_SessionID");
                        }
                        #endregion
                    }
                    else
                    {
                        string type = hf[0].GuanJianZiHuiFuType.name;
                        CommBll.WriteLog("type:" + type, path);
                        switch (type)
                        {
                            case "文字":
                                //strresponse = ResponseTextMsg(hf);
                                if (hf[0].img.Length > 1)
                                {
                                    strresponse = ResponsePicTextMsg("", hf[0].content, "http://weixin.cqzuxia.com/FileUpload/" + hf[0].img, hf[0].temp1);
                                }
                                else { strresponse = ResponsePicTextMsg("", hf[0].content, "", hf[0].temp1); }
                                break;
                            case "图文":
                                strresponse = ResponsePicTextMsg(hf);
                                break;
                            default:
                                break;
                        }
                    }
                }
                #endregion
            }
            CommBll.WriteLog("ToUserName：" + ToUserName, path);
            CommBll.WriteLog("clientUser：" + clientUser, path);
            FansInteraction fi = new FansInteraction()
            {
                Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                EmpId = int.Parse(id),
                FansOpenId = clientUser,
                SendContent = sendContent,
                ReturnContent = strresponse,
                Temp1 = serverUser,
            };
            //互动
            FIBll.InsertInteraction(fi);
            strresponse = strresponse.Replace("zuxia_UserID", clientUser);
            strresponse = strresponse.Replace("zuxia_SessionID", WeiXin.Core.SecurityEncryption.DESEncrypt(id));
            Response.Write(strresponse);
        }

        /// <summary>
        /// 回复文本内容
        /// </summary>
        /// <param name="to">接收者</param>
        /// <param name="from">消息来源</param>
        /// <param name="content">消息内容</param>
        /// <returns>生成的输出文本</returns>
        public string ResponseText(string content)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<xml>");
            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", clientUser);
            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", serverUser);
            sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTime.Now.Ticks.ToString());
            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", content);
            sb.AppendFormat("<FuncFlag>0</FuncFlag>");
            sb.AppendFormat("</xml>");
            return sb.ToString();
        }

        /// <summary>
        /// 回复文本内容（文字消息只获取一条内容）
        /// </summary>
        /// <param name="to">接收者</param>
        /// <param name="from">消息来源</param>
        /// <param name="content">消息内容</param>
        /// <returns>生成的输出文本</returns>
        public string ResponseTextMsg(List<GuanJianZiHuiFu> hf)
        {
            string content = "";
            foreach (var item in hf)
            {
                content = item.content.Trim();
                break;
            }
            StringBuilder sb = new StringBuilder();
            content = content.Replace("<p>", "");
            content = content.Replace("</p>", "");
            content = content.Replace("<br />", "");
            sb.AppendFormat("<xml>");
            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", clientUser);
            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", serverUser);
            sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTime.Now.Ticks.ToString());
            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", content);
            sb.AppendFormat("<FuncFlag>0</FuncFlag>");
            sb.AppendFormat("</xml>");

            return sb.ToString();
        }

        /// <summary>
        /// 回复图文内容
        /// </summary>
        /// <param name="to">接收者</param>
        /// <param name="from">消息来源</param>
        /// <param name="content">消息内容</param>
        /// <returns>生成的输出文本</returns>
        public string ResponsePicTextMsg(List<GuanJianZiHuiFu> articles)
        {
            if (articles == null)
            {
                articles = new List<GuanJianZiHuiFu>();
            }
            int count = 0;
            StringBuilder sbItems = new StringBuilder();
            foreach (GuanJianZiHuiFu article in articles)
            {
                try
                {

                    StringBuilder sbTemp = new StringBuilder();
                    sbTemp.AppendFormat("<item>");
                    sbTemp.AppendFormat("   <Title><![CDATA[{0}]]></Title>", article.title);
                    if (article.typeId == 1)
                    {
                        sbTemp.AppendFormat("   <Description><![CDATA[{0}]]></Description>", article.content);
                    }
                    else {
                        sbTemp.AppendFormat("   <Description><![CDATA[{0}]]></Description>", article.title);
                    }
                    if (article.img.Length > 0)
                    {
                        sbTemp.AppendFormat("   <PicUrl><![CDATA[{0}]]></PicUrl>", "http://weixin.cqzuxia.com/FileUpload/" + article.img);
                    }
                    else
                    {
                        sbTemp.AppendFormat("   <PicUrl><![CDATA[{0}]]></PicUrl>", "");
                    }

                    if (article.temp1 != null && article.temp1.Contains("http"))
                    {
                        sbTemp.AppendFormat("   <Url><![CDATA[{0}]]></Url>", article.temp1);
                    }
                    else
                    {

                        sbTemp.AppendFormat("   <Url><![CDATA[{0}]]></Url>", "http://weixin.cqzuxia.com/Article/Article/" + article.hfId + "?sessionid=" + clientUser);
                    }
                    sbTemp.AppendFormat("   <FuncFlag>0</FuncFlag>");
                    sbTemp.AppendFormat("</item>");
                    sbItems.Append(sbTemp.ToString());
                    count++;
                    if (count == 9)
                    {
                        break;
                    }
                }
                catch
                {
                }
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<xml>");
            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", clientUser);
            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", serverUser);
            sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTime.Now.Ticks.ToString());
            sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
            sb.AppendFormat("<ArticleCount>{0}></ArticleCount>", count);
            sb.AppendFormat("<Articles>");
            sb.AppendFormat(sbItems.ToString());
            sb.AppendFormat("</Articles>");
            sb.AppendFormat("<FuncFlag>0</FuncFlag>");
            sb.AppendFormat("</xml>");

            return sb.ToString();
        }


        /// <summary>
        /// 回复图文消息
        /// </summary>
        /// <param name="Title">图文标题</param>
        /// <param name="Description">图文介绍</param>
        /// <param name="PicUrl">图片地址</param>
        /// <param name="Url">跳转页面</param>
        /// <returns></returns>
        public string ResponsePicTextMsg(string Title, string Description, string PicUrl, string Url)
        {

            Description = Description.Replace("<p>", "");
            Description = Description.Replace("</p>", "");
            Description = Description.Replace("<br />", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<xml>");
            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", clientUser);
            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", serverUser);
            sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTime.Now.Ticks.ToString());
            sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
            sb.AppendFormat("<ArticleCount>{0}></ArticleCount>", 1);
            sb.AppendFormat("<Articles>");
            sb.AppendFormat("<item>");
            sb.AppendFormat("<Title><![CDATA[{0}]]></Title>", Title);
            sb.AppendFormat("<Description><![CDATA[{0}]]></Description>", Description);

            sb.AppendFormat("<PicUrl><![CDATA[{0}]]></PicUrl>", PicUrl);
            sb.AppendFormat("<Url><![CDATA[{0}]]></Url>", Url);
            sb.AppendFormat("</item>");
            sb.AppendFormat("</Articles>");
            sb.AppendFormat("<FuncFlag>0</FuncFlag>");
            sb.AppendFormat("</xml>");

            return sb.ToString();
        }


        /// <summary>
        /// 回复音乐内容
        /// </summary>
        /// <param name="to">接收者</param>
        /// <param name="from">消息来源</param>
        /// <param name="title">标题</param>
        /// <param name="description">描述信息</param>
        /// <param name="musicurl">音乐链接</param>
        /// <param name="hqmusicurl">高质量音乐链接，WIFI环境优先使用该链接播放音乐</param>
        /// <returns>生成的输出文本</returns>
        public string ResponseMusicMsg(string title, string description, string musicurl, string hqmusicurl)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<xml>");
            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", clientUser);
            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", serverUser);
            sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTime.Now.Ticks.ToString());
            sb.AppendFormat("<MsgType><![CDATA[music]]></MsgType>");
            sb.AppendFormat("<Music>");
            sb.AppendFormat("   <Title><![CDATA[{0}]]></Title>", title);
            sb.AppendFormat("   <Description><![CDATA[{0}]]></Description>", description);
            sb.AppendFormat("   <MusicUrl><![CDATA[{0}]]></MusicUrl>", musicurl);
            sb.AppendFormat("   <HQMusicUrl><![CDATA[{0}]]></HQMusicUrl>", hqmusicurl);
            sb.AppendFormat("   <FuncFlag>0</FuncFlag>");
            sb.AppendFormat("</Music>");
            sb.AppendFormat("</xml>");
            return sb.ToString();
        }
        #endregion
        #region 音乐

        public string GetMusic(string MusicName)
        {
            WebClient MyWebClient = new WebClient();
            MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于对向Internet资源的请求进行身份验证的网络凭据。

            Byte[] Comp = MyWebClient.DownloadData("http://api2.sinaapp.com/search/music/?appkey=0020130430&appsecert=fa6095e1133d28ad&reqtype=music&keyword=" + MusicName); //获取快递公司数据 
            var pageHtml = Encoding.UTF8.GetString(Comp); //如果获取网站页面采用的是UTF-8，则使用这句
            JavaScriptSerializer js = new JavaScriptSerializer();
            var obj = js.DeserializeObject(pageHtml);
            Dictionary<string, object> dic = obj as Dictionary<string, object>;
            if (dic["errcode"].ToString() == "0")
            {
                Dictionary<string, object> dic2 = dic.Values.ToArray()[2] as Dictionary<string, object>;
                string title = dic2["title"].ToString();
                string musicurl = dic2["musicurl"].ToString();
                string hqmusicurl = dic2["hqmusicurl"].ToString();
                return ResponseMusicMsg(title, "按住可转发好友，播放失败请换首歌！", musicurl, hqmusicurl);
            }
            else
            {
                return ResponseText("[流泪] Sorry~ 你要的音乐我没有找到~ [流泪]");
            }

        }
        #endregion
        #region 验证方法


        private void Valid(string token)
        {
            string echoStr = Request.QueryString["echoStr"].ToString();
            CommBll.WriteLog(echoStr, path);

            if (CheckSignature(token))
            {
                if (!string.IsNullOrEmpty(echoStr))
                {
                    CommBll.WriteLog("Response.Write:" + echoStr, path);

                    Response.Write(echoStr);
                    Response.End();
                }
            }
        }

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// <returns></returns>
        private bool CheckSignature(string token)
        {

            string signature = Request.QueryString["signature"].ToString();

            string timestamp = Request.QueryString["timestamp"].ToString();

            string nonce = Request.QueryString["nonce"].ToString();
            string[] ArrTmp = { token, timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            if (tmpStr.Equals(signature))
            {
                CommBll.WriteLog("验证成功", path);
                return true;
            }
            else
            {
                CommBll.WriteLog("验证失败", path);
                return false;
            }


        }

        #endregion
    }
}
