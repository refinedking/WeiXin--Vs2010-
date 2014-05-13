
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
    public class normal_templateService
    {
        normal_templateDAO Tdao = new normal_templateDAO();
        #region Update
        public int Update(normal_templateContract t)
        {
            return Tdao.Update(t.ToPO<normal_template>());
        }

        /// <summary>
        /// 设置该模版为默认模版
        /// </summary>
        /// <param name="id"></param>
        /// <param name="eid"></param>
        /// <returns></returns>
        public int UpdateTemplateSetDef(int id, int eid)
        {
            return Tdao.UpdateTemplateSetDef(id, eid);
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
            return Tdao.UpdateTemplateSetDef(type, stype, eid);
        }
        #endregion
        #region Select
        /// <summary>
        /// 查询企业的模型：自己的+公用的
        /// </summary>
        /// <returns></returns>
        public PagerList<normal_template> GetTemplateByPage(int pageNo, int pageSize, int eid)
        {
            return Tdao.GetTemplateByPage(pageNo, pageSize, eid);

        }

        /// <summary>
        /// 根据ID查询模版
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public normal_templateContract GetTemplateById(int id, int eid)
        {
            return Tdao.GetTemplateById(id, eid).ToBO<normal_templateContract>();
        }
        /// <summary>
        /// 根据ID查询模版
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public normal_template GetTemplateById(int id)
        {
            return Tdao.GetTemplateById(id);
        }


        /// <summary>
        /// 根据企业ID,Type.Stype查询模版
        /// </summary>
        /// <param name="eid">企业ID</param>
        /// <param name="type">类型。文章article、产品product</param>
        /// <param name="stype">频道：channel 栏目class 内容；content</param>
        /// <returns></returns>
        public List<normal_template> getTemplateByList(int eid, string type, string stype)
        {
            return Tdao.getTemplateByList(eid, type, stype);
        }
        #endregion

        #region Insert


        public int Insert(normal_templateContract t)
        {
            return Tdao.Insert(t.ToPO<normal_template>());
        }
        #endregion

        #region Delete
        public void Delete(int eid, int id)
        {
            Tdao.Delete(eid, id);
        }
        #endregion
    }
}
