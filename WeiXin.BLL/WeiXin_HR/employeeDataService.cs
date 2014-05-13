﻿
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
using System.Text;
using WeiXin.DAL;
using WeiXin.Model;
namespace WeiXin.BLL
{
    /// <summary>
    /// 企业店铺信息
    /// </summary>
    public class employeeDataService
    {
        employeeDataDAL DataDao = new employeeDataDAL();
        /// <summary>
        /// 查询企业的店铺信息
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public List<employeeData> GetEmpData(int eid)
        {
            return DataDao.GetEmpData(eid);
        }
        /// <summary>
        /// 添加企业店铺信息
        /// </summary>
        /// <param name="ed"></param>
        public void Insert(employeeData ed)
        {
            DataDao.Insert(ed);
        }
        /// <summary>
        /// 删除企业店铺信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteEmpData(int id)
        {
            return DataDao.DeleteEmpData(id);
        }
    }
}
