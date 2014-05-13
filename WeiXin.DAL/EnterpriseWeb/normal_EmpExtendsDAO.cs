
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
using WeiXin.Model;
using WeiXin.Core.DBUtility;

namespace WeiXin.DAL
{
    public class normal_EmpExtendsDAO
    {
        WeiXinSystemDBDataContext db = new WeiXinSystemDBDataContext(SqlHelper.connectionString);

        #region Select
        /// <summary>
        /// 查询该企业是否已经使用该插件
        /// </summary>
        /// <param name="exid"></param>
        /// <param name="eid"></param>
        /// <returns></returns>
        public normal_EmpExtends GetEmpExtendsByExtendsID(int exid, int eid)
        {
            return (from e in db.normal_EmpExtends where e.extendID == exid && e.eid == eid select e).FirstOrDefault();
        }

        /// <summary>
        /// 查询企业开通并启用的插件
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public List<normal_EmpExtends> GetEmpExtends(int eid)
        {
            IQueryable<normal_EmpExtends> list = (from e in db.normal_EmpExtends where e.Enabled == 1 && e.eid == eid select e);
            return list.ToList();
        }
        #endregion

        #region Insert
        public int Insert(normal_EmpExtends neec)
        {
            db.normal_EmpExtends.InsertOnSubmit(neec);
            db.SubmitChanges();
            return neec.Id;

        }
        #endregion

        #region Update
        public int Update(normal_EmpExtends neec)
        {
            string sql = string.Format(@"update normal_EmpExtends set info='{0}', [Enabled]={1} where id={2}", neec.Info, neec.Enabled, neec.Id);
            return SqlHelper.ExecuteNonQuery(System.Data.CommandType.Text, sql);
        }
        #endregion
    }
}
