using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WeiXin.Core.Transfer
{
    /// <summary>
    /// 对象转换类
    /// </summary>
    public static class DataTransfer
    {
        /// <summary>
        /// PO转换为BO对象（查询）
        /// </summary>
        /// <typeparam name="T">业务实体对象</typeparam>
        /// <param name="entity">数据实体对象</param>
        /// <returns></returns>
        public static T ToBO<T>(this PersistenceObject entity) where T : class
        {
            Type type = typeof(T);
            T result = Activator.CreateInstance(type) as T;
            if (result == null)
            {
                throw new NullReferenceException("未指定\"" + type.FullName + "\"类型的实例");
            }
            if (entity == null)
            {
                throw new NullReferenceException("未指定\"" + entity.GetType().FullName + "\"类型的实例");
            }
            foreach (PropertyInfo item in type.GetProperties())
            {
                foreach (PropertyInfo property in entity.GetType().GetProperties())
                {
                    if (property.GetValue(entity, null) == null)
                    {
                        continue;
                    }

                    if (property.Name.ToLower() == item.Name.ToLower())
                    {
                        object value = property.GetValue(entity, null);
                        item.SetValue(result, value, null);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// PO转换为BO对象（查询）
        /// </summary>
        /// <typeparam name="T">业务实体对象</typeparam>
        /// <param name="entity">数据实体对象</param>
        /// <returns></returns>
        public static T ToBO<T>(this PersistenceObject entity, T oldEntity) where T : class
        {
            Type type = typeof(T);
            T result = oldEntity;
            if (result == null)
            {
                throw new NullReferenceException("未指定\"" + type.FullName + "\"类型的实例");
            }
            if (entity == null)
            {
                throw new NullReferenceException("未指定\"" + entity.GetType().FullName + "\"类型的实例");
            }
            foreach (PropertyInfo item in type.GetProperties())
            {
                foreach (PropertyInfo property in entity.GetType().GetProperties())
                {
                    if (property.GetValue(entity, null) == null)
                    {
                        continue;
                    }

                    if (property.Name.ToLower() == item.Name.ToLower())
                    {
                        object value = property.GetValue(entity, null);
                        item.SetValue(result, value, null);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// BO转换为PO(新增)
        /// </summary>
        /// <typeparam name="T">数据实体对象</typeparam>
        /// <param name="entity">业务实体对象</param>
        /// <returns></returns>
        public static T ToPO<T>(this BussinessObject entity) where T : class
        {
            if (entity == null)
            {
                throw new NullReferenceException();
            }
            //获得数据实体类型
            Type type = typeof(T);
            //动态实例化数据实体对象
            T result = Activator.CreateInstance(type) as T;

            //通过反射获取数据实体属性
            foreach (PropertyInfo item in type.GetProperties())
            {
                //通过反射获取业务实体属性
                foreach (PropertyInfo property in entity.GetType().GetProperties())
                {
                    if (property.Name.ToLower() == item.Name.ToLower())
                    {
                        item.SetValue(result, property.GetValue(entity, null), null);
                        continue;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// BO转换为PO(修改)
        /// </summary>
        /// <typeparam name="T">数据实体对象</typeparam>
        /// <param name="entity">业务实体对象</param>
        /// <param name="func">Lambda表达式</param>
        /// <returns></returns>
        public static T ToPO<T>(this BussinessObject entity, Func<T> func) where T : class
        {
            if (entity == null)
            {
                throw new NullReferenceException();
            }
            if (func == null) throw new ArgumentNullException("func");
            Type type = typeof(T);
            T result = func();
            foreach (PropertyInfo item in type.GetProperties())
            {
                foreach (PropertyInfo property in entity.GetType().GetProperties())
                {
                    if (property.Name == item.Name)
                    {
                        item.SetValue(result, property.GetValue(entity, null), null);
                    }
                }
            }
            return result;
        }
    }
}