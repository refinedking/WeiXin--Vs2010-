
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
    public class module_productService
    {
        module_productDAO ProDao = new module_productDAO();

        #region Select
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

            return ProDao.GetProductByPager(ccid, pageNo, pageSize, keyWords);

        }

        public List<module_product> GetProductTopNum(int eid, int num)
        {
            return ProDao.GetProductTopNum(eid, num);
        }
        public List<module_product> GetProductFocusImg(int eid, int num)
        {
            return ProDao.GetProductFocusImg(eid, num);
        }


        /// <summary>
        /// 根据ccid查询商品
        /// </summary>
        /// <param name="ccid">频道</param>
        /// <param name="id">商品ID</param>
        /// <returns></returns>
        public module_productContract GetProductByIdAndccid(int ccid, int id)
        {
            return ProDao.GetProductByIdAndccid(ccid, id).ToBO<module_productContract>();
        }

        /// <summary>
        /// 根据商品ID查询商品
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <returns></returns>
        public module_product GetProductById(int id)
        {
            return ProDao.GetProductById(id);
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
            return ProDao.GetProductByPager(id, ccid, classid, pageNo, pageSize, keyWords);

        }

        public List<module_product> ProductDs2List(DataSet ds)
        {
            return DataConvert.DataSetToIList<module_product>(ds, 0).ToList();
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
            return ProDao.GetProductByPagerByAjax(id, ccid, classid, pageNo, pageSize, keyWords);

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
        public PagerList<module_product> GetProductByPagerByAjax(int ccid, int classid, int pageNo, int pageSize, string keyWords)
        {
            return ProDao.GetProductByPagerByAjax(ccid, classid, pageNo, pageSize, keyWords);

        }


        /// <summary>
        /// 点击频道进来的时候，显示栏目下的num个焦点新闻
        /// </summary>
        /// <param name="eid">EID</param>
        /// <param name="num">数量</param>
        /// <param name="ccid">频道ID</param>
        /// <returns></returns>
        public List<module_product> GetProductTop5ByClass(int eid, int num, int ccid)
        {
            return ProDao.GetProductTop5ByClass(eid, num, ccid);

        }

        #endregion

        #region Insert
        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public int InsertProduct(module_productContract product)
        {
            return ProDao.InsertProduct(product.ToPO<module_product>());
        }
        #endregion

        #region Update

        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public int UpdateProduct(module_productContract product)
        {
            return ProDao.UpdateProduct(product.ToPO<module_product>());
        }


        /// <summary>
        /// 修改商品参数
        /// </summary>
        /// <param name="Name">需要修改的字段</param>
        /// <param name="val">字段的值</param>
        /// <param name="id">需要修改的文章</param>
        /// <returns></returns>
        public int UpdateProduct(string Name, int val, int id)
        {
            return ProDao.UpdateProduct(Name, val, id);
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
            return ProDao.DeleteProduct(id, ccid);
        }
        #endregion
    }
}
