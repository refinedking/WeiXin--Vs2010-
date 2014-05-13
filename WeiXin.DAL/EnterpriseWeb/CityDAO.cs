
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
  public  class CityDAO
    {
      WeiXinSystemDBDataContext db = new WeiXinSystemDBDataContext(SqlHelper.connectionString);

      public City GetCityByName(string name)
      { 
        return (from c in db.City where c.name==name select c).FirstOrDefault();
      }
    }
}
