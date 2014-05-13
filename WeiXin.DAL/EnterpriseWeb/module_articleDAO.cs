
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
    //文章管理
    public class module_articleDAO
    {
        WeiXinSystemDBDataContext db = new WeiXinSystemDBDataContext(SqlHelper.connectionString);
        #region Insert
        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public int InsertArticle(module_article article)
        {
            db.module_article.InsertOnSubmit(article);
            db.SubmitChanges();
            return article.Id;
        }
        #endregion

        #region Update

        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public int UpdateArticle(module_article a)
        {
            string sql = string.Format(@"update module_article set ClassId={0},Title='{1}',TColor='{2}',Summary='{3}',Editor='{4}',Author='{5}',Tags='{6}',Img='{7}',IsImg={8},SourceFrom='{9}',Content='{10}' where Id={11}", a.ClassId, a.Title, a.TColor, a.Summary, a.Editor, a.Author, a.Tags, a.Img, a.IsImg, a.SourceFrom, a.Content, a.Id);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
        }

        /// <summary>
        /// 修改文章参数
        /// </summary>
        /// <param name="Name">需要修改的字段</param>
        /// <param name="val">字段的值</param>
        /// <param name="id">需要修改的文章</param>
        /// <returns></returns>
        public int UpdateArticle(string Name, int val, int id)
        {
            string sql = string.Format(@"update module_article set {0}={1} where id={2}", Name, val, id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
        }
        #endregion

        #region Select

        /// <summary>
        /// 查询企业的5条置顶新闻  必须是通过、置顶 有图 isimg=1 Ispass=1 IsHead=1
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public List<module_article> GetArticleTop5(int eid, int num)
        {
            List<module_article> list = (from a in db.module_article where a.normal_channel.eid == eid && a.IsPass == 1 && a.IsHead == 1 && a.IsImg == 1 select a).Take(num).OrderByDescending(s => s.Id).ToList();
            return list;
        }

        /// <summary>
        /// 查询首页焦点图，IsPass == 1 IsHead == 1  IsFocus == 1 IsImg == 1 
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public List<module_article> GetIndexFocusImg(int eid, int num)
        {
            List<module_article> list = (from a in db.module_article
                                         where a.normal_channel.eid == eid
                                         && a.IsPass == 1 && a.IsHead == 1
                                         && a.IsFocus == 1 && a.IsImg == 1 
                                         select a).Take(num).OrderByDescending(s => s.Id).ToList();
            return list;
        
        }
        /// <summary>
        /// 点击频道进来的时候，显示栏目下的num个焦点新闻
        /// </summary>
        /// <param name="eid">EID</param>
        /// <param name="num">数量</param>
        /// <param name="ccid">频道ID</param>
        /// <returns></returns>
        public List<module_article> GetArticleTop5ByClass(int eid, int num ,int ccid)
        {
            List<module_article> list = (from a in db.module_article
                                         where a.normal_channel.eid == eid
                                         && a.IsPass == 1 
                                         && a.ChannelId==ccid
                                         && a.IsFocus == 1 
                                         select a).Take(num).OrderByDescending(s => s.Id).ToList();
            return list;
        }
        /// <summary>
        /// 分页查询文章列表
        /// </summary>
        /// <param name="ccid"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public PagerList<module_article> GetArticleByPager(int ccid, int pageNo, int pageSize, string keyWords)
        {
            IQueryable<module_article> list = (from cr in db.module_article
                                               where cr.Title.Contains(keyWords.Trim())
                                               && cr.ChannelId == ccid
                                               select cr);
            int totalData = list.Count();
            return new PagerList<module_article>(list.OrderByDescending(o => o.Id), pageNo, pageSize, totalData);

        }
        /// <summary>
        /// 分页查询文章列表
        /// </summary>
        /// <param name="ccid">频道ID</param>
        /// <param name="classid">栏目ID</param>
        /// <param name="pageNo">页数</param>
        /// <param name="pageSize">页数量</param>
        /// <param name="keyWords">搜索关键词</param>
        /// <returns></returns>
        public PagerList<module_article> GetArticleByPager(int ccid,int classid, int pageNo, int pageSize, string keyWords)
        {
            db.DeferredLoadingEnabled = false;
            IQueryable<module_article> list = (from cr in db.module_article
                                               where cr.Title.Contains(keyWords.Trim())
                                               && cr.ChannelId == ccid
                                               && cr.ClassId == classid
                                               select cr);
            int totalData = list.Count();
            return new PagerList<module_article>(list.OrderByDescending(o => o.Id), pageNo, pageSize, totalData,"web");

        }

        /// <summary>
        /// 根据ccid查询文章
        /// </summary>
        /// <param name="ccid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public module_article GetArticleByIdAndccid(int ccid, int id)
        {
            return (from a in db.module_article where a.ChannelId == ccid && a.Id == id select a).FirstOrDefault();
        }

        /// <summary>
        /// 根据id查询文章
        /// </summary>
        /// <param name="ccid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public module_article GetArticleById(int id)
        {
            return (from a in db.module_article where a.Id == id select a).FirstOrDefault();
        }

        /// <summary>
        /// 根据sql语句查询文章
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetArticleBySql(string sql)
        {
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
            return dt;
        }

        /// <summary>
        /// 根据sql语句查询文章
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetArticleBySql2(string sql)
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);
            return ds;
        }



        #endregion

        #region Delete
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ccid"></param>
        /// <returns></returns>
        public int DeleteArticle(int id, int ccid)
        {
            string sql = string.Format(@"delete module_article where Id ={0} and ChannelId={1}", id, ccid);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
        }
        #endregion

    }
}
