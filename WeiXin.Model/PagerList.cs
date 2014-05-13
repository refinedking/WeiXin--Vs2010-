/*-------------------------------------------------------
// Copyright (C) 2011 重庆足下科技有限公司 版权所有。 
// 文件名：  PagerList.cs
// 功能描述：OA 系统自定义分页数据源
// 创建标识：刘新奇  2012-05-06
// 修改标识：见每个方法前面
// 修改描述：见每个方法前面
//------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXin.Model
{
    /// <summary>
    /// OA系统自定义分页数据源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagerList<T> : List<T>
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
        /// 通过构造函数分页//调用代码 PageHelper（T） page = new PageHelper（T）(list, curPage, pageSize);
        /// </summary>
        /// <param name="iquery">数据源</param>
        /// <param name="curPage">当前页码（自动不小于1，不大于总页数）</param>
        /// <param name="pageSize">每页显示数据量（0表示不分页；自动不小于1，不大于200,null则默认为14）</param>
        public PagerList(IQueryable<T> iquery, int? curPage, int? pageSize, int totalData)
        {
            CurrentPage = curPage.Value;
            PageSize = pageSize < 1 ? 15 : pageSize.Value;
            TotalCount = totalData;
            PageCount = (int)Math.Ceiling(TotalCount / (double)PageSize);
            CurrentPage = CurrentPage < 1 ? 1 : CurrentPage; //当前页码，自动不小于1
            CurrentPage = CurrentPage > PageCount ? PageCount : CurrentPage;//当前页码，自动不大于总页数
            AddRange(iquery.Skip(((CurrentPage < 1 ? 1 : CurrentPage) - 1) * PageSize).Take(PageSize).ToList());
        }

        /// <summary>
        /// 通过构造函数分页//调用代码 PageHelper（T） page = new PageHelper（T）(list, curPage, pageSize);
        /// </summary>
        /// <param name="iquery">数据源</param>
        /// <param name="curPage">当前页码（自动不小于1，不大于总页数）</param>
        /// <param name="pageSize">每页显示数据量（0表示不分页；自动不小于1，不大于200,null则默认为14）</param>
        public PagerList(IQueryable<T> iquery, int? curPage, int? pageSize, int totalData,string type)
        {
            CurrentPage = curPage.Value;
            PageSize = pageSize < 1 ? 15 : pageSize.Value;
            TotalCount = totalData;
            PageCount = (int)Math.Ceiling(TotalCount / (double)PageSize);
            CurrentPage = CurrentPage < 1 ? 1 : CurrentPage; //当前页码，自动不小于1
            //CurrentPage = CurrentPage > PageCount ? PageCount : CurrentPage;//当前页码，自动不大于总页数
            AddRange(iquery.Skip(((CurrentPage < 1 ? 1 : CurrentPage) - 1) * PageSize).Take(PageSize).ToList());
        }
        /// <summary>
        /// 通过构造函数分页//调用代码 PageHelper（T） page = new PageHelper（T）(list, curPage, pageSize);
        /// </summary>
        /// <param name="list">数据源</param>
        /// <param name="curPage">当前页码（自动不小于1，不大于总页数）</param>
        /// <param name="pageSize">每页显示数据量（0表示不分页；自动不小于1，不大于200,null则默认为14）</param>
        public PagerList(List<T> list, int? curPage, int? pageSize, int totalData)
        {
            CurrentPage = curPage.Value;
            PageSize = pageSize < 1 ? 15 : pageSize.Value;
            TotalCount = totalData;
            PageCount = (int)Math.Ceiling(TotalCount / (double)PageSize);
            CurrentPage = CurrentPage < 1 ? 1 : CurrentPage; //当前页码，自动不小于1
            CurrentPage = CurrentPage > PageCount ? PageCount : CurrentPage;//当前页码，自动不大于总页数
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
        /// 分页标签字符串
        /// </summary>
        public string PagerHtmlA
        {
            get
            {
                string strHtml = string.Empty;
                //分页详情
                strHtml += @"<div class='left'>每页<span>" + PageSize + "</span>条记录　总共<span>" + TotalCount + "</span>条记录　页码:<span>" + CurrentPage + "/" + PageCount + "</span></div>";
                strHtml += "<div class='right'>";
                //上一页和首页按钮
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
                //分页标签组数
                int intSize = 5;
                //当前分页阶段
                int intRange = (int)((CurrentPage - 1) / intSize);
                //生成分页标签组
                for (int i = intRange > 0 ? intRange * intSize + 1 : 1; i < ((intRange + 1) * intSize > PageCount ? PageCount + 1 : (intRange + 1) * intSize + 1); i++)
                {
                    if (i == CurrentPage)
                    {
                        strHtml += "<span curPage='-1' style='color:Red;'>" + (i < 10 ? "0" + i : i.ToString()) + "</span>&nbsp;";
                        continue;
                    }
                    strHtml += "<span curPage='" + i + "'>" + (i < 10 ? "0" + i : i.ToString()) + "</span>&nbsp;";
                }
                //下一页和尾页按钮
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
                //跳转标签
                strHtml += "<input type=\"text\" class=\"txtCurPage\" maxlength= \"6\" name=\"CurPage\" value=\"" + CurrentPage + "\" />&nbsp;<span curPage='0'>GO</span></div>";
                return strHtml;
            }
        }
    }
}
