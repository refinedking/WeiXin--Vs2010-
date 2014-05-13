
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
using WeiXin.BO;

using WeiXin.Core.Transfer;
namespace WeiXin.BLL
{
    public class normal_EmpExtendsService
    {
        normal_EmpExtendsDAO EmpExDao = new normal_EmpExtendsDAO();

        #region Select
        /// <summary>
        /// 查询该企业是否已经使用该插件
        /// </summary>
        /// <param name="exid"></param>
        /// <param name="eid"></param>
        /// <returns></returns>
        public normal_EmpExtendsContract GetEmpExtendsByExtendsID(int exid, int eid)
        {
            return EmpExDao.GetEmpExtendsByExtendsID(exid, eid).ToBO<normal_EmpExtendsContract>();
        }

        /// <summary>
        /// 查询企业开通的插件
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public List<normal_EmpExtends> GetEmpExtends(int eid)
        {
            return EmpExDao.GetEmpExtends(eid);

        }
        /// <summary>
        /// 查询该企业是否已经使用该插件
        /// </summary>
        /// <param name="exid"></param>
        /// <param name="eid"></param>
        /// <returns></returns>
        public normal_EmpExtends GetEmpExtendsByExtendsID2(int exid, int eid)
        {
            return EmpExDao.GetEmpExtendsByExtendsID(exid, eid);
        }
        #endregion


        #region Insert
        public int Insert(normal_EmpExtendsContract neec)
        {
            return EmpExDao.Insert(neec.ToPO<normal_EmpExtends>());

        }
        #endregion

        #region Update
        public int Update(normal_EmpExtendsContract neec)
        {
            return EmpExDao.Update(neec.ToPO<normal_EmpExtends>());
        }
        #endregion
    }
}
