using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace WeiXin.Core.DBUtility
{
    /// <summary>
    /// LinqToSql数据访问帮助类
    /// </summary>
    public sealed class LinqHelper
    {
        public static readonly string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ToString();

    }
}
