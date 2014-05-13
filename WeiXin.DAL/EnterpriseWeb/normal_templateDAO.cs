
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
    /// <summary>
    /// 模版数据访问层
    /// </summary>
    public class normal_templateDAO
    {
        WeiXinSystemDBDataContext db = new WeiXinSystemDBDataContext(SqlHelper.connectionString);
        #region Insert


        public int Insert(normal_template t)
        {
            db.normal_template.InsertOnSubmit(t);
            db.SubmitChanges();
            return t.Id;
        }
        #endregion
        #region Select
        /// <summary>
        /// 查询企业的模型：自己的+公用的
        /// </summary>
        /// <returns></returns>
        public PagerList<normal_template> GetTemplateByPage(int pageNo, int pageSize, int eid)
        {
            IQueryable<normal_template> list = (from m in db.normal_template
                                                where (m.eid == null || m.eid == eid)
                                                select m
                   ); int totalData = list.Count();
            return new PagerList<normal_template>(list.OrderByDescending(o => o.Id), pageNo, pageSize, totalData);

        }

        /// <summary>
        /// 根据ID查询模版
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public normal_template GetTemplateById(int id, int eid)
        {
            return (from t in db.normal_template where t.Id == id && t.eid == eid select t).FirstOrDefault();
        }
        /// <summary>
        /// 根据ID查询模版
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public normal_template GetTemplateById(int id )
        {
            return (from t in db.normal_template where t.Id == id select t).FirstOrDefault();
        }

        /// <summary>
        /// 根据企业ID,Type.Stype查询模版
        /// </summary>
        /// <param name="eid">企业ID</param>
        /// <param name="type">类型。文章article、产品product</param>
        /// <param name="stype">频道：channel 栏目class 内容；content</param>
        /// <returns></returns>
        public List<normal_template> getTemplateByList(int eid,string type,string stype)
        { 
            List<normal_template> list=(from t in db.normal_template where
                                           ( t.eid==eid || t.eid==null)
                                            && t.Type == type && t.SType == stype
                                        select t).OrderByDescending(s => s.IsDefault).OrderByDescending(s=>s.Id).ToList();
            return list;
        }
        #endregion

        #region Update
        public int Update(normal_template t)
        {
            string sql = string.Format(@"update normal_template set title='{0}',content='{1}' where id={2}", t.Title, t.content, t.Id);
            return SqlHelper.ExecuteNonQuery(System.Data.CommandType.Text, sql);
        }

        /// <summary>
        /// 设置该模版为默认模版
        /// </summary>
        /// <param name="id"></param>
        /// <param name="eid"></param>
        /// <returns></returns>
        public int UpdateTemplateSetDef(int id, int eid)
        {
            string sql = string.Format(@"update normal_template set IsDefault=1 where Id={0} and eid={1}", id, eid);
            return SqlHelper.ExecuteNonQuery(System.Data.CommandType.Text, sql);
        }

        /// <summary>
        /// 设置该类型的模版为非默认模版
        /// </summary>
        /// <param name="type"></param>
        /// <param name="stype"></param>
        /// <param name="eid"></param>
        /// <returns></returns>
        public int UpdateTemplateSetDef(string type, string stype, int eid)
        {
            string sql = string.Format(@"update normal_template set IsDefault=0  where Type='{0}' and SType='{1}' and eid={2}", type, stype, eid);
            return SqlHelper.ExecuteNonQuery(System.Data.CommandType.Text, sql);
        }
        #endregion

        #region Delete
        public void Delete(int eid, int id)
        {
            string sql = string.Format(@"delete normal_template   where id={0}  and eid={1}", id, eid);
            SqlHelper.ExecuteNonQuery(System.Data.CommandType.Text, sql);

        }
        #endregion
    }
}
