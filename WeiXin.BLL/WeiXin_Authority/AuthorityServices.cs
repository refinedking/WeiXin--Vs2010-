
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
using System.Data;
using WeiXin.Model;
using WeiXin.Core;

using WeiXin.Core.Transfer;
namespace WeiXin.BLL
{
    public class AuthorityServices
    {
        AuthorityRepository authorityRepository = new AuthorityRepository();

        /// <summary>
        /// 查询所有的功能点
        /// </summary>
        /// <returns></returns>
        public List<AuthorityContract> GetAllAuthority()
        {
            DataSet ds = authorityRepository.GetAuthority();
            IList<Authority> list = DataConvert.DataSetToIList<Authority>(ds, "Authority");
            return list.Select(a => a.ToBO<AuthorityContract>()).ToList();
        }

        /// <summary>
        /// 查询菜单拥有的功能点（根据菜单的编号）
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public List<AuthorityContract> GetAuthorityByModuleID(int moduleId)
        {
            DataSet ds = authorityRepository.GetAuthorityByModuleID(moduleId);
            return DataConvert.DataSetToIList<Authority>(ds, "Authority").Select(a => a.ToBO<AuthorityContract>()).ToList();
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <returns></returns>
        public PageHelper<AuthorityContract> GetPageData(int pageIndex, int pageSize, string authorityName = "")
        {
            object[] obj = authorityRepository.GetPageDataAuthority(pageIndex, pageSize, authorityName);
            List<AuthorityContract> list = DataConvert.DataSetToIList<AuthorityContract>((DataSet)obj[0], 0).ToList();
            return new PageHelper<AuthorityContract>(list, (int)obj[1], (int)obj[2], (int)obj[3]);
        }

        /// <summary>
        /// 添加功能点
        /// </summary>
        /// <param name="authorityContract"></param>
        /// <returns></returns>
        public bool AddAuthority(AuthorityContract authorityContract)
        {
            Authority authority = authorityContract.ToPO<Authority>();
            return authorityRepository.AddAuthority(authority: authority);
        }

        /// <summary>
        /// 修改功能点
        /// </summary>
        /// <param name="authorityContract"></param>
        /// <returns></returns>
        public bool UpdateAuthority(AuthorityContract authorityContract)
        {
            Authority authority = authorityContract.ToPO<Authority>();
            return authorityRepository.UpdateAuthority(authority: authority);
        }

        /// <summary>
        /// 更新功能点状态
        /// </summary>
        /// <param name="authorityIds">功能点ID</param>
        /// <returns></returns>
        public bool UpdateAuthorityStatus(string authorityIds)
        {
            return authorityRepository.UpdateAuthorityStatus(authorityIds);
        }

        /// <summary>
        /// 根据功能点ID查询功能点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AuthorityContract GetAuthortityEntityById(string id)
        {
            DataSet ds = authorityRepository.GetAuthority(id);
            return DataConvert.DataSetToEntity<Authority>(ds, 0).ToBO<AuthorityContract>();
        }

        /// <summary>
        /// 删除功能点
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteAuthority(string ids)
        {
            return authorityRepository.DeleteAuthority(ids);
        }

        /// <summary>
        /// 检查功能点名称或者标识是否重复
        /// </summary>
        /// <param name="authorityName"></param>
        /// <param name="authorityTag"></param>
        /// <returns></returns>
        public bool IsExists(string authorityTag, string id)
        {
            return authorityRepository.IsExists(authorityTag: authorityTag, id: id);
        }
    }
}