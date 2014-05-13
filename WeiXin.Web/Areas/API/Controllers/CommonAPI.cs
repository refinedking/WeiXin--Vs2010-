using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.Xml.Serialization;
using System.Data;
using Newtonsoft.Json;
using System.IO;
using System.Web.Script.Serialization;
using WeiXin.BLL;
using WeiXin.Model;

namespace WeiXin.Web.Areas.API.Controllers
{
    public class CommonAPI
    {
        CityService CityBll = new CityService();

        #region 娱乐插件

        #region 天气
        /// <summary>
        /// 天气插件 返回string
        /// </summary>
        /// <param name="city">城市名称 如：重庆</param>
        /// <returns>查询结果</returns>
        public string GetWeather(string city)
        {

            try
            {
                City c = CityBll.GetCityByName(city);
                WebClient MyWebClient = new WebClient();
                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于对向Internet资源的请求进行身份验证的网络凭据。
                Byte[] pageData = MyWebClient.DownloadData("http://m.weather.com.cn/data/" + c.Id + ".html"); //从指定网站下载数据 
                //string pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句 
                var pageHtml = Encoding.UTF8.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句
                JavaScriptSerializer js = new JavaScriptSerializer();
                var obj = js.DeserializeObject(pageHtml);
                Dictionary<string, object> dic = obj as Dictionary<string, object>;

                Dictionary<string, object> obbbj = dic.Values.ToArray()[0] as Dictionary<string, object>;
                string str = "查询城市：";
                str += obbbj["city"].ToString() + "\n日期:";
                str += obbbj["date_y"].ToString() + "\n";
                str += obbbj["week"].ToString() + "\n今日气温:";//周信息 
                str += obbbj["temp1"].ToString() + "\n天气:";//今日气温  
                str += obbbj["wind1"].ToString() + "\n";//今日天气
                str += obbbj["index"].ToString() + "\n";//气温热
                str += obbbj["index_d"].ToString() + "\n紫外线:";//穿衣
                str += obbbj["index_uv"].ToString() + "\n洗车指数:";//紫外线信息 
                str += obbbj["index_xc"].ToString() + "\n旅游指数:";//洗车指数
                str += obbbj["index_tr"].ToString() + "\n舒适指数:"; //旅游指数
                str += obbbj["index_co"].ToString() + "\n晨练指数:"; //舒适指数
                str += obbbj["index_cl"].ToString() + "\n晾晒指数:"; //晨练指数
                str += obbbj["index_ls"].ToString() + "\n感冒指数:"; //晾晒指数
                str += obbbj["index_ag"].ToString() + "\n"; // 感冒指数 
                return str;
            }
            catch (Exception)
            {
                return "暂时没有查询到你说输入的城市，请输入如【重庆天气】进行查询";
            }
        }
        #endregion
        #region 笑话

        /// <summary>
        /// 获取笑话
        /// </summary>
        /// <returns></returns>
        public string GetXiaoHua()
        {
            try
            {
                WebClient MyWebClient = new WebClient();
                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于对向Internet资源的请求进行身份验证的网络凭据。
                Byte[] pageData = MyWebClient.DownloadData("http://open.binguo.me/api.php?type=joke"); //从指定网站下载数据 

                var pageHtml = Encoding.UTF8.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句
                pageHtml = pageHtml.Replace("&ldquo;", "“");
                pageHtml = pageHtml.Replace("&rdquo;", "”");
                return pageHtml;
            }
            catch (Exception)
            {
                return "暂时没有找到笑话，请输入如【笑话】进行查询";
            }

        }
        #endregion
        #region 翻译

        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="word">需要翻译的词</param>
        /// <returns></returns>
        public string GetFanYi(string word)
        {
            try
            {
                WebClient MyWebClient = new WebClient();
                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于对向Internet资源的请求进行身份验证的网络凭据。
                //百度帐号：y0ngwang,474196974
                Byte[] pageData = MyWebClient.DownloadData("http://openapi.baidu.com/public/2.0/bmt/translate?client_id=V6CGLfyaI2ycQBTf8OUf3u0v&q=" + word + "&from=auto&to=auto"); //从指定网站下载数据 

                var pageHtml = Encoding.UTF8.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句
                //{"from":"zh","to":"en","trans_result":[{"src":"\u4f60\u597d","dst":"Hello"}]}
                pageHtml = pageHtml.Replace("[", "");
                pageHtml = pageHtml.Replace("]", "");
                JavaScriptSerializer js = new JavaScriptSerializer();
                var obj = js.DeserializeObject(pageHtml);
                string str = "翻译:\n";
                Dictionary<string, object> dic = obj as Dictionary<string, object>;
                str += dic["from"].ToString() + "==>" + dic["to"].ToString() + "\n";
                Dictionary<string, object> obbbj = dic.Values.ToArray()[2] as Dictionary<string, object>;
                str += "原文:\n" + obbbj.Values.ToArray()[0].ToString();
                str += "\n译文:\n" + obbbj.Values.ToArray()[1].ToString();

                return str;
            }
            catch (Exception)
            {
                return "你说的话，暂时还不会翻译，翻译使用如【@你好】！";
            }
        }
        #endregion
        #region 股票
        /// <summary>
        /// 股票
        /// </summary>
        /// <param name="gpid"></param>
        /// <returns></returns>
        public string GetGuPiao(string gpid)
        {
            WebClient MyWebClient = new WebClient();
            MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于对向Internet资源的请求进行身份验证的网络凭据。
            Byte[] pageData = MyWebClient.DownloadData("http://quote.stock.hexun.com/stockdata/stock_quote.aspx?stocklist=" + gpid); //从指定网站下载数据  
            var pageHtml = Encoding.Default.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句
            var list = pageHtml.Split(';')[0];
            list = list.Replace("dataArr", "");
            list = list.Replace("=", "");
            list = list.Replace("[", "");
            list = list.Replace("]", "");
            list = list.Replace("'", "");
            var a = list.Split(',');
            string str = "";
            str += "查询股票：" + a[1];
            str += "\n股票代码：" + a[0];
            str += "\n当前价格：" + a[2];
            str += "\n下跌：" + a[3] + "%";
            str += "\n今日开盘价格：" + a[4];
            str += "\n最高：" + a[6];
            str += "\n最低：" + a[7];
            str += "\n成交：" + a[8];
            str += "\n换手：" + a[10];
            str += "\n市盈：" + a[5];
            str += "\n振幅：" + a[11];
            str += "\n数据来源互联网实时记录!";
            return str;
        }
        #endregion
        #region 快递
        /// <summary>
        /// 快递查询
        /// </summary>
        /// <param name="date"></param> 
        /// <returns></returns>
        public string GetKuaiDi(string postid)
        {
            string str = "";
            WebClient MyWebClient = new WebClient();
            MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于对向Internet资源的请求进行身份验证的网络凭据。
            Byte[] Comp = MyWebClient.DownloadData("http://www.kuaidi100.com/autonumber/auto?num=" + postid); //获取快递公司数据 
            var CompHtml = Encoding.UTF8.GetString(Comp); //如果获取网站页面采用的是UTF-8，则使用这句
            try
            {
                JavaScriptSerializer Compjs = new JavaScriptSerializer();
                var compObj = Compjs.DeserializeObject(CompHtml);
                Dictionary<string, object> Compdic = ((Object[])compObj).ToArray()[0] as Dictionary<string, object>;
                string comCode = Compdic["comCode"].ToString();//查询的公司 申通、圆通等

                Byte[] pageData = MyWebClient.DownloadData("http://www.kuaidi100.com/query?type=" + comCode + "&postid=" + postid + "&id=1&valicode=&temp=0.06350956972032484"); //从指定网站下载数据  
                var pageHtml = Encoding.UTF8.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句
                JavaScriptSerializer js = new JavaScriptSerializer();
                var obj = js.DeserializeObject(pageHtml);
                Dictionary<string, object> dic = obj as Dictionary<string, object>;
                var message = dic["message"].ToString();

                if (message == "ok")
                {
                    for (int i = ((object[])dic.Values.ToArray()[7]).Length - 1; i >= 0; i--)
                    {
                        Dictionary<string, object> temqp = ((object[])dic.Values.ToArray()[7]).ToArray()[i] as Dictionary<string, object>;
                        str += temqp["ftime"] + "\n";
                        str += temqp["context"] + "\n";
                    }
                }
                else
                {
                    str = "[流泪] Sorry,你要查询的快递暂时没有找到，请稍后再试~~";
                }

            }
            catch (Exception)
            {

                str = "[流泪] Sorry,你的快递单号输入有误。亲，仔细核对下单号哟~";
            }


            return str;
        }
        #endregion
        #region 汽车
        public string GetCar()
        {
            WebClient MyWebClient = new WebClient();
            MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于对向Internet资源的请求进行身份验证的网络凭据。
            
            Byte[] Comp = MyWebClient.DownloadData("http://auto880.gotoip1.com/search/ajax.aspx?oper=ajaxGetContentList&type=&mode=&ch=0&k=红&pagesize=10&page=1&ppId=11&xhId=11&jwId="); //获取快递公司数据 
            var CompHtml = Encoding.UTF8.GetString(Comp); 
            return "";
        }

        #endregion

        #endregion








    }
}