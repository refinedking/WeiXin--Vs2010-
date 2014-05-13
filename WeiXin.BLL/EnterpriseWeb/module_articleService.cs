
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

using WeiXin.BO;
using WeiXin.Model;
using WeiXin.Core.Transfer;
using WeiXin.Core;
using System.Data;
namespace WeiXin.BLL
{
    public class module_articleService
    {
        module_articleDAO ArticleDao = new module_articleDAO();
        #region Insert
        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public int InsertArticle(module_articleContract article)
        {
            return ArticleDao.InsertArticle(article.ToPO<module_article>());
        }
        #endregion
        #region Update
        
      
        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public int UpdateArticle(module_articleContract article)
        {
            return ArticleDao.UpdateArticle(article.ToPO<module_article>());
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
            return ArticleDao.UpdateArticle(Name, val, id);
        }
        #endregion
        #region Select
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
            return ArticleDao.GetArticleByPager(ccid, pageNo, pageSize, keyWords);
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
        public PagerList<module_article> GetArticleByPager(int ccid, int classid, int pageNo, int pageSize, string keyWords)
        {
            return ArticleDao.GetArticleByPager(ccid, classid, pageNo, pageSize, keyWords);


        }  
        /// <summary>
        /// 查询企业的5条置顶新闻 必须是通过、置顶 Ispass=1 IsHead=1
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public List<module_article> GetArticleTop5(int eid, int num)
        {
            return ArticleDao.GetArticleTop5(eid, num);
        }


        /// <summary>
        /// 查询首页焦点图，IsPass == 1 IsHead == 1  IsFocus == 1 IsImg == 1 
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public List<module_article> GetIndexFocusImg(int eid, int num)
        {
            return ArticleDao.GetIndexFocusImg(eid, num);

        }
        /// <summary>
        /// 点击频道进来的时候，显示该频道下的num个焦点新闻
        /// </summary>
        /// <param name="eid">EID</param>
        /// <param name="num">数量</param>
        /// <param name="ccid">频道ID</param>
        /// <returns></returns>
        public List<module_article> GetArticleTop5ByClass(int eid, int num, int ccid)
        {
            return ArticleDao.GetArticleTop5ByClass(eid, num, ccid);
        }

        /// <summary>
        /// 根据ccid查询文章
        /// </summary>
        /// <param name="ccid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public module_articleContract GetArticleByIdAndccid(int ccid, int id)
        {
            return ArticleDao.GetArticleByIdAndccid(ccid, id).ToBO<module_articleContract>();
        }

        public List<module_article> ArticleDs2List(DataSet ds)
        {
            return DataConvert.DataSetToIList<module_article>(ds,0).ToList();
        }

        /// <summary>
        /// 根据id查询文章
        /// </summary> 
        /// <param name="id"></param>
        /// <returns></returns>
        public module_article GetArticleById(int id)
        {
            return ArticleDao.GetArticleById(id);
        }


        /// <summary>
        /// 根据sql语句查询文章
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetArticleBySql(string sql)
        {
            return ArticleDao.GetArticleBySql(sql);
        }

        /// <summary>
        /// 根据sql语句查询文章
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetArticleBySql2(string sql)
        {
            return ArticleDao.GetArticleBySql2(sql);
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
            return ArticleDao.DeleteArticle(id, ccid);
        }
        #endregion
    }
}
