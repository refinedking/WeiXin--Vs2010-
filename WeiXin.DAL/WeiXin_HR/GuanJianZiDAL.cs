
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
    /// effect:提供关键字数据访问方法
    /// </summary>
    public class GuanJianZiDAL
    {
        WeiXinSystemDBDataContext db = new WeiXinSystemDBDataContext(SqlHelper.connectionString);

        #region Api
        /// <summary>
        /// 匹配关键字
        /// </summary>
        /// <param name="content"></param>
        /// <param name="eid"></param>
        /// <returns></returns>
        public GuanJianZi GetGuanjianzi(string content, int eid)
        {
            return (from gjz in db.GuanJianZi where content.Contains(gjz.name)  && gjz.eId == eid select gjz).FirstOrDefault(); ;
        }
        #endregion
        /// <summary>
        /// 查询关键字列表
        /// </summary>
        /// <returns></returns>
        public List<GuanJianZi> GetGZJList()
        {
            return (from p in db.GuanJianZi select p).OrderByDescending(p => p.gjzId).ToList();
        }


        /// <summary>
        /// 关键字信息分页方法
        /// </summary> 
        /// <param name="pageNo">当前页数</param>
        /// <param name="pageSize">显示数据条数</param>
        /// <param name="keyWords">查询条件</param>
        /// <returns>当前页数的数据</returns>
        public PagerList<GuanJianZi> GetGuanJianZIByPager(int pageNo, int pageSize, string keyWords)
        {
            IQueryable<GuanJianZi> list = (from cr in db.GuanJianZi
                                           where (cr.name.Contains(keyWords.Trim())
                                         )
                                           select cr);
            int totalData = list.Count();
            return new PagerList<GuanJianZi>(list.OrderByDescending(o => o.gjzId), pageNo, pageSize, totalData);
        }
        /// <summary>
        /// 关键字信息分页方法2 查询企业自己的关键字
        /// </summary> 
        /// <param name="pageNo">当前页数</param>
        /// <param name="pageSize">显示数据条数</param>
        /// <param name="keyWords">查询条件</param>
        /// <returns>当前页数的数据</returns>
        public PagerList<GuanJianZi> GetGuanJianZIByPager(int eid,int pageNo, int pageSize, string keyWords)
        {
            IQueryable<GuanJianZi> list = (from cr in db.GuanJianZi
                                           where (cr.name.Contains(keyWords.Trim())
                                           && cr.eId==eid
                                         )
                                           select cr);
            int totalData = list.Count();
            return new PagerList<GuanJianZi>(list.OrderByDescending(o => o.gjzId), pageNo, pageSize, totalData);
        }

        /// <summary>
        /// 根据关键字编号查询该关键字所属企业
        /// </summary>
        /// <returns></returns>
        public employeeInfo GetEmployeeInfoByEid(int eId)
        {
            employeeInfo list = (from emp in db.employeeInfo where emp.Eid == eId select emp).FirstOrDefault();
            return list;
        }

        /// <summary>
        /// 根据关键字编号查询关键字信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetGuanJianZiByGJZId(int gjzId)
        {
            string sql = string.Format(@"select * from GuanJianZi where gjzId='" + gjzId + "'");
            return SqlServerHelper.Query(sql);
        }

        /// <summary>
        /// 添加关键字
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        public int AddGuanJianZi(GuanJianZi gjz)
        {
            string sql = string.Format(@"insert into GuanJianZi values('{0}',{1},{2},'{3}',{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}')", gjz.name, gjz.eId,gjz.typeId, gjz.time, gjz.isDisplay, gjz.remark, gjz.temp1, gjz.temp2, gjz.temp3, gjz.temp4, gjz.temp5, gjz.temp6, gjz.temp7, gjz.temp8, gjz.temp9, gjz.temp10);

            return SqlServerHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 修改关键字
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        public int UpdateGuanJianZi(GuanJianZi gjz)
        {
            string sql = string.Format(@" update GuanJianZi set name='{0}',eid={1},typeId={2},[time]='{3}',isDisplay={4},remark='{5}',temp1='{6}',temp2='{7}',temp3='{8}',temp4='{9}',temp5='{10}',temp6='{11}',temp7='{12}',temp8='{13}',temp9='{14}',temp10='{15}' where gjzId={16}", gjz.name, gjz.eId,gjz.typeId, gjz.time, gjz.isDisplay, gjz.remark, gjz.temp1, gjz.temp2, gjz.temp3, gjz.temp4, gjz.temp5, gjz.temp6, gjz.temp7, gjz.temp8, gjz.temp9, gjz.temp10, gjz.gjzId);
            return SqlServerHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 判断要添加的关键字是否存在
        /// </summary>
        /// <param name="name">关键字</param>
        /// <param name="eid">关键字所属企业id</param>
        /// <param name="gjzId">关键字id</param>
        /// <returns></returns>
        public bool IsExists(string name, string eid, string gjzId)
        {
            string sql = "";
            if (gjzId == null)  //说明是添加关键字
            {
                sql = string.Format(@"select name from GuanJianZi where name='" + name + "' and eid=" + eid + "");
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
                sql = string.Format(@"select count(*) from GuanJianZi where name='" + name + "' and eid=" + eid + " and gjzId=" + gjzId);
                string count = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0].Rows[0][0].ToString();
                if (count=="0")
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
        /// 删除关键字的方法
        /// </summary>
        /// <param name="gjzId">关键字编号</param>
        /// <returns></returns>
        public int DeleteGuanJianZi(int gjzId)
        {
            string sql = string.Format(@"delete from GuanJianZi where gjzId=" + gjzId + "");
            return SqlServerHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 获取所有的企业
        /// </summary>
        /// <returns></returns>
        public List<employeeInfo> GetAllEmp()
        {
            IQueryable<employeeInfo> list = (from cr in db.employeeInfo select cr);
            return list.ToList();
        }

        /// <summary>
        /// 获取所有的企业
        /// </summary>
        /// <returns></returns>
        public List<employeeInfo> GetAllEmp(int eid)
        {
            IQueryable<employeeInfo> list = (from cr in db.employeeInfo where cr.Eid==eid select cr);
            return list.ToList();
        }

        /// <summary>
        /// 根据企业编号查询该企业下的关键字(有效关键字)
        /// </summary>
        /// <param name="eId">企业编号</param>
        /// <returns></returns>
        public DataSet GetGuanJianZiByEId(int eId)
        {
            string sql = string.Format(@"select * from GuanJianZi where eId=" + eId + " and isDisplay=1 ");
            return SqlServerHelper.Query(sql);
        }

        /// <summary>
        /// 判断要删除的关键字下是否存在此关键字的回复信息
        /// </summary>
        /// <param name="gjzId">关键字ID</param>
        /// <returns></returns>
        public int IsHaveGJZHFByGJZId(int gjzId)
        {
            string sql = string.Format(@"select * from GuanJianZiHuiFu where gjzId=" + gjzId);
            return SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0].Rows.Count;
        }

        /// <summary>
        /// 获取所有的关键字类型
        /// </summary>
        /// <returns></returns>
        public List<GuanJianZiType> GetAllGJZType()
        {
            IQueryable<GuanJianZiType> list = (from cr in db.GuanJianZiType select cr);
            return list.ToList();
        }
    }
}
