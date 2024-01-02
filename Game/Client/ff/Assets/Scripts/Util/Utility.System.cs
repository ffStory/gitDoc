using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Util
{
    public static partial class Utility
    {
        /// <summary>
        /// 是否是内置类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsBuiltInType(Type type)
        {
            return (type == typeof(object) || Type.GetTypeCode(type) != TypeCode.Object);
        }
        
        public static string FirstCharLower(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                throw new ArgumentException("String is mull or empty");
            }

            return s[0].ToString().ToLower() + s.Substring(1);
        }

        /// <summary>
        /// 反射获得属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        static public object GetPropertyValue(object obj, string propertyName)
        {
            Type type = obj.GetType();

            // 获取属性信息
            var property = type.GetProperty(propertyName);

            if (property != null)
            {
                // 获取属性值
                return property.GetValue(obj);
            }

            throw new ArgumentException($"Property '{propertyName}' not found on type '{type}'.");
        }
        
        /// <summary>
        /// 表达式树获得属性类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Type GetPropertyType<T>(T obj, string propertyName)
        {
            // 创建参数表达式
            ParameterExpression param = Expression.Parameter(typeof(T), "x");

            // 创建属性访问表达式
            MemberExpression memberExpression = Expression.Property(param, propertyName);

            // 创建 Lambda 表达式
            Expression<Func<T, Type>> lambda = Expression.Lambda<Func<T, Type>>(
                Expression.Constant(memberExpression.Type),
                param
            );

            // 编译 Lambda 表达式并获取委托
            Func<T, Type> func = lambda.Compile();

            // 调用委托获取属性类型
            return func(obj);
        }
        
        /// <summary>
        /// 根据属性名赋值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetProperty(object obj, string propertyName, object value)
        {
            // 获取对象的类型
            Type type = obj.GetType();

            // 获取属性信息
            PropertyInfo propertyInfo = type.GetProperty(propertyName);

            // 检查属性是否存在
            if (propertyInfo != null)
            {
                // 设置属性值
                propertyInfo.SetValue(obj, value);
            }
            else
            {
                Console.WriteLine($"Property '{propertyName}' not found.");
            }
        }
    }
}
