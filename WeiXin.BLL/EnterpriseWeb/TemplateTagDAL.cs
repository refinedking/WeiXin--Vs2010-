
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
using System.Text;
using WeiXin.Core;
using WeiXin.Core.Parser.AST;
using System.Data;

namespace WeiXin.BLL
{
    /// <summary>
    /// 获得标题
    /// </summary>
    public class TemplateTag_GetFormatTitle : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = true;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp;
            string _title, _formattitle;
            exp = tag.AttributeValue("title");
            if (exp == null)
                throw new Exception("没有title标签");
            _title = manager.EvalExpression(exp).ToString();
            _formattitle = WeiXin.Core.Strings.HtmlEncode(_title);
            manager.WriteValue(_formattitle);
        }
    }

    /// <summary>
    /// 获得栏目名称
    /// </summary>
    public class TemplateTag_GetClassName : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = true;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp;
            string _classid, _classname;
            exp = tag.AttributeValue("classid");
            if (exp == null)
                throw new Exception("没有classid标签");
            _classid = manager.EvalExpression(exp).ToString();
            // _classname = (new JumboTCMS.DAL.Normal_ClassDAL().GetClassName(_classid));
            manager.WriteValue("");
        }
    }
    /// <summary>
    /// 获得栏目地址
    /// </summary>
    public class TemplateTag_GetClassLink : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = true;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp;
            string _channelid, _channelishtml, _classid, _classlink;
            exp = tag.AttributeValue("channelid");
            if (exp == null)
                throw new Exception("没有channelid标签");
            _channelid = manager.EvalExpression(exp).ToString();

            exp = tag.AttributeValue("channelishtml");
            if (exp == null)
                _channelishtml = "0";
            _channelishtml = manager.EvalExpression(exp).ToString();

            exp = tag.AttributeValue("classid");
            if (exp == null)
                throw new Exception("没有classid标签");
            _classid = manager.EvalExpression(exp).ToString();
            // _classlink = (new Normal_ClassDAL()).GetClassLink(1, _channelishtml == "1", _channelid, _classid, false);
            manager.WriteValue("");
        }
    }
    /// <summary>
    /// 获得内容地址
    /// </summary>
    public class TemplateTag_GetContentLink : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = true;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp;
            string _channelid, _contentid, _contenturl, _contentlink;
            exp = tag.AttributeValue("channelid");
            if (exp == null)
                throw new Exception("没有channelid标签");
            _channelid = manager.EvalExpression(exp).ToString();
            exp = tag.AttributeValue("contentid");
            if (exp == null)
                throw new Exception("没有contentid标签");
            _contentid = manager.EvalExpression(exp).ToString();
            exp = tag.AttributeValue("contenturl");
            if (exp == null)
                throw new Exception("没有contenturl标签");
            _contenturl = manager.EvalExpression(exp).ToString();
            //查询频道信息，获取频道模型，
            normal_channelService channelBll = new normal_channelService();
            DataSet ds = channelBll.GetChannelBySql("select  eid,Type from [normal_channel] where id=" + _channelid);
            string eid = ds.Tables[0].Rows[0]["eid"].ToString();
            string Type = ds.Tables[0].Rows[0]["Type"].ToString();
            _contentlink = "/Enterprise/Web/" + Type + "/" + _contentid + "?sessionid=" + WeiXin.Core.SecurityEncryption.DESEncrypt(eid);
            manager.WriteValue(_contentlink);
        }
    }
    /// <summary>
    /// 获得内容缩略图
    /// </summary>
    public class TemplateTag_GetImgurl : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = true;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp;
            string _sitedir, _isimg, _img, _imgurl;
            exp = tag.AttributeValue("sitedir");
            if (exp == null)
                throw new Exception("没有sitedir标签");
            _sitedir = manager.EvalExpression(exp).ToString();
            exp = tag.AttributeValue("isimg");
            if (exp == null)
                _isimg = "0";
            else
                _isimg = manager.EvalExpression(exp).ToString();
            exp = tag.AttributeValue("img");
            if (exp == null)
                _img = "";
            else
                _img = manager.EvalExpression(exp).ToString();
            if (_isimg == "0" && _img.Length == 0)
                _imgurl = _sitedir + "no.png";
            else
                _imgurl = _img;
            manager.WriteValue(_imgurl);
        }
    }
    /// <summary>
    /// 获得截断后的字符串
    /// </summary>
    public class TemplateTag_GetCutstring : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = true;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp;
            string _len, _cutstring;
            exp = tag.AttributeValue("len");
            if (exp == null)
                throw new Exception("没有len标签");
            _len = manager.EvalExpression(exp).ToString();
            //_cutstring = JumboTCMS.Utils.Strings.CutString(JumboTCMS.Utils.Strings.NoHTML(innerContent), Convert.ToInt32(_len));
            //  manager.WriteValue(_cutstring);
        }
    }
    /// <summary>
    /// 获得点击率
    /// </summary>
    public class TemplateTag_GetViewnum : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = true;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp;
            string _sitedir, _channelid, _channeltype, _contentid, _viewnum;
            exp = tag.AttributeValue("sitedir");
            if (exp == null)
                throw new Exception("没有sitedir标签");
            _sitedir = manager.EvalExpression(exp).ToString();
            exp = tag.AttributeValue("channelid");
            if (exp == null)
                throw new Exception("没有channelid标签");
            _channelid = manager.EvalExpression(exp).ToString();
            exp = tag.AttributeValue("channeltype");
            if (exp == null)
                throw new Exception("没有channeltype标签");
            _channeltype = manager.EvalExpression(exp).ToString();
            exp = tag.AttributeValue("contentid");
            if (exp == null)
                throw new Exception("没有contentid标签");
            _contentid = manager.EvalExpression(exp).ToString();
            _viewnum = "<script src=\"" + _sitedir + "plus/viewcount.aspx?ccid=" + _channelid + "&cType=" + _channeltype + "&id=" + _contentid + "&addit=0\"></script>";
            manager.WriteValue(_viewnum);
        }
    }
}