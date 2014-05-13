
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
using System.Data;

namespace WeiXin.Core
{
    /// <summary>
    /// 分页类
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class PageHelper<T> : List<T>
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 每页记录条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// 数据源
        /// </summary>
        public List<T> DataSource { get; set; }
        /// <summary>
        /// 通过构造函数分页//调用代码 PageHelper（T） page = new PageHelper（T）(list, curPage, pageSize);
        /// </summary>
        /// <param name="obj">数据源</param>
        /// <param name="curPage">当前页码（自动不小于1，不大于总页数）</param>
        /// <param name="pageSize">每页显示数据量（0表示不分页；自动不小于1，不大于200,null则默认为14）</param>
        public PageHelper(List<T> list, int curPage, int pageSize, int totalCount)
        {
            PageSize = pageSize;
            TotalCount = totalCount;
            PageCount = (int)Math.Ceiling(TotalCount / (double)PageSize);
            CurrentPage = curPage > PageCount ? PageCount : curPage;
            AddRange(list);
        }
        /// <summary>
        /// 是否有上一页或首页
        /// </summary>
        public bool HasPrevious { get { return CurrentPage > 1; } }
        /// <summary>
        /// 是否有下一页或尾页
        /// </summary>
        public bool HasNext { get { return CurrentPage < PageCount; } }

        /// <summary>
        /// 
        /// </summary>
        public string PagerHtmlA
        {
            get
            {
                string strHtml = string.Empty;
                strHtml += @"<div class='left'>每页<span>" + PageSize + "</span>条记录　总共<span>" + TotalCount + "</span>条记录　页码:<span>" + CurrentPage + "/" + PageCount + "</span></div>";
                strHtml += "<div class='right'>";
                if (HasPrevious)
                {
                    strHtml += "<span curPage='1'>首 页</span>&nbsp;";
                    strHtml += "<span curPage='" + (CurrentPage - 1) + "'>上一页</span>&nbsp;";
                }
                else
                {
                    strHtml += "<span disabled='disabled'>首 页</span>&nbsp;";
                    strHtml += "<span disabled='disabled'>上一页</span>&nbsp;";
                }
                int intRange = (int)((CurrentPage - 1) / 10);
                for (int i = intRange > 0 ? intRange * 10 + 1 : 1; i < ((intRange + 1) * 10 > PageCount ? PageCount + 1 : (intRange + 1) * 10 + 1); i++)
                {
                    if (i == CurrentPage)
                    {
                        strHtml += "<span curPage='-1' style='color:red;'>" + (i < 10 ? "0" + i : i.ToString()) + "</span>&nbsp;";
                        continue;
                    }
                    strHtml += "<span curPage='" + i + "'>" + (i < 10 ? "0" + i : i.ToString()) + "</span>&nbsp;";
                }
                if (HasNext)
                {
                    strHtml += "<span curPage='" + (CurrentPage + 1) + "'>下一页</span>&nbsp;";
                    strHtml += "<span curPage='" + PageCount + "'>尾 页</span>&nbsp;";
                }
                else
                {
                    strHtml += "<span disabled='disabled'>下一页</span>&nbsp;";
                    strHtml += "<span disabled='disabled'>尾 页</span>&nbsp;";
                }
                strHtml += "<input type='text' class='txtCurPage' maxlength='6' name='CurPage' value='" + CurrentPage + "' />&nbsp;<span curPage='0'>GO</span></div>";
                return strHtml;
            }
        }
    }
}