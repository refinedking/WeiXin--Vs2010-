using System;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;

namespace WeiXin.Core.DBUtility
{
    /// <summary>
    /// OA SqlServer数据库访问帮助类
    /// </summary>
    public sealed class SqlHelper
    {
        //连接字符串
        public static readonly string connectionString = WeiXin.Core.SecurityEncryption.DESDecrypt( ConfigurationManager.ConnectionStrings["connectionString"].ToString());
         
        #region 私有构造函数和方法
        private SqlHelper() { }
        #endregion


        #region 添加功能:查询数据库
        /// <summary>
        /// 执行参数数据库查询
        /// </summary>
        /// <remarks>
        /// 示例: 
        ///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">参数集合</param>
        /// <returns></returns>
        public static DataSet ExecuteDataset(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            // 创建并打开数据库连接对象,操作完成释放对象.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // 调用指定数据库连接字符串重载方法.
                return ExecuteDataset(connection, commandType, commandText, commandParameters);
            }

        }
        /// <summary>
        /// 执行指定数据库连接字符串的命令,返回DataSet.
        /// </summary>
        /// <remarks>
        /// 示例:  
        ///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param>一个有效的数据库连接字符串</param>
        /// <param>命令类型 (存储过程,命令文本或其它)</param>
        /// <param>存储过程名称或T-SQL语句</param>
        /// <returns>返回一个包含结果集的DataSet</returns>
        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText)
        {
            // 创建并打开数据库连接对象,操作完成释放对象.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // 调用指定数据库连接字符串重载方法.
                return ExecuteDataset(connection, commandType, commandText, (SqlParameter[])null);
            }
        }
        /// <summary>
        /// 执行无参数数据库查询
        /// </summary>
        /// <remarks>
        /// 示例: 
        ///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns></returns>
        public static DataSet ExecuteDataset(CommandType commandType, string commandText)
        {
            return ExecuteDataset(commandType, commandText, null);

        }
        /// <summary>
        ///  对指定数据库执行参数查询
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">参数集合</param>
        /// <returns></returns>
        public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");
            SqlCommand cmd = new SqlCommand(commandText, connection);
            cmd.CommandType = commandType;
            if (commandParameters != null && commandParameters.Length > 0)
            {
                cmd.Parameters.AddRange(commandParameters);
            }
            // 创建SqlDataAdapter和DataSet.
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                // 填充DataSet.
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }
        #endregion

        #region 添加功能:非查询数据库
        /// <summary>
        /// 执行指定连接字符串,类型的SqlCommand.如果没有提供参数,不返回结果.
        /// </summary>
        /// <remarks>
        /// 示例:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param>一个有效的数据库连接字符串</param>
        /// <param>命令类型 (存储过程,命令文本, 其它.)</param>
        /// <param>存储过程名称或SQL语句</param>
        /// <param>SqlParameter参数数组</param>
        /// <returns>返回命令影响的行数</returns>
        public static int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return ExecuteNonQuery(connection, commandType, commandText, commandParameters);
            }
        }
        /// <summary>
        /// 执行指定数据库连接对象的命令
        /// </summary>
        /// <remarks>
        /// 示例:  
        ///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param>一个有效的数据库连接对象</param>
        /// <param>命令类型(存储过程,命令文本或其它.)</param>
        /// <param>T存储过程名称或T-SQL语句</param>
        /// <param>SqlParamter参数数组</param>
        /// <returns>返回影响的行数</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            // 创建SqlCommand命令,并进行预处理
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);
            // Finally, execute the command
            int retval = cmd.ExecuteNonQuery();
            // 清除参数,以便再次使用.
            cmd.Parameters.Clear();
            if (mustCloseConnection)
                connection.Close();
            return retval;
        }
        /// <summary>
        /// 执行参数数据库非查询
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">参数集合</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(CommandType commandType, string commandText, ref SqlParameter[] commandParameters)
        {
            // 创建并打开数据库连接对象,操作完成释放对象.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // 调用指定数据库连接字符串重载方法.
                return ExecuteNonQuery(connection, commandType, commandText, ref commandParameters);
            }
        }
        /// <summary>
        /// 执行无参数据库非查询
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            SqlParameter[] parameters = new SqlParameter[0];
            return ExecuteNonQuery(commandType, commandText, ref parameters);
        }
        /// <summary>
        /// 对指定数据库执行参数非查询
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">参数集合</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, ref SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");
            SqlCommand cmd = new SqlCommand(commandText, connection);
            cmd.Parameters.Clear();//清空命令对象原有参数
            //if (commandParameters.Length > 0)
            //{
            //    foreach (SqlParameter parameter in commandParameters)
            //    {
            //        if (parameter != null)
            //        {
            //            // 检查未分配值的输出参数,将其分配以DBNull.Value.
            //            if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) && (parameter.Value == null))
            //            {
            //                parameter.Value = DBNull.Value;
            //            }
            //            cmd.Parameters.Insert(0, parameter);
            //        }
            //    }
            //}
            cmd.Parameters.AddRange(commandParameters);
            cmd.CommandType = commandType;
            using (cmd)
            {
                connection.Open();
                int intCount = cmd.ExecuteNonQuery();
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                return intCount;
            }
        }

        #endregion

        #region 添加功能:参数处理

        /// <summary>
        /// 根据实体和存储过程名称自动生成参数集合项
        /// 注意：使用该方法必须遵循 存储过程名称格式必须为：@p_字段名，且实体属性名必须与表字段名一致。
        /// </summary>
        /// <remarks>
        /// 示例:  
        ///  第一步：获取参数集合 SqlParameter[] parameters = SqlHelper.GetSqlParametersByObject<Edu_RunOff>(insertObject, "Proc_Insert_Edu_RunOff");
        ///  第二步：执行SQL命令 SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Proc_Insert_Edu_RunOff", ref parameters);
        ///  如果是新增且存储过程有输出参数，则还要进行第三步：获得输出参数的值 insertObject.Id = int.Parse(parameters.Where(o => o != null && o.ParameterName == "@p_Id").FirstOrDefault().SqlValue.ToString());
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="p_ProcName"></param>
        /// <returns></returns>
        public static SqlParameter[] GetSqlParametersByObject<T>(T obj, string p_ProcName) where T : class
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (string.IsNullOrEmpty(p_ProcName)) throw new ArgumentNullException("p_ProcName");
            //从数据库存储过程中检索存储过程参数
            SqlCommand cmd = new SqlCommand(p_ProcName, new SqlConnection(SqlHelper.connectionString));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlCommandBuilder.DeriveParameters(cmd);
            cmd.Connection.Close();
            //拷贝参数
            int arrCount = cmd.Parameters.Count;
            SqlParameter[] arrayParameters = new SqlParameter[arrCount];
            int arrIndex = 0;
            foreach (SqlParameter parameter in cmd.Parameters)
            {
                foreach (var property in obj.GetType().GetProperties())
                {
                    if (parameter.ParameterName.Substring(3, parameter.ParameterName.Length - 3).ToLower() == property.Name.ToLower() && parameter.Direction != ParameterDirection.Output)
                    {
                        SqlParameter tempParameter = new SqlParameter(parameter.ParameterName, property.GetValue(obj, null) == null ? DBNull.Value : property.GetValue(obj, null));
                        arrayParameters[arrIndex] = tempParameter;
                        arrayParameters[arrIndex].Direction = parameter.Direction;
                        arrIndex++;
                        break;
                    }
                }
            }
            Array.Resize(ref arrayParameters, arrIndex);
            return arrayParameters;
        }

        /// <summary>
        /// 根据实体和存储过程名称自动生成参数集合项
        /// 注意：使用该方法必须遵循 存储过程名称格式必须为：@p_字段名，且实体属性名必须与表字段名一致。
        /// </summary>
        /// <remarks>
        /// 示例:  
        ///  第一步：获取参数集合 SqlParameter[] parameters = SqlHelper.GetSqlParametersByObject<Edu_RunOff>(insertObject, "Proc_Insert_Edu_RunOff");
        ///  第二步：执行SQL命令 SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Proc_Insert_Edu_RunOff", ref parameters);
        ///  如果是新增且存储过程有输出参数，则还要进行第三步：获得输出参数的值 insertObject.Id = int.Parse(parameters.Where(o => o != null && o.ParameterName == "@p_Id").FirstOrDefault().SqlValue.ToString());
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="p_ProcName"></param>
        /// <returns></returns>
        public static SqlParameter[] GetSqlParametersByObject<T>(T obj, string p_ProcName, int appendCount) where T : class
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (string.IsNullOrEmpty(p_ProcName)) throw new ArgumentNullException("p_ProcName");
            //从数据库存储过程中检索存储过程参数
            SqlCommand cmd = new SqlCommand(p_ProcName, new SqlConnection(SqlHelper.connectionString));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlCommandBuilder.DeriveParameters(cmd);
            cmd.Connection.Close();
            //拷贝参数
            int arrCount = cmd.Parameters.Count + appendCount;
            SqlParameter[] arrayParameters = new SqlParameter[arrCount];
            int arrIndex = 0;
            foreach (SqlParameter parameter in cmd.Parameters)
            {
                foreach (var property in obj.GetType().GetProperties())
                {
                    if (parameter.ParameterName.Substring(3, parameter.ParameterName.Length - 3).ToLower() == property.Name.ToLower() && parameter.Direction != ParameterDirection.Output)
                    {
                        SqlParameter tempParameter = new SqlParameter(parameter.ParameterName, property.GetValue(obj, null) == null ? DBNull.Value : property.GetValue(obj, null));
                        arrayParameters[arrIndex] = tempParameter;
                        arrayParameters[arrIndex].Direction = parameter.Direction;
                        arrIndex++;
                        break;
                    }
                }
            }
            Array.Resize(ref arrayParameters, arrIndex);
            return arrayParameters;
        }
        #endregion
        #region 预处理
        /// <summary>
        /// 预处理用户提供的命令,数据库连接/事务/命令类型/参数
        /// </summary>
        /// <param>要处理的SqlCommand</param>
        /// <param>数据库连接</param>
        /// <param>一个有效的事务或者是null值</param>
        /// <param>命令类型 (存储过程,命令文本, 其它.)</param>
        /// <param>存储过程名或都T-SQL命令文本</param>
        /// <param>和命令相关联的SqlParameter参数数组,如果没有参数为'null'</param>
        /// <param><c>true</c> 如果连接是打开的,则为true,其它情况下为false.</param>
        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");
            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }
            // 给命令分配一个数据库连接.
            command.Connection = connection;
            // 设置命令文本(存储过程名或SQL语句)
            command.CommandText = commandText;
            // 分配事务
            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }
            // 设置命令类型.
            command.CommandType = commandType;
            // 分配命令参数
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }
        /// <summary>
        /// 将SqlParameter参数数组(参数值)分配给SqlCommand命令.
        /// 这个方法将给任何一个参数分配DBNull.Value;
        /// 该操作将阻止默认值的使用.
        /// </summary>
        /// <param>命令名</param>
        /// <param>SqlParameters数组</param>
        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // 检查未分配值的输出参数,将其分配以DBNull.Value.
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }
        #endregion
    }
}