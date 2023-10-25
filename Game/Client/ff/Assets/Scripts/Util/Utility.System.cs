using System;

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

    }
}
