using System;
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
        /// 反射获得属性类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Type GetPropertyType(object obj, string propertyName)
        {
            Type type = obj.GetType();

            // 使用反射获取属性信息
            PropertyInfo propertyInfo = type.GetProperty(propertyName);

            if (propertyInfo != null)
            {
                // 返回属性的类型
                return propertyInfo.PropertyType;
            }
            // 属性不存在
            throw new ArgumentException($"Property {propertyName} not found in type {type}");
        }
        
        public static Type GetPropertyType<T>(string propertyName)
        {
            Type type = typeof(T);

            // 使用反射获取属性信息
            PropertyInfo propertyInfo = type.GetProperty(propertyName);

            if (propertyInfo != null)
            {
                // 返回属性的类型
                return propertyInfo.PropertyType;
            }
            else
            {
                // 属性不存在
                throw new ArgumentException($"Property {propertyName} not found in type {type}");
            }
        }
        
        /// <summary>
        /// 获得属性的类型和值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static (Type Type, object Value) GetPropertyTypeAndValue(object obj, string propertyName)
        {
            Type type = obj.GetType();

            // 使用反射获取属性信息
            PropertyInfo propertyInfo = type.GetProperty(propertyName);

            if (propertyInfo != null)
            {
                // 获取属性的类型和值
                Type propertyType = propertyInfo.PropertyType;
                object propertyValue = propertyInfo.GetValue(obj);

                return (propertyType, propertyValue);
            }
            
            // 属性不存在
            throw new ArgumentException($"Property {propertyName} not found in type {type}");
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
