
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
using System.Data;
using WeiXin.Core;

namespace WeiXin.BLL
{
    /// <summary>
    /// 企业微网 栏目
    /// </summary>
    public class normal_ClassService
    {
        normal_ClassDAO ClassDao = new normal_ClassDAO();
        #region Select

        /// <summary>
        /// 查询企业 频道下的栏目
        /// </summary>
        /// <param name="ccid">频道ID</param>
        /// <param name="eid">企业ID</param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public PagerList<normal_class> GetClassByPager(int ccid, int eid, int pageNo, int pageSize, string keyWords)
        {
            return ClassDao.GetClassByPager(ccid, eid, pageNo, pageSize, keyWords);

        }

        /// <summary>
        /// 根据频道ID和企业的Eid查询该频道下有那些栏目
        /// </summary>
        /// <param name="ccid">频道ID</param>
        /// <param name="eid">企业Eid</param>
        /// <returns></returns>
        public List<normal_class> GetClassByccidAndEid(int ccid, int eid)
        {
            return ClassDao.GetClassByccidAndEid(ccid, eid);

        }


        /// <summary>
        /// 根据频道ID和企业的Eid查询该频道下有那些栏目
        /// </summary>
        /// <param name="ccid">频道ID</param>
        /// <param name="eid">企业Eid</param>
        /// <param name="eid">提取数据条数</param>
        /// <returns></returns>
        public List<normal_class> GetClassByccidAndEid(int ccid, int eid, int count)
        {
            return ClassDao.GetClassByccidAndEid(ccid, eid, count);

        }
        /// <summary>
        /// 根据Eid和栏目ID查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="eid"></param>
        /// <returns></returns>
        public normal_classContract GetClassByIdAndEid(int id, int eid)
        {
            return ClassDao.GetClassByIdAndEid(id, eid).ToBO<normal_classContract>();

        }

        /// <summary>
        /// 根据sql语句查询文章
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetClassBySql(string sql)
        {
            return ClassDao.GetClassBySql(sql);
        }

        public normal_class GetClassModelBySql(string sql)
        {
            return DataConvert.DataSetToEntity<normal_class>(ClassDao.GetClassBySql2(sql), 0);
        }

        public normal_class GetClassModelByidAndCCid(int id, int ccid)
        {
            return ClassDao.GetClassModelByidAndCCid(id, ccid);
        }

        #endregion


        #region Insert
        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int InsertClass(normal_classContract c)
        {
            return ClassDao.InsertClass(c.ToPO<normal_class>());
        }
        #endregion
        #region Update


        /// <summary>
        /// 修改栏目
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int UpdateClass(normal_classContract c)
        {
            return ClassDao.UpdateClass(c.ToPO<normal_class>());
        }
        #endregion


        #region Delete
        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>
        public int DeleteClass(int id, int eid)
        {
            return ClassDao.DeleteClass(id, eid);
        }
        #endregion
    }
}
