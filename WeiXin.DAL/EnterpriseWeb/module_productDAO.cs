
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
using System.Data;

namespace WeiXin.DAL
{
    /// <summary>
    /// 产品操作
    /// </summary>
    public class module_productDAO
    {
        WeiXinSystemDBDataContext db = new WeiXinSystemDBDataContext(SqlHelper.connectionString);

        #region Insert
        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="Product"></param>
        /// <returns></returns>
        public int InsertProduct(module_product Product)
        {
            db.module_product.InsertOnSubmit(Product);
            db.SubmitChanges();
            return Product.Id;
        }
        #endregion

        #region Update

        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="Product"></param>
        /// <returns></returns>
        public int UpdateProduct(module_product a)
        {

            string sql = string.Format(@"update module_product set ClassId={0},Title='{1}',TColor='{2}',Summary='{3}',Editor='{4}',Author='{5}',Tags='{6}',Img='{7}',IsImg={8},SourceFrom='{9}',Content='{10}',Price0={12},Points={13} where Id={11}", a.ClassId, a.Title, a.TColor, a.Summary, a.Editor, a.Author, a.Tags, a.Img, a.IsImg, a.SourceFrom, a.Content, a.Id, a.Price0, a.Points);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
        }

        /// <summary>
        /// 修改商品参数
        /// </summary>
        /// <param name="Name">需要修改的字段</param>
        /// <param name="val">字段的值</param>
        /// <param name="id">需要修改的商品</param>
        /// <returns></returns>
        public int UpdateProduct(string Name, int val, int id)
        {
            string sql = string.Format(@"update module_product set {0}={1} where id={2}", Name, val, id);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
        }
        #endregion

        #region Delete

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <param name="ccid">商品ID</param>
        /// <returns></returns>
        public int DeleteProduct(int id, int ccid)
        {
            string sql = string.Format(@"delete module_product where Id ={0} and ChannelId={1}", id, ccid);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
        }
        #endregion

        #region Select
        /// <summary>
        /// 点击频道进来的时候，显示栏目下的num个焦点新闻
        /// </summary>
        /// <param name="eid">EID</param>
        /// <param name="num">数量</param>
        /// <param name="ccid">频道ID</param>
        /// <returns></returns>
        public List<module_product> GetProductTop5ByClass(int eid, int num, int ccid)
        {
            List<module_product> list = (from a in db.module_product
                                         where a.normal_channel.eid == eid
                                         && a.IsPass == 1
                                         && a.ChannelId == ccid
                                         && a.IsFocus == 1

                                         select a).Take(num).OrderByDescending(s => s.Id).ToList();
            return list;
        }

        public List<module_product> GetProductTopNum(int eid, int num)
        {
            List<module_product> list = (from a in db.module_product
                                         where a.normal_channel.eid == eid
                                         && a.IsPass == 1
                                         && a.IsFocus == 1
                                         && a.IsHead == 1
                                         select a).Take(num).OrderByDescending(s => s.Id).ToList();
            return list;
        }

        public List<module_product> GetProductFocusImg(int eid, int num)
        {
            List<module_product> list = (from a in db.module_product
                                         where a.normal_channel.eid == eid
                                              && a.IsPass == 1 && a.IsHead == 1
                                         && a.IsFocus == 1 && a.IsImg == 1
                                         select a).Take(num).OrderByDescending(s => s.Id).ToList();
            return list;
        }
        /// <summary>
        /// 分页查询商品列表
        /// </summary>
        /// <param name="ccid">频道ID</param>
        /// <param name="pageNo">当前页码</param>
        /// <param name="pageSize">页数量</param>
        /// <param name="keyWords">关键词</param>
        /// <returns></returns>
        public PagerList<module_product> GetProductByPager(int ccid, int pageNo, int pageSize, string keyWords)
        {
            IQueryable<module_product> list = (from cr in db.module_product
                                               where cr.Title.Contains(keyWords.Trim())
                                               && cr.ChannelId == ccid
                                               select cr);
            int totalData = list.Count();
            return new PagerList<module_product>(list.OrderByDescending(o => o.Id), pageNo, pageSize, totalData);

        }

        /// <summary>
        /// 根据ccid查询商品
        /// </summary>
        /// <param name="ccid">频道</param>
        /// <param name="id">商品ID</param>
        /// <returns></returns>
        public module_product GetProductByIdAndccid(int ccid, int id)
        {
            return (from a in db.module_product where a.ChannelId == ccid && a.Id == id select a).FirstOrDefault();
        }

        /// <summary>
        /// 根据商品ID查询商品
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <returns></returns>
        public module_product GetProductById(int id)
        {
            module_product pro = (from a in db.module_product where a.Id == id select a).FirstOrDefault();

            return pro;
        }

        /// <summary>
        /// 查询栏目下的产品，除去现有的产品
        /// </summary>
        /// <param name="id">当前页面的产品ID</param>
        /// <param name="ccid">频道</param>
        /// <param name="classid">栏目</param>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public PagerList<module_product> GetProductByPager(int id, int ccid, int classid, int pageNo, int pageSize, string keyWords)
        {

            IQueryable<module_product> list = (from cr in db.module_product
                                               where cr.Title.Contains(keyWords.Trim())
                                               && cr.ChannelId == ccid
                                               && cr.ClassId == classid
                                               && cr.Id != id
                                               select cr);
            int totalData = list.Count();
            return new PagerList<module_product>(list.OrderByDescending(o => o.Id), pageNo, pageSize, totalData, "web");

        }
        /// <summary>
        /// 查询栏目下的产品，除去现有的产品
        /// </summary>
        /// <param name="id">当前页面的产品ID</param>
        /// <param name="ccid">频道</param>
        /// <param name="classid">栏目</param>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public PagerList<module_product> GetProductByPagerByAjax(int id, int ccid, int classid, int pageNo, int pageSize, string keyWords)
        {
            db.DeferredLoadingEnabled = false;

            IQueryable<module_product> list = (from cr in db.module_product
                                               where cr.Title.Contains(keyWords.Trim())
                                               && cr.ChannelId == ccid
                                               && cr.ClassId == classid
                                               && cr.Id != id
                                               select cr);
            int totalData = list.Count();
            return new PagerList<module_product>(list.OrderByDescending(o => o.Id), pageNo, pageSize, totalData, "web");

        }

        /// <summary>
        /// 查询栏目下的产品
        /// </summary>

        /// <param name="ccid">频道</param>
        /// <param name="classid">栏目</param>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public PagerList<module_product> GetProductByPagerByAjax(int ccid, int classid, int pageNo, int pageSize, string keyWords)
        {
            db.DeferredLoadingEnabled = false;

            IQueryable<module_product> list = (from cr in db.module_product
                                               where cr.Title.Contains(keyWords.Trim())
                                               && cr.ChannelId == ccid && cr.ClassId == classid
                                               select cr);
            int totalData = list.Count();
            return new PagerList<module_product>(list.OrderByDescending(o => o.Id), pageNo, pageSize, totalData, "web");

        }
        #endregion
    }
}
