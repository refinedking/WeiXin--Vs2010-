
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
using System.Data;
using System.Data.SqlClient;
using WeiXin.Model;
using WeiXin.Core.DBUtility;
using WeiXin.DAL;

namespace WeiXin.DAL
{
    /// <summary>
    /// author:刘强
    /// time:2012-2-16
    /// effect:提供关键字回复数据访问方法
    /// </summary>
    public class GuanJianZiHuiFuDAL
    {
        WeiXinSystemDBDataContext db = new WeiXinSystemDBDataContext(SqlHelper.connectionString);

        #region Api
        /// <summary>
        /// 根据关键字回复，暂定 回复最新的一条，稍后更改
        /// </summary>
        /// <param name="gjz"></param>
        /// <param name="empid"></param>
        /// <returns></returns>
        public List<GuanJianZiHuiFu> HuiFu(string gjz, int empid)
        {
            //步骤1、查询该企业的所有关键字
            GuanJianZi  guanjianzi = (from g in
                                               db.GuanJianZi where
                                               g.eId == empid 
                                               && g.name==gjz
                                               && g.isDisplay != 0
                                           select g).FirstOrDefault();
            List<GuanJianZiHuiFu> list = null;
            if (guanjianzi != null)
            {
                int guanjianziID = guanjianzi.gjzId;
                int guanjianziType = guanjianzi.typeId; 
                switch (guanjianziType)
                { 
                    case 1://最新数据
                        //查询该关键字最新的一条回复内容
                        list = (from h in db.GuanJianZiHuiFu
                                where h.gjzId == guanjianziID &&
                                h.GuanJianZi.eId == empid && h.isDisplay != 0
                                select h).OrderByDescending(s => s.hfId).Take(1).ToList();

                        break;
                    case 2:
                        //查询该关键字最新的5条回复内容
                        list = (from h in db.GuanJianZiHuiFu
                                where h.gjzId == guanjianziID &&
                                h.GuanJianZi.eId == empid && h.isDisplay != 0
                                select h).OrderByDescending(s => s.hfId).Take(5).ToList();
                        break;
                    case 3:
                        //查询该关键字最新的一条回复内容
                        list = (from h in db.GuanJianZiHuiFu
                                where h.gjzId == guanjianziID &&
                                h.GuanJianZi.eId == empid
                                && h.isDisplay != 0
                                orderby db.NEWID()
                                select h).Take(1).ToList();
                        break;
                    case 4:
                        list = (from h in db.GuanJianZiHuiFu
                                where h.gjzId == guanjianziID &&
                                h.GuanJianZi.eId == empid
                                && h.isDisplay != 0
                                orderby db.NEWID()
                                select h).Take(5).ToList(); break;
                    case 5:
                        list = (from h in db.GuanJianZiHuiFu
                                where h.gjzId == guanjianziID &&
                                h.GuanJianZi.eId == empid
                                && h.isDisplay != 0
                                select h).Take(9).ToList();
                        break;
                    default:
                        list = (from h in db.GuanJianZiHuiFu
                                where h.gjzId == guanjianziID &&
                                h.GuanJianZi.eId == empid && h.isDisplay != 0
                                select h).OrderByDescending(s => s.hfId).Take(1).ToList();

                        break;
                }
            }
            return list;
        }

        /// <summary>
        /// 更新访问次数
        /// </summary>
        /// <param name="id"></param>
        public void ArticleRead(int id)
        {
            string sql = "update GuanJianZiHuiFu set hfCount=hfCount+1 where hfId="+id;
            SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
        }
        #endregion

        /// <summary>
        /// 查询关键字回复列表
        /// </summary>
        /// <returns></returns>
        public List<GuanJianZiHuiFu> GetGZJHFList()
        {
            return (from p in db.GuanJianZiHuiFu select p).OrderByDescending(p => p.hfId).ToList();
        }


        /// <summary>
        /// 关键字回复信息分页方法
        /// </summary> 
        /// <param name="pageNo">当前页数</param>
        /// <param name="pageSize">显示数据条数</param>
        /// <param name="keyWords">查询条件</param>
        /// <returns>当前页数的数据</returns>
        public PagerList<GuanJianZiHuiFu> GetGuanJianZIHuiFuByPager(int pageNo, int pageSize, string keyWords)
        {

            IQueryable<GuanJianZiHuiFu> list = (from cr in db.GuanJianZiHuiFu
                                                where (cr.content.Contains(keyWords.Trim()) || cr.GuanJianZiHuiFuType.name.Contains(keyWords.Trim()) || cr.GuanJianZi.name.Contains(keyWords.Trim()) || cr.GuanJianZi.employeeInfo.wxName.Contains(keyWords.Trim())
                                         )
                                                select cr);
            int totalData = list.Count();
            return new PagerList<GuanJianZiHuiFu>(list.OrderByDescending(o => o.hfId), pageNo, pageSize, totalData);
        }

        /// <summary>
        /// 关键字回复信息分页方法2 查询企业的关键字回复信息
        /// </summary> 
        /// <param name="pageNo">当前页数</param>
        /// <param name="pageSize">显示数据条数</param>
        /// <param name="keyWords">查询条件</param>
        /// <returns>当前页数的数据</returns>
        public PagerList<GuanJianZiHuiFu> GetGuanJianZIHuiFuByPager(int eid,int pageNo, int pageSize, string keyWords)
        {

            IQueryable<GuanJianZiHuiFu> list = (from cr in db.GuanJianZiHuiFu
                                                where
                                                cr.GuanJianZi.eId==eid&&
                                                (cr.content.Contains(keyWords.Trim()) || cr.GuanJianZiHuiFuType.name.Contains(keyWords.Trim()) || cr.GuanJianZi.name.Contains(keyWords.Trim()) || cr.GuanJianZi.employeeInfo.wxName.Contains(keyWords.Trim())
                                         )
                                                select cr);
            int totalData = list.Count();
            return new PagerList<GuanJianZiHuiFu>(list.OrderByDescending(o => o.hfId), pageNo, pageSize, totalData);
        }

        /// <summary>
        /// 获取所有的关键字回复类型
        /// </summary>
        /// <returns></returns>
        public List<GuanJianZiHuiFuType> GetAllGJZType()
        {
            IQueryable<GuanJianZiHuiFuType> list = (from cr in db.GuanJianZiHuiFuType select cr);
            return list.ToList();
        }

        /// <summary>
        /// 根据回复编号查询关键字回复信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetGuanJianZiHuiFuByHFId(int hfId)
        {
            string sql = string.Format(@"select * from GuanJianZiHuiFu where hfId='" + hfId + "'");
            return SqlServerHelper.Query(sql);
        }

        /// <summary>
        /// 判断要添加的关键字回复内容是否存在
        /// </summary>
        /// <param name="name">关键字内容</param>
        /// <param name="eid">关键字内容所属关键字</param>
        /// <param name="hfId">关键字内容Id</param>
        /// <returns></returns>
        public bool IsExists(string name, string gjzId, string hfId)
        {
            string sql = "";
            if (hfId == null)  //说明是添加关键字
            {
                sql = string.Format(@"select hfId from GuanJianZiHuiFu where content='" + name + "' and gjzId=" + gjzId + "");
                var count = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0].Rows.Count;
                if (SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0].Rows.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else  //修改关键字
            {
                sql = string.Format(@"select hfId from GuanJianZiHuiFu where content='" + name + "' and gjzId=" + gjzId + " and hfId=" + hfId);
                var count = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0].Rows.Count;
                if (SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0].Rows.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 添加关键字回复内容
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        public int AddGuanJianZiHuiFu(GuanJianZiHuiFu gjz)
        {
            db.GuanJianZiHuiFu.InsertOnSubmit(gjz);
            db.SubmitChanges();
            return gjz.hfId; 
        }

        /// <summary>
        /// 修改关键字回复内容
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        public int UpdateGuanJianZiHuiFu(GuanJianZiHuiFu gjz)
        {
            try
            {
                var rest = from g in db.GuanJianZiHuiFu where g.hfId == gjz.hfId select g;
                foreach (var item in rest)
                {
                    item.hfId = gjz.hfId;
                    item.typeId = gjz.typeId;
                    item.gjzId = gjz.gjzId;
                    item.content = gjz.content;
                    item.time = gjz.time;
                    item.isDisplay = gjz.isDisplay;
                    item.remark = gjz.remark;
                    item.title = gjz.title;
                    item.img = gjz.img;
                    item.temp1 = gjz.temp1;

                };
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }

        }

        /// <summary>
        /// 删除关键字回复信息
        /// </summary>
        /// <param name="hfId">关键字回复Id</param>
        /// <returns></returns>
        public int DeleteGuanJianZiHuiFu(int hfId)
        {
            string sql = string.Format(@"delete from GuanJianZiHuiFu where hfId=" + hfId + "");
            return SqlServerHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 根据回复类型编号查询回复类型信息
        /// </summary>
        /// <param name="typeId">回复类型编号</param>
        /// <returns></returns>
        public GuanJianZiHuiFuType GetAllGJZTypeByTypeId(string typeId)
        {
            GuanJianZiHuiFuType list = (from emp in db.GuanJianZiHuiFuType where emp.Id == int.Parse(typeId) select emp).FirstOrDefault();
            return list;
        }
    }
}
