
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
/*-------------------------------------------------------

// Copyright (C) 2011 重庆足下科技有限公司 版权所有。
// 文件名：  PublicBusinessMethodCore.cs
// 功能描述：OA系统公共业务方法集合
// 创建标识：刘新奇  2012-05-06
// 修改标识：见每个方法前面
// 修改描述：见每个方法前面
//------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace WeiXin.Core
{
    /// <summary>
    /// OA系统公共业务方法类
    /// 提供OA系统中，所有公共业务方法
    /// </summary>
    public class PublicBusinessMethodCore
    {
        #region 权限系统 李伟

        public List<T> GetPageAuthority<T>(string filePath, string tag) where T : class
        {
            string xpath = string.Format("//tree[@action='{0}']/action", tag);
            XmlNodeList xmlNodeList = XMLHelper.GetXmlNodeListByXpath(filePath, xpath);
            List<T> list_PageAuthority = new List<T>();
            foreach (XmlNode node in xmlNodeList)
            {
                //获取T的类型
                Type type = typeof(T);
                //动态实例化数据实体对象
                T result = Activator.CreateInstance(type) as T;
                //通过反射获取数据实体属性
                foreach (PropertyInfo item in type.GetProperties())
                {
                    if ("AuthorityTag".Equals(item.Name))
                    {
                        item.SetValue(result, node.Attributes["tag"].Value, null);
                    }
                    if ("AuthorityID".Equals(item.Name))
                    {
                        item.SetValue(result, int.Parse(node.Attributes["id"].Value), null);
                    }
                }
                list_PageAuthority.Add(result);
            }
            return list_PageAuthority;
        }

        #endregion 权限系统 李伟

        #region 教职系统 刘新奇

        #region 2012-05-06 刘新奇 添加功能:生成学员学号

        /// <summary>
        /// 生成学员学号学号
        /// </summary>
        /// <returns></returns>
        public static string Create_StudentNo()
        {
            return "学号" + DateTime.Now.ToLongTimeString();
        }

        #endregion 2012-05-06 刘新奇 添加功能:生成学员学号

        #endregion 教职系统 刘新奇

        #region 学术系统

        #endregion 学术系统

        #region 人事系统

        #endregion 人事系统

        #region 就业系统

        #endregion 就业系统

        #region 财务系统

        #endregion 财务系统
    }
}