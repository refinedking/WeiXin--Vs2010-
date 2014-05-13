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
using WeiXin.Model;
using WeiXin.Web.Controllers;
using WeiXin.BO;
using System.Data;
using System.Text;
using System.Collections.Specialized;

namespace WeiXin.Web.Areas.Enterprise.Controllers
{
    [WebException]
    public class WebController : CommonController
    {
        public int pageSize = 5;
        public string PageTitle;
        public int Eid = 0;
        normal_channelService ChannelBll = new normal_channelService();//频道
        normal_ClassService ClassBll = new normal_ClassService();//栏目
        module_articleService ArticleBll = new module_articleService();//文章
        module_productService ProBll = new module_productService();//产品
        normal_templateService NTBll = new normal_templateService();//模版
        EmployeeService empBll = new EmployeeService();//企业微信
        FansService FansBll = new FansService();//Fans

        CardConfigService CardConfigBll = new CardConfigService();//会员卡配置
        FansToCardService Fans2CardBll = new FansToCardService();//会员卡领取
        /// <summary>
        /// 1、企业网站首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string sessionid)
        {
            ViewData["SessionID"] = sessionid;
            Eid = int.Parse(SecurityEncryption.DESDecrypt(sessionid));
            //获取企业的首页模版 
            List<normal_template> list = NTBll.getTemplateByList(Eid, "system", "index");

            PageTitle = empBll.GetEmpInfoByEId(Eid).Name;
            //得到模版
            string content = list[0].content;
            //解析频道标签
            replaceTag_ChannelLoop(ref content, Eid);
            content = content.Replace("{$Sessionid}", sessionid);
            replaceTag_ContentLoop(ref   content);
            replaceTag_SiteConfig(ref content);


            ViewData["content"] = content;
            //查询企业信息
            //ViewData["Title"] = empBll.GetEmpInfoByEId(eid).Name;
            ////查询频道（只显示4个）
            //ViewData["Top4Channel"] = ChannelBll.GetChannelByList(eid, 4);
            ////------------------------------------------------------------------
            ////查询5个新闻置顶的新闻
            //List<module_article> list5News = ArticleBll.GetArticleTop5(eid, 5);
            ////如果文章数量不足5个，则查询是否有产品置顶的
            //List<module_product> list5Pro = ProBll.GetProductTopNum(eid, 5 - list5News.Count);
            //ViewData["TopNews"] = list5News;
            //ViewData["TopPro"] = list5Pro;
            ////-------------------------------------------------------------------
            ////查询5个焦点图，置顶，有图，焦点
            //List<module_article> listIndexImg = ArticleBll.GetIndexFocusImg(eid, 5);
            //ViewData["IndexImg"] = listIndexImg;
            //ViewData["IndexProImg"] = ProBll.GetProductFocusImg(eid, 5 - listIndexImg.Count);
            return View();
        }


        #region 文章模型

        /// <summary>
        /// 2、文章频道页
        /// </summary>
        /// <param name="ccid">频道ID</param>
        /// <param name="sessionid">Eid</param>
        /// <returns></returns>
        public ActionResult ArticleClassList(int ccid, string sessionid)
        {
            int eid = int.Parse(SecurityEncryption.DESDecrypt(sessionid));
            ViewData["SessionID"] = sessionid;
            string content = GetClassListContent(eid, "article", "channel", ccid);
            PageTitle = ArticleBll.GetArticleBySql(@"select a.Title+'_'+b.Name from normal_channel a ,employeeInfo b
where a.eid=b.Eid and a.Id=" + ccid).Rows[0][0].ToString();
            replaceTag_ContentLoop(ref content);
            replaceTag_SiteConfig(ref content);
            content = content.Replace("{$Sessionid}", sessionid);
            ViewData["content"] = content;
            ////查询频道下的3个栏目
            //ViewData["Top3Class"] = ClassBll.GetClassByccidAndEid(ccid, eid, 3);
            ////点击频道进来的时候，显示频道下的5个焦点新闻
            //return View(ArticleBll.GetArticleTop5ByClass(eid, 5, ccid));
            return View();
        }

        /// <summary>
        /// 3、文章栏目页 
        /// </summary>
        /// <param name="id">栏目ID</param>
        /// <param name="ccid">频道ID</param>
        /// <param name="sessionid">Eid</param>
        /// <returns></returns>
        public ActionResult ArticleList(int id, int ccid, string sessionid)
        {
            int eid = int.Parse(SecurityEncryption.DESDecrypt(sessionid));
            ViewData["SessionID"] = sessionid;
            GetSiteClassPage(id.ToString(), 1, ccid, eid);
            string content = GetClassListContent(eid, "article", "class", ccid);
            PageTitle = ArticleBll.GetArticleBySql(@"select  a.title+'_'+b.Title+'_'+c.Name from normal_class a,normal_channel b,employeeInfo c where a.ChannelId=b.Id and b.eid=c.Eid and a.Id=" + id).Rows[0][0].ToString();
            content = content.Replace("<zuxia:site.page.title/>", PageTitle);
            content = content.Replace("{$ClassId}", id.ToString());
            content = content.Replace("{$ChannelId}", ccid.ToString());
            replaceTag_ContentLoop(ref content);
            content = GetSiteClassPage(id.ToString(), 1, ccid, eid);
            content = content.Replace("{$Page}", pageIndex.ToString());
            content = content.Replace("{$Sessionid}", sessionid);
            content = content.Replace("{$ClassId}", id.ToString());
            replaceTag_SiteConfig(ref content);
            ViewData["content"] = content;


            //查询频道下的3个栏目
            //ViewData["Top3Class"] = ClassBll.GetClassByccidAndEid(ccid, eid, 3);
            ////查询栏目下的文章， 
            //PagerList<module_article> list = ArticleBll.GetArticleByPager(ccid, id, pageIndex, pageSize, "");
            //ViewData["PageIndex"] = pageIndex;
            //ViewData["id"] = id;
            //ViewData["ccid"] = ccid;
            ////栏目页标题= 频道名称+用户名+愉生活
            //ViewData["title"] = ClassBll.GetClassByIdAndEid(id, eid).Title + "-" + ChannelBll.GetChannelByidAndEid(eid, ccid).Title + "-" + empBll.GetEmpInfoByEId(eid).Name + "-愉生活";
            //return View(list);
            return View();
        }


        /// <summary>
        /// 文章内容页
        /// </summary>
        /// <param name="sessionid">企业ID</param>
        /// <param name="id">新闻ID</param>
        /// <returns></returns>
        public ActionResult Article(string sessionid, int id)
        {
            int eid = int.Parse(SecurityEncryption.DESDecrypt(sessionid));
            ViewData["SessionID"] = sessionid;
            module_article article = ArticleBll.GetArticleById(id);//文章内容
            //article.ClassId
            string content = GetContent(eid, "article", "content", id);
            content = content.Replace("{$Sessionid}", sessionid);
            ViewData["content"] = content;
            //根据文章内容，查询栏目3个！
            //ViewData["Top3Class"] = ClassBll.GetClassByccidAndEid(article.ChannelId, eid, 3);
            ArticleBll.UpdateArticle("ViewNum", article.ViewNum + 1, id);
            //更新文章的阅读次数
            return View();
        }

        #endregion

        #region 产品模型

        /// <summary>
        /// 产品频道页
        /// </summary>
        /// <param name="ccid"></param>
        /// <param name="sessionid"></param>
        /// <returns></returns>
        public ActionResult ProductClassList(int ccid, string sessionid)
        {
            int eid = int.Parse(SecurityEncryption.DESDecrypt(sessionid));
            ViewData["SessionID"] = sessionid;
            string content = GetClassListContent(eid, "product", "channel", ccid);
            PageTitle = ArticleBll.GetArticleBySql(@"select a.Title+'_'+b.Name from normal_channel a ,employeeInfo b
where a.eid=b.Eid and a.Id=" + ccid).Rows[0][0].ToString();
            replaceTag_ContentLoop(ref content);
            replaceTag_SiteConfig(ref content);
            content = content.Replace("{$Sessionid}", sessionid);
            ViewData["content"] = content;


            //查询频道下的3个栏目
            //  ViewData["Top3Class"] = ClassBll.GetClassByccidAndEid(ccid, eid, 3);
            //点击频道进来的时候，显示频道下的5个焦点新闻
            //  return View(ProBll.GetProductTop5ByClass(eid, 5, ccid));
            return View();
        }


        /// <summary>
        /// 产品列表页
        /// </summary>
        /// <param name="id">栏目</param>
        /// <param name="ccid">频道</param>
        /// <param name="sessionid">eid</param>
        /// <returns></returns>
        public ActionResult productList(int id, int ccid, string sessionid)
        {

            //int eid = int.Parse(SecurityEncryption.DESDecrypt(sessionid));
            //ViewData["SessionID"] = sessionid;
            ////查询频道下的3个栏目
            //ViewData["Top3Class"] = ClassBll.GetClassByccidAndEid(ccid, eid, 3);

            ////查询栏目下的产品， 
            //PagerList<module_product> list = ProBll.GetProductByPager(ccid, pageIndex, pageSize, "");

            //ViewData["PageIndex"] = pageIndex;
            //ViewData["classid"] = id;
            //ViewData["ccid"] = ccid;
            ////栏目页标题= 栏目名称+频道名称+用户名+愉生活
            //ViewData["title"] = ClassBll.GetClassByIdAndEid(id, eid).Title + "-" + ChannelBll.GetChannelByidAndEid(eid, ccid).Title + "-" + empBll.GetEmpInfoByEId(eid).Name + "-愉生活";
            //return View(list);
            int eid = int.Parse(SecurityEncryption.DESDecrypt(sessionid));
            ViewData["SessionID"] = sessionid;
            string content = GetSiteClassPage(id.ToString(), 1, ccid, eid);
            // string content = GetClassListContent(eid, "product", "class", ccid);
            PageTitle = ArticleBll.GetArticleBySql(@"select  a.title+'_'+b.Title+'_'+c.Name from normal_class a,normal_channel b,employeeInfo c where a.ChannelId=b.Id and b.eid=c.Eid and a.Id=" + id).Rows[0][0].ToString();
            content = content.Replace("<zuxia:site.page.title/>", PageTitle);
            content = content.Replace("{$ClassId}", id.ToString());
            content = content.Replace("{$ChannelId}", ccid.ToString());
            replaceTag_ContentLoop(ref content);
            content = GetSiteClassPage(id.ToString(), 1, ccid, eid);
            content = content.Replace("{$Page}", pageIndex.ToString());
            content = content.Replace("{$Sessionid}", sessionid);
            content = content.Replace("{$ClassId}", id.ToString());
            replaceTag_SiteConfig(ref content);
            ViewData["content"] = content;
            return View();
        }
        /// <summary>
        /// 产品内容页
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Product(string sessionid, int id)
        {
            //int eid = int.Parse(SecurityEncryption.DESDecrypt(sessionid));
            //module_product product = ProBll.GetProductById(id);//产品内容
            ////根据产品内容，查询栏目3个！
            //ViewData["Top3Class"] = ClassBll.GetClassByccidAndEid(product.ChannelId, eid, 3);

            //ViewData["SessionID"] = sessionid;
            //ViewData["classid"] = product.ClassId;
            //ViewData["PageIndex"] = pageIndex;
            //ViewData["ccid"] = product.ChannelId;

            ////查询该企业其他的5个商品
            //ViewData["OtherPro"] = ProBll.GetProductByPager(id, product.ChannelId, int.Parse(product.ClassId.ToString()), pageIndex, pageSize, "");
            //ProBll.UpdateProduct("ViewNum", int.Parse(product.ViewNum.ToString()) + 1, id);
            ////更新文章的阅读次数
            //return View(product);

            int eid = int.Parse(SecurityEncryption.DESDecrypt(sessionid));
            ViewData["SessionID"] = sessionid;
            module_product product = ProBll.GetProductById(id);//产品内容 
            string content = GetContent(eid, "product", "content", id);
            content = content.Replace("{$Sessionid}", sessionid);
            ViewData["content"] = content;
            //更新阅读次数
            ProBll.UpdateProduct("ViewNum", int.Parse(product.ViewNum.ToString()) + 1, id);

            return View();
        }

        #endregion

        #region 解析
        /// <summary>
        /// 获得栏目页内容(频道ID只能从外部传入,频道ID不能为0)
        /// </summary>
        /// <param name="_classid"></param>
        /// <param name="_currentpage"></param>
        /// <returns></returns>
        public string GetSiteClassPage(string _classid, int _currentpage, int channelId, int eid)
        {
            normal_class _class = ClassBll.GetClassModelByidAndCCid(int.Parse(_classid), channelId);
            int _totalcount = ClassBll.GetClassBySql("select * from [module_" + _class.normal_channel.Type + "] where  [ClassId] in (Select id FROM [normal_class] WHERE [IsOut]=0 ) and [IsPass]=1 AND [ChannelId]=" + channelId).Rows.Count;
            int _pagecount = WeiXin.Core.Int.PageCount(_totalcount, _class.PageSize);

            System.Collections.ArrayList ContentList = getClassSinglePage(_class, _pagecount, _currentpage, eid);
            string _pagestr = ContentList[0].ToString();
            if (ContentList.Count > 2)
            {
                string ViewStr = ContentList[1].ToString();
                _pagestr = _pagestr.Replace(ViewStr, ContentList[2].ToString());
            }

            // ExcuteLastHTML(ref _pagestr);
            return _pagestr;

        }
        /// <summary>
        /// 解析公共标签
        /// </summary>
        /// <param name="_pagestr">原始内容</param>
        /// <returns></returns>
        public void ReplacePublicTag(ref string _pagestr)
        {
            //  replaceTag_Include(ref _pagestr);
            replaceTag_SiteConfig(ref _pagestr);
            //  replaceTag_GetRemoteWeb(ref _pagestr);
            // replaceTag_ChannelLoop(ref _pagestr);
        }


        private System.Collections.ArrayList getClassSinglePage(normal_class _class, int _pagecount, int _page, int eid)
        {
            string pId = string.Empty;
            string _pagestr = string.Empty;

            //得到模板方案组ID/模板内容

            _pagestr = GetClassListContent(eid, _class.normal_channel.Type, "class", _class.ChannelId);
            ReplacePublicTag(ref _pagestr);


            //   ReplaceChannelClassLoopTag(ref _pagestr);
            // ReplaceClassTag(ref _pagestr, _class.Id);
            //   ReplaceContentLoopTag(ref _pagestr);
            System.Collections.ArrayList ContentList = new System.Collections.ArrayList();
            ContentList.Add(_pagestr);
            getClassSinglePageListBody(_class, ref ContentList, _pagecount, _page);

            return ContentList;
        }
        private void getClassSinglePageListBody(normal_class _class, ref System.Collections.ArrayList ContentList, int _pagecount, int _page)
        {

            string whereStr = string.Empty;
            string _pagestr = ContentList[0].ToString();
            whereStr = " [ClassId] in (SELECT ID FROM [normal_class] WHERE [IsOut]=0 and id=" + _class.Id + ")";
            whereStr += " AND [IsPass]=1 AND [ChannelId]=" + _class.ChannelId;
            int _pagesize = (_class.PageSize < 1) ? 5 : _class.PageSize;

            System.Collections.ArrayList TagArray = WeiXin.Core.Strings.GetHtmls(_pagestr, "{$zuxia:class(", "{$/zuxia:class}", false, false);
            if (TagArray.Count > 0)//标签存在
            {
                string LoopBody = string.Empty;
                string TempStr = string.Empty;
                string FiledsStr = string.Empty;
                int StartTag, EndTag;
                StartTag = TagArray[0].ToString().IndexOf(")}", 0);
                FiledsStr = TagArray[0].ToString().Substring(0, StartTag).ToLower();
                if (!("," + FiledsStr + ",").Contains(",adddate,")) FiledsStr += ",adddate";
                EndTag = TagArray[0].ToString().Length;
                LoopBody = "{$zuxia:class(" + TagArray[0].ToString() + "{$/zuxia:class}";
                TempStr = TagArray[0].ToString().Substring(StartTag + 2, EndTag - StartTag - 2).Replace("<#foreach content>", "<#foreach collection=\"${contents}\" var=\"field\" index=\"i\">");//需要循环的部分
                ContentList.Add(LoopBody);

                if (_pagecount > 0)
                {
                    if (_page == 0)
                    {
                        for (int i = 1; i < _pagecount + 1; i++)
                        {
                            NameValueCollection orders = new NameValueCollection();
                            orders.Add("AddDate", "desc");
                            orders.Add("Id", "desc");
                            string wStr = WeiXin.Core.SqlHelp.GetMultiOrderPagerSQL("Id,ChannelId,ClassId,[IsPass],[FirstPage]," + FiledsStr,
                                "module_" + _class.normal_channel.Type,
                                _pagesize,
                                i,
                                orders,
                                whereStr);
                            DataSet dtContent = ArticleBll.GetArticleBySql2(wStr);
                            ContentList.Add(operateContentTag(_class.normal_channel.Type, dtContent, TempStr));
                            dtContent.Clear();
                            dtContent.Dispose();
                        }
                    }
                    else
                    {
                        _page = _page == 0 ? 1 : _page;
                        NameValueCollection orders = new NameValueCollection();
                        orders.Add("AddDate", "desc");
                        orders.Add("Id", "desc");
                        string wStr = WeiXin.Core.SqlHelp.GetMultiOrderPagerSQL("Id,ChannelId,ClassId,[IsPass],[FirstPage]," + FiledsStr,
                              "module_" + _class.normal_channel.Type,
                            _pagesize,
                            _page,
                            orders,
                            whereStr);
                        DataSet dtContent = ArticleBll.GetArticleBySql2(wStr);
                        ContentList.Add(operateContentTag(_class.normal_channel.Type, dtContent, TempStr));
                        dtContent.Clear();
                        dtContent.Dispose();
                    }
                }
                else
                    ContentList.Add("  ");
            }


        }


        /// <summary>
        /// 替换循环频道标签(将频道信息赋值给循环体)(解析次序：5)
        /// </summary>
        /// <param name="_pagestr"></param>
        private void replaceTag_ChannelLoop(ref string _pagestr, int eid)
        {
            string RegexString = "<zuxia:channelloop (?<tagcontent>.*?)>(?<tempstr>.*?)</zuxia:channelloop>";
            string[] _tagcontent = WeiXin.Core.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
            string[] _tempstr = WeiXin.Core.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
            if (_tagcontent.Length > 0)//标签存在
            {
                string _loopbody = string.Empty;
                string _replacestr = string.Empty;
                string _viewstr = string.Empty;
                string _tagrepeatnum, _tagisnav, _tagselectids, _tagorderfield, _tagordertype, _tagwherestr = string.Empty;
                for (int i = 0; i < _tagcontent.Length; i++)
                {
                    _loopbody = "<zuxia:channelloop " + _tagcontent[i] + ">" + _tempstr[i] + "</zuxia:channelloop>";
                    _tagisnav = WeiXin.Core.Strings.AttributeValue(_tagcontent[i], "isnav");
                    if (_tagisnav == "") _tagisnav = "0";
                    _tagrepeatnum = WeiXin.Core.Strings.AttributeValue(_tagcontent[i], "repeatnum");
                    _tagselectids = WeiXin.Core.Strings.AttributeValue(_tagcontent[i], "selectids");
                    _tagorderfield = WeiXin.Core.Strings.AttributeValue(_tagcontent[i], "orderfield");
                    if (_tagorderfield == "") _tagorderfield = "id";
                    _tagordertype = WeiXin.Core.Strings.AttributeValue(_tagcontent[i], "ordertype");
                    if (_tagordertype == "") _tagordertype = "asc";
                    _tagwherestr = WeiXin.Core.Strings.AttributeValue(_tagcontent[i], "wherestr");

                    string _sql = "select top " + _tagrepeatnum + " * FROM [normal_channel] WHERE [Enabled]=1";
                    if (_tagisnav == "1")
                        _sql += " AND [IsNav]=1";
                    if (_tagselectids != "")
                        _sql += " AND id in (" + _tagselectids.Replace("|", ",") + ")";
                    if (_tagwherestr != "")
                        _sql += " and " + _tagwherestr;
                    _sql += " and eid=" + eid;
                    _sql += " ORDER BY " + _tagorderfield + " " + _tagordertype;

                    List<normal_channel> listChannel = ChannelBll.ListChannelDT2List(_sql);
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in listChannel)
                    {
                        _viewstr = _tempstr[i];
                        ChannelBll.ExecuteTags(ref _viewstr, item);
                        sb.Append(_viewstr);
                    }
                    _pagestr = _pagestr.Replace(_loopbody, sb.ToString());
                }
            }
        }


        /// <summary>
        /// 解析文章内容，获取内容源
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="type"></param>
        /// <param name="stype"></param>
        /// <param name="id">文章ID</param>
        /// <returns></returns>
        public string GetContent(int eid, string type, string stype, int id)
        {
            List<normal_template> list = NTBll.getTemplateByList(eid, type, stype);
            //得到模版
            string content = list[0].content;
            //替换标签: 标题格式：文章-栏目-频道-站点-尾巴
            //查询新闻内容 
            //  content = content.Replace("{$_title}", article.Title);
            ReplacePublicTag(ref   content, id, type);

            return content;
        }

        /// <summary>
        /// 解析文章栏目页 ，获取内容源
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="type"></param>
        /// <param name="stype"></param>
        /// <param name="ccid"></param>
        /// <returns></returns>
        public string GetClassListContent(int eid, string type, string stype, int ccid)
        {
            List<normal_template> list = NTBll.getTemplateByList(eid, type, stype);
            //得到模版
            string content = list[0].content;
            content = content.Replace("{$ChannelId}", ccid.ToString());
            replaceTag_ChannelClassLoop(ref content);

            return content;
        }
        /// <summary>
        /// 解析公共标签  1 
        /// </summary>
        /// <param name="_pagestr">原始内容</param>
        /// <param name="id">文章ID</param>
        /// <returns></returns>
        public void ReplacePublicTag(ref string _pagestr, int id, string type)
        {
            ReplactContentTag(ref   _pagestr, id, type);
            replaceTag_ChannelClassLoop(ref _pagestr);
        }
        /// <summary>
        /// 解析内容 2
        /// </summary>
        /// <param name="_pagestr"></param>
        /// <param name="id">文章ID</param>
        private void ReplactContentTag(ref string _pagestr, int id, string type)
        {
            switch (type)
            {
                case "article":
                    module_article article = ArticleBll.GetArticleById(id);
                    PageTitle = article.Title + "_" + article.normal_class.Title + "_" + article.normal_channel.Title + "_" + article.normal_channel.employeeInfo.Name;
                    replaceTag_SiteConfig(ref _pagestr);
                    executeTag_Content(ref _pagestr, article);
                    break;
                case "product":
                    module_product product = ProBll.GetProductById(id);//产品内容
                    PageTitle = product.Title + "_" + product.normal_class.Title + "_" + product.normal_channel.Title + "_" + product.normal_channel.employeeInfo.Name;
                    replaceTag_SiteConfig(ref _pagestr);
                    executeTag_Product(ref _pagestr, product);
                    break;
                default:
                    break;
            }

        }
        /// <summary>
        /// 替换公共标签(解析次序：3)
        /// </summary>
        /// <param name="_pagestr"></param>
        private void replaceTag_SiteConfig(ref string _pagestr)
        {
            _pagestr = _pagestr.Replace("{site.Url}", "http://weixin.cqzuxia.com/");
            _pagestr = _pagestr.Replace("<zuxia:site.author/>", "Mr.Wang");
            _pagestr = _pagestr.Replace("<zuxia:site.page.title/>", PageTitle);
            _pagestr = _pagestr.Replace("<zuxia:site.page.basehref/>", "http://weixin.cqzuxia.com/");

        }
        /// <summary>
        /// 替换栏目标签
        /// </summary>
        /// <param name="_pagestr"></param>
        public void ReplaceClassTag(ref string _pagestr, string _ClassId)
        {
            executeTag_Class(ref _pagestr, _ClassId);

        }
        /// <summary>
        /// 解析栏目标签1 点击频道进入
        /// </summary>
        /// <param name="_pagestr"></param>
        /// <param name="_classid"></param>
        /// <returns></returns>
        private void executeTag_Class(ref string _pagestr, string _classid)
        {
            string sql = "SELECT a.[Id],a.[Title],a.[Info],[Img],[Keywords],[Content],[TopicNum],[Code],len(code) as len,[ChannelId],[ParentId],b.type FROM [normal_class] a ,normal_channel b where a.ChannelId=b.id and a.[IsOut]=0 AND a.[Id]=" + _classid;
            DataTable _dt = ClassBll.GetClassBySql(sql);
            if (_dt.Rows.Count > 0)
            {
                string _channelid = _dt.Rows[0]["ChannelId"].ToString();
                string _parentid = _dt.Rows[0]["ParentId"].ToString();
                _pagestr = _pagestr.Replace("{$ClassId}", _dt.Rows[0]["Id"].ToString());
                _pagestr = _pagestr.Replace("{$ClassName}", _dt.Rows[0]["Title"].ToString());
                _pagestr = _pagestr.Replace("{$ClassInfo}", _dt.Rows[0]["Info"].ToString());
                _pagestr = _pagestr.Replace("{$ClassKeywords}", _dt.Rows[0]["Keywords"].ToString());
                _pagestr = _pagestr.Replace("{$ClassContent}", _dt.Rows[0]["Content"].ToString());
                _pagestr = _pagestr.Replace("{$ClassImg}", _dt.Rows[0]["Img"].ToString());
                _pagestr = _pagestr.Replace("{$ClassTopicNum}", _dt.Rows[0]["TopicNum"].ToString());
                _pagestr = _pagestr.Replace("{$ClassCode}", _dt.Rows[0]["Code"].ToString());

                _pagestr = _pagestr.Replace("{$ClassLink}", "/Enterprise/Web/" + _dt.Rows[0]["type"].ToString().ToLower() + "List/" + _dt.Rows[0]["Id"].ToString() + "?sessionid={$Sessionid}&ccid=" + _channelid);


            }
        }
        /// <summary>
        /// 替换频道栏目循环标签(不支持跨频道)
        /// </summary>
        /// <param name="_pagestr"></param>
        private void replaceTag_ChannelClassLoop(ref string _pagestr)
        {

            string RegexString = "<zuxia:classloop (?<tagcontent>.*?)>(?<tempstr>.*?)</zuxia:classloop>";
            string[] _tagcontent = WeiXin.Core.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
            string[] _tempstr = WeiXin.Core.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
            if (_tagcontent.Length > 0)//标签存在
            {
                string _loopbody = string.Empty;
                string _replacestr = string.Empty;
                string _viewstr = string.Empty;
                string _tagrepeatnum, _tagselectids, _tagdepth, _tagparentid, _tagwherestr, _tagorderfield, _tagordertype, _hascontent = string.Empty;
                for (int i = 0; i < _tagcontent.Length; i++)
                {
                    _loopbody = "<zuxia:classloop " + _tagcontent[i] + ">" + _tempstr[i] + "</zuxia:classloop>";
                    string _tagchannelid = WeiXin.Core.Strings.AttributeValue(_tagcontent[i], "channelid");
                    _tagrepeatnum = WeiXin.Core.Strings.AttributeValue(_tagcontent[i], "repeatnum");
                    _tagselectids = WeiXin.Core.Strings.AttributeValue(_tagcontent[i], "selectids");
                    _tagdepth = WeiXin.Core.Strings.AttributeValue(_tagcontent[i], "depth");
                    _tagparentid = WeiXin.Core.Strings.AttributeValue(_tagcontent[i], "parentid");
                    _tagwherestr = WeiXin.Core.Strings.AttributeValue(_tagcontent[i], "wherestr");
                    _tagorderfield = WeiXin.Core.Strings.AttributeValue(_tagcontent[i], "orderfield");
                    if (_tagorderfield == "") _tagorderfield = "code";
                    _tagordertype = WeiXin.Core.Strings.AttributeValue(_tagcontent[i], "ordertype");
                    if (_tagordertype == "") _tagordertype = "asc";
                    if (_tagrepeatnum == "") _tagrepeatnum = "0";
                    _hascontent = WeiXin.Core.Strings.AttributeValue(_tagcontent[i], "hascontent");
                    if (_hascontent == "") _hascontent = "0";
                    if (_tagdepth == "") _tagdepth = "0";
                    string pStr = " [Id],[Title],[Info],[TopicNum],[Code],[ChannelId] FROM [normal_class] WHERE [IsOut]=0 AND [ChannelId]=" + _tagchannelid;
                    string oStr = " ORDER BY code asc";
                    if (_tagorderfield.ToLower() != "code")
                        oStr = " ORDER BY " + _tagorderfield + " " + _tagordertype + ",code asc";
                    else
                        oStr = " ORDER BY " + _tagorderfield + " " + _tagordertype;

                    if (_tagrepeatnum != "0")
                        pStr = " top " + _tagrepeatnum + pStr;
                    if (_tagparentid != "" && _tagparentid != "0")
                        pStr += " AND [ParentId]=" + _tagparentid;
                    if (_hascontent == "1")
                        pStr += " AND [TopicNum]>0";
                    if (_tagwherestr != "")
                        pStr += " AND " + _tagwherestr.Replace("小于", "<").Replace("大于", ">").Replace("不等于", "<>");
                    if (_tagselectids != "")
                        pStr += " AND [id] IN (" + _tagselectids.Replace("|", ",") + ")";

                    string sql = "select" + pStr + oStr;
                    DataTable _dt = ClassBll.GetClassBySql(sql);
                    StringBuilder sb = new StringBuilder();
                    for (int j = 0; j < _dt.Rows.Count; j++)
                    {
                        _viewstr = _tempstr[i];
                        _viewstr = _viewstr.Replace("{$ClassNO}", (j + 1).ToString());
                        executeTag_Class(ref _viewstr, _dt.Rows[j]["Id"].ToString());
                        sb.Append(_viewstr);
                    }
                    _pagestr = _pagestr.Replace(_loopbody, sb.ToString());
                    _dt.Clear();
                    _dt.Dispose();
                }
            }
        }



        /// <summary>
        /// 解析单条内容标签
        /// </summary>
        /// <param name="_pagestr"></param>
        /// <param name="_contentid"></param>
        /// <returns></returns>
        private void executeTag_Content(ref string _pagestr, module_article article)
        {
            _pagestr = _pagestr.Replace("{$_title}", article.Title);
            _pagestr = _pagestr.Replace("{$_id}", article.Id.ToString());
            _pagestr = _pagestr.Replace("{$_adddate}", article.AddDate.ToString("yyyy-MM-dd"));
            _pagestr = _pagestr.Replace("{$_viewnum}", article.ViewNum.ToString());
            _pagestr = _pagestr.Replace("{$_author}", article.Author);
            _pagestr = _pagestr.Replace("{$_summary}", article.Summary);
            _pagestr = _pagestr.Replace("{$Content}", article.Content);
            _pagestr = _pagestr.Replace("{$ChannelId}", article.ChannelId.ToString());
        }

        /// <summary>
        /// 解析单条产品标签
        /// </summary>
        /// <param name="_pagestr"></param>
        /// <param name="_contentid"></param>
        /// <returns></returns>
        private void executeTag_Product(ref string _pagestr, module_product pro)
        {
            _pagestr = _pagestr.Replace("{$_title}", pro.Title);
            _pagestr = _pagestr.Replace("{$_img}", pro.Img);
            _pagestr = _pagestr.Replace("{$_adddate}", DateTime.Parse(pro.AddDate.ToString()).ToString("yyyy-MM-dd"));
            _pagestr = _pagestr.Replace("{$_viewnum}", pro.ViewNum.ToString());
            _pagestr = _pagestr.Replace("{$_author}", pro.Author);
            _pagestr = _pagestr.Replace("{$_summary}", pro.Summary);
            _pagestr = _pagestr.Replace("{$Points}", decimal.Parse(pro.Points.ToString()).ToString("C"));
            _pagestr = _pagestr.Replace("{$Price0}", decimal.Parse(pro.Price0.ToString()).ToString("C"));

            _pagestr = _pagestr.Replace("{$Content}", pro.Content);
            _pagestr = _pagestr.Replace("{$ChannelId}", pro.ChannelId.ToString());
        }

        /// <summary>
        /// 替换内容循环标签
        /// </summary>
        /// <param name="_pagestr"></param>
        private void replaceTag_ContentLoop(ref string _pagestr)
        {
            string RegexString = "<zuxia:contentloop (?<tagcontent>.*?)>(?<tempstr>.*?)</zuxia:contentloop>";
            string[] _tagcontent = WeiXin.Core.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
            string[] _tempstr = WeiXin.Core.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
            if (_tagcontent.Length > 0)//标签存在
            {
                string _loopbody = string.Empty;
                string _replacestr = string.Empty;
                string _viewstr = string.Empty;
                for (int i = 0; i < _tagcontent.Length; i++)
                {
                    _loopbody = "<zuxia:contentloop " + _tagcontent[i] + ">" + _tempstr[i] + "</zuxia:contentloop>";
                    _replacestr = getContentList_RL(_tagcontent[i], _tempstr[i].Replace("<#foreach content>", "<#foreach collection=\"${contents}\" var=\"field\" index=\"i\">"));
                    _pagestr = _pagestr.Replace(_loopbody, _replacestr);
                }
            }
        }
        /// <summary>
        /// 提取列表供列表标签使用
        /// </summary>
        /// <param name="Parameter"></param>

        /// <returns></returns>
        private string getContentList_RL(string _tagcontent, string _tempstr)
        {

            string _viewstr = string.Empty;
            string _tagrepeatnum = WeiXin.Core.Strings.AttributeValue(_tagcontent, "repeatnum");
            if (_tagrepeatnum == "") _tagrepeatnum = "10";
            string _tagchannelid = WeiXin.Core.Strings.AttributeValue(_tagcontent, "channelid");
            if (_tagchannelid == "") _tagchannelid = "0";
            string _tagchanneltype = WeiXin.Core.Strings.AttributeValue(_tagcontent, "channeltype");
            if (_tagchanneltype == "") _tagchanneltype = "article";
            string _tagclassid = WeiXin.Core.Strings.AttributeValue(_tagcontent, "classid");
            if (_tagclassid == "") _tagclassid = "0";
            string _tagfields = WeiXin.Core.Strings.AttributeValue(_tagcontent, "fields");
            string _tagorderfield = WeiXin.Core.Strings.AttributeValue(_tagcontent, "orderfield");
            if (_tagorderfield == "") _tagorderfield = "adddate";
            string _tagordertype = WeiXin.Core.Strings.AttributeValue(_tagcontent, "ordertype");
            if (_tagordertype == "") _tagordertype = "desc";
            string _tagistop = WeiXin.Core.Strings.AttributeValue(_tagcontent, "istop");
            if (_tagistop == "") _tagistop = "0";
            string _tagisfocus = WeiXin.Core.Strings.AttributeValue(_tagcontent, "isfocus");
            if (_tagisfocus == "") _tagisfocus = "0";
            string _tagishead = WeiXin.Core.Strings.AttributeValue(_tagcontent, "ishead");
            if (_tagishead == "") _tagishead = "0";
            string _tagisimg = WeiXin.Core.Strings.AttributeValue(_tagcontent, "isimg");
            if (_tagisimg == "") _tagisimg = "0";
            string _tagtimerange = WeiXin.Core.Strings.AttributeValue(_tagcontent, "timerange");
            string _tagexceptids = WeiXin.Core.Strings.AttributeValue(_tagcontent, "exceptids");
            string _tagwherestr = WeiXin.Core.Strings.AttributeValue(_tagcontent, "wherestr");
            string _tagislike = WeiXin.Core.Strings.AttributeValue(_tagcontent, "islike");
            string _tagkeywords = WeiXin.Core.Strings.AttributeValue(_tagcontent, "keywords");
            string _ccType = string.Empty;
            string sql = "";
            if (_tagchannelid != "0")
            {

                sql = "SELECT [Id],[Type] FROM [normal_channel] WHERE [Id]=" + _tagchannelid + " AND [Enabled]=1";
                DataTable dtChannel = ChannelBll.GetChannelBySql(sql).Tables[0];
                if (dtChannel.Rows.Count > 0)
                {
                    _ccType = dtChannel.Rows[0]["Type"].ToString();
                }
            }
            else
            {
                _ccType = _tagchanneltype;
            }
            //  JumboTCMS.DAL.Normal_ChannelDAL dal = new JumboTCMS.DAL.Normal_ChannelDAL();
            //  dal.ExecuteTags(ref _tempstr, _tagchannelid);
            if (_tagclassid != "0")
                executeTag_Class(ref _tempstr, _tagclassid);

            sql = "SELECT TOP " + _tagrepeatnum + " [Id],[ChannelId],(select ishtml from [normal_channel] where id=[module_" + _ccType + "].channelid) as channelishtml,[ClassId],[FirstPage]," + _tagfields + " FROM [module_" + _ccType + "] WHERE ([IsPass]=1";
            if (_tagchannelid != "0")
            {
                string isChannel = " AND [ChannelId]=" + _tagchannelid;
                sql += isChannel;
                if (_tagclassid != "0")
                    sql += " And [ClassId] in (SELECT ID FROM [normal_class] WHERE [IsOut]=0 AND [Code] Like (SELECT Code FROM [normal_class] WHERE [IsOut]=0 AND [Id]=" + _tagclassid + isChannel + ")+'%')" + isChannel;
            }
            else
            {
                if (_tagclassid != "0")
                    sql += " And [ClassId] in (SELECT ID FROM [normal_class] WHERE [IsOut]=0 AND [Code] Like (SELECT Code FROM [normal_class] WHERE [IsOut]=0 AND [Id]=" + _tagclassid + ")+'%')";
                else
                    sql += " And [ChannelId] in (SELECT ID FROM [normal_channel] WHERE [Type]='" + _ccType + "' AND [Enabled]=1 and eid=" + Eid + ")";

            }

            if (_tagistop == "1")
                sql += " And [IsTop]=1";
            else if (_tagistop == "-1")
                sql += " And [IsTop]=0";
            if (_tagisfocus == "1")
                sql += " And [IsFocus]=1";
            else if (_tagisfocus == "-1")
                sql += " And [IsFocus]=0";
            if (_tagishead == "1")
                sql += " And [IsHead]=1";
            else if (_tagishead == "-1")
                sql += " And [IsHead]=0";

            //switch (_tagtimerange)
            //{
            //    case "1d":
            //        sql += " AND datediff('d',AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
            //        break;
            //    case "1w":
            //        sql += " AND datediff('ww',AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
            //        break;
            //    case "1m":
            //        sql += " AND datediff('m',AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
            //        break;
            //    case "1y":
            //        sql += " AND AddDate>=#" + (DateTime.Now.Year + "-1-1") + "#";
            //        break;
            //}

            switch (_tagtimerange)
            {
                case "1d":
                    sql += " AND datediff(d,AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                    break;
                case "1w":
                    sql += " AND datediff(ww,AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                    break;
                case "1m":
                    sql += " AND datediff(m,AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                    break;
                case "1y":
                    sql += " AND AddDate>='" + (DateTime.Now.Year + "-1-1") + "'";
                    break;
            }

            if (_tagisimg == "1")
                sql += " And [IsImg]=1 And (right(Img,4)='.jpg' Or right(Img,4)='.gif')";
            if (_tagwherestr != "")
                sql += " AND " + _tagwherestr.Replace("小于", "<").Replace("大于", ">").Replace("不等于", "<>");
            if (_tagexceptids != "")
                sql += " AND id not in(" + _tagexceptids + ")";
            if (_tagislike == "1")
            {
                if (_tagkeywords == "") _tagkeywords = "足下微信";
                _tagkeywords = _tagkeywords.Replace(",", " ").Replace(";", " ").Replace("；", " ").Replace("、", " ");
                string[] key = _tagkeywords.Split(new string[] { " " }, StringSplitOptions.None);
                string _joinstr = " AND (1<0";//亏我想得出来
                for (int i = 0; i < key.Length; i++)
                {
                    if (key[i].Length > 1)
                    {
                        _joinstr += " OR [Tags] LIKE '%" + key[i].Trim() + "%'";
                    }
                }
                _joinstr += ")";
                sql += _joinstr;
            }
            if (_tagorderfield.ToLower() != "rnd")
            {
                if (_tagorderfield.ToLower() != "adddate")
                    sql += ") ORDER BY " + _tagorderfield + " " + _tagordertype + ",adddate Desc,id Desc";
                else
                    sql += ") ORDER BY " + _tagorderfield + " " + _tagordertype + ",id Desc"; ;
            }
            else
            {
                sql += ")";// +ORDER_BY_RND();
            }
            DataSet content = ChannelBll.GetChannelBySql(sql);

            string ReplaceStr = operateContentTag(_ccType, content, _tempstr);
            //ReplaceStr = ReplaceStr.Replace("{$ContentCount}", dt.Rows.Count.ToString());
            //dt.Clear();
            //dt.Dispose();
            // return ReplaceStr;
            return ReplaceStr;
        }

        private string operateContentTag(string _channeltype, DataSet _ds, string _tempstr)
        {
            string _replacestr = _tempstr;
            _replacestr = _replacestr.Replace("$_{title}", "<#formattitle title=\"${field.title}\" />");
            _replacestr = _replacestr.Replace("$_{url}", "<#contentlink channelid=\"${field.channelid}\" contentid=\"${field.id}\" contenturl=\"${field.firstpage}\" />");

            _replacestr = _replacestr.Replace("$_{img}", "<#imgurl sitedir=\"\"  isimg=\"${field.isimg}\" img=\"${field.img}\" />");
            _replacestr = _replacestr.Replace("$_{classname}", "<#classname classid=\"${field.classid}\" />");
            _replacestr = _replacestr.Replace("$_{classlink}", "<#classlink channelid=\"${field.channelid}\" channelishtml=\"${field.channelishtml}\" classid=\"${field.classid}\" />");
            _replacestr = _replacestr.Replace("$_{channelname}", "<#channelname channelid=\"${field.channelid}\" />");
            _replacestr = _replacestr.Replace("$_{channellink}", "<#channellink channelid=\"${field.channelid}\" channelishtml=\"${field.channelishtml}\" />");
            _replacestr = _replacestr.Replace("$_{viewnum}", "<#viewnum sitedir=\"\\\" channeltype=\"" + _channeltype + "\" channelid=\"${field.channelid}\" contentid=\"${field.id}\" />");

            string _TemplateContent = _replacestr;
            WeiXin.Core.TemplateManager manager = WeiXin.Core.TemplateManager.FromString(_TemplateContent);
            string _content = "";
            manager.RegisterCustomTag("contentlink", new TemplateTag_GetContentLink());
            manager.RegisterCustomTag("formattitle", new TemplateTag_GetFormatTitle());
            manager.RegisterCustomTag("imgurl", new TemplateTag_GetImgurl());
            manager.RegisterCustomTag("classname", new TemplateTag_GetClassName());
            manager.RegisterCustomTag("classlink", new TemplateTag_GetClassLink());

            manager.RegisterCustomTag("cutstring", new TemplateTag_GetCutstring());
            manager.RegisterCustomTag("viewnum", new TemplateTag_GetViewnum());
            switch (_channeltype.ToLower())
            {
                case "product":
                    manager.SetValue("contents", ProBll.ProductDs2List(_ds));
                    break;
                default:
                    manager.SetValue("contents", ArticleBll.ArticleDs2List(_ds));
                    break;
            }
            _content = manager.Process();
            return _content;
        }


        #endregion

        #region JsonResult

        [HttpPost]
        public JsonResult AjaxLoadProductMore(int ccid, int classid, int Page, int id)
        {
            PagerList<module_product> list = ProBll.GetProductByPagerByAjax(id, ccid, classid, Page + 1, pageSize, "");

            //将获取到的关键字信息转为json字符串
            return Json(list);

        }
        [HttpPost]
        public JsonResult AjaxLoadProductMore2(int ccid, int classid, int Page)
        {
            PagerList<module_product> list = ProBll.GetProductByPagerByAjax(ccid, classid, Page + 1, pageSize, "");

            //将获取到的关键字信息转为json字符串
            return Json(list);

        }

        [HttpPost]
        public JsonResult AjaxLoadArticleMore(int ccid, int id, int Page)
        {
            PagerList<module_article> list = ArticleBll.GetArticleByPager(ccid, id, Page + 1, pageSize, "");

            //将获取到的关键字信息转为json字符串
            return Json(list);
        }


        /// <summary>
        /// Ajax 加载更多
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AjaxGetWall(string sessionid, int page)
        {
            WallInfoService WallBll = new WallInfoService();
            int eid = int.Parse(SecurityEncryption.DESDecrypt(sessionid));

            PagerList<WallInfo> list = WallBll.GetWallInfoByPager(eid, page + 1, pageSize);
            return Json(list);
        }
        #endregion

        #region 完善资料
        /// <summary>
        /// sessionid
        /// </summary>
        /// <param name="sessionid">企业ID</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public ActionResult Member(string sessionid, string userid)
        {
            int eid = int.Parse(SecurityEncryption.DESDecrypt(sessionid));

            ViewData["userid"] = userid;
            ViewData["sessionid"] = sessionid;
            ViewData["Top4Channel"] = ChannelBll.GetChannelByList(eid, 4);
            //查询会员的信息
            return View(FansBll.GetFansInfoByOpenID(userid));
        }

        [HttpPost]
        public ActionResult Member(FormCollection fc, FansContract fans, string sessionid, string userid)
        {
            int eid = int.Parse(SecurityEncryption.DESDecrypt(sessionid));
            var a = fc["br"];
            ViewData["userid"] = userid;
            ViewData["sessionid"] = sessionid;
            ViewData["Top4Channel"] = ChannelBll.GetChannelByList(eid, 4);

            string bak = Request.QueryString["bak"];
            string session = Request.QueryString["userid"];
            if (fans.UserBr == null)
            {
                try
                {
                    fans.UserBr = DateTime.Parse(fans.UserBr).ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    fans.UserBr = "1990-01-01";
                }

            }
            else
            {
                fans.UserBr = DateTime.Parse(fans.UserBr).ToString("yyyy-MM-dd");
            }
            fans.FromUserName = userid;
            //修改会员信息
            try
            {
                FansBll.UpdateFansName(fans);
                if (bak != null)
                {
                    ViewData["url"] = Url.Action(bak, new { sessionid = sessionid, userid = userid });
                }
                else
                {
                    ViewData["url"] = Url.Action("article", new { sessionid = sessionid, id = 243 });
                }
                ViewData["msg"] = "完善个人信息成功!";

                return View("Success");

            }
            catch (Exception)
            {
            }

            return View(fans);
        }
        #endregion

        #region 留言墙
        /// <summary>
        /// 留言墙
        /// </summary>
        /// <returns></returns>
        public ActionResult WllList(string sessionid, string UserID)
        {
            int eid = int.Parse(SecurityEncryption.DESDecrypt(sessionid));
            //查询企业的留言墙内容信息
            ViewData["UserID"] = UserID;
            ViewData["sessionid"] = sessionid;
            WallInfoService WallBll = new WallInfoService();
            ViewData["PageIndex"] = pageIndex;
            return View(WallBll.GetWallInfoByPager(eid, pageIndex, pageSize));
        }
        /// <summary>
        /// 添加留言
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public ActionResult SendWall(string sessionid, string UserID)
        {
            int eid = int.Parse(SecurityEncryption.DESDecrypt(sessionid));
            ViewData["userId"] = UserID;
            //1、判断用户信息是否完整，完整则可以留言，不完整则先完善个人信息
            FansContract fans = FansBll.GetFansInfoByOpenID(UserID);
            if (fans.TrueName == null || fans.UserBr == null || fans.UserPhone == null || fans.UserPhone == "")
            {
                ViewData["msg"] = "你还没有完善个人信息，请先完善个人信息!";
                ViewData["url"] = Url.Action("Member", new { sessionid = sessionid, UserID = UserID, bak = "SendWall" });
                return View("Error");
            }
            else
            {
                WallInfo wi = new WallInfo();
                wi.FansOpenId = fans.FromUserName;
                wi.EmpId = eid;
                wi.Temp1 = fans.TrueName;
                return View(wi);
            }
        }

        [HttpPost]
        public ActionResult SendWall(FormCollection fc, WallInfo wi, string sessionid, string UserID)
        {
            int eid = int.Parse(SecurityEncryption.DESDecrypt(sessionid));
            ViewData["userId"] = UserID;
            //1、判断用户信息是否完整，完整则可以留言，不完整则先完善个人信息
            FansContract fans = FansBll.GetFansInfoByOpenID(UserID);

            wi.FansOpenId = fans.FromUserName;
            wi.EmpId = eid;
            wi.Temp1 = fans.TrueName;
            wi.date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            WallInfoService WallBll = new WallInfoService();
            if (WallBll.Insert(wi) > 0)
            {

                ViewData["url"] = Url.Action("WllList", new { sessionid = sessionid, UserID = UserID });
                ViewData["msg"] = "留言成功!";
                return View("Success");
            }
            else
            {

                ViewData["url"] = Url.Action("WllList", new { sessionid = sessionid, UserID = UserID });
                ViewData["msg"] = "留言失败!";
                return View("Error");
            }
        }

        #endregion

        #region 会员卡
        public ActionResult MemberCard(string userid, string sessionid)
        {
            //1、判断用户是否完善个人信息
            //1、判断用户信息是否完整，完整则可以留言，不完整则先完善个人信息
            FansContract fans = FansBll.GetFansInfoByOpenID(userid);
            if (fans.TrueName == null || fans.UserBr == null || fans.UserPhone == null || fans.UserPhone == "")
            {
                ViewData["msg"] = "你还没有完善个人信息，请先完善个人信息!";
                ViewData["url"] = Url.Action("Member", new { sessionid = sessionid, UserID = userid, bak = "MemberCard" });
                return View("Error");
            }
            else
            {
                int eid = int.Parse(WeiXin.Core.SecurityEncryption.DESDecrypt(sessionid));
                //1、查询会员卡的信息 
                CardConfig ccc = CardConfigBll.GetCardConfigModelByEid(eid);
                //2、查询用户的信息，判断用户是否领取会员卡 
                FansToCard ftc = new FansToCardService().GetFansToCardByUserID(userid, eid);
                ViewData["ftc"] = ftc;
                ViewData["userid"] = userid;
                ViewData["sessionid"] = sessionid;
                return View(ccc);
            }
        }

        /// <summary>
        /// 领取会员卡
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="sessionid"></param>
        /// <returns></returns>
        [HttpPost]
        public string MemberCard(int id, string userid, string sessionid)
        {
            int eid = int.Parse(WeiXin.Core.SecurityEncryption.DESDecrypt(sessionid));

            //1、判断是否领取会员卡
            FansToCard ftc = new FansToCardService().GetFansToCardByUserID(userid, eid);
            if (ftc == null)
            {
                //2、查询该会员卡的信息

                //1、查询会员卡的信息 
                CardConfig ccc = CardConfigBll.GetCardConfigModelByEid(eid);
                int cardID = int.Parse(ccc.MinCardID.ToString());//领取的卡号
                //3、查询该会员卡现在最大的卡号
                FansToCard f = Fans2CardBll.GetMaxCardID(ccc.id);
                //4、查询粉丝的个人信息
                UserToEmp fans = FansBll.GetUserToEmpByOpenID(userid);
                if (f != null)
                {
                    //第N个人领取会员卡
                    cardID = f.Cardid + 1;
                }
                //2、领取会员卡
                ftc = new FansToCard
                {
                    FansID = fans.id,
                    CardConfigID = id,
                    Cardid = cardID,
                    CardState = ccc.CardState,
                    GetDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    ExpirationDate = "",
                    Integral = 0,
                    Money = 0,
                    CardPass = WeiXin.Core.SecurityEncryption.DESEncrypt("123456")

                };
                if (new FansToCardService().Insert(ftc) > 0)
                {
                    return "ok";
                }
                else
                {
                    return "error";
                }
            }
            else
            {
                return "ok";
            }
        }
        #endregion
    }
}
