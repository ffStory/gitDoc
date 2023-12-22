using System;
using System.Linq.Expressions;

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
        /// 使用表达式树获取对象的属性类型和值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static (Type type, object value) GetPropertyInfo<T>(T obj, string propertyName)
        {
            // 创建参数表达式
            ParameterExpression param = Expression.Parameter(typeof(T), "x");

            // 创建属性访问表达式
            MemberExpression memberExpression = Expression.Property(param, propertyName);

            var constructor = typeof(ValueTuple<Type, object>).GetConstructor(new[] {typeof(Type), typeof(object)});
            if (constructor == null)
            {
                return (null, null);
            }
            // 创建 Lambda 表达式
            Expression<Func<T, (Type type, object value)>> lambda =
                Expression.Lambda<Func<T, (Type type, object value)>>(
                    Expression.New(
                        constructor,
                        Expression.Constant(memberExpression.Type),
                        Expression.Convert(memberExpression, typeof(object))
                    ),
                    param
                );

            // 编译 Lambda 表达式并获取委托
            Func<T, (Type type, object value)> func = lambda.Compile();

            // 调用委托获取属性类型和值
            return func(obj);
        }
        
        /// <summary>
        /// 表达式树获得属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static object GetPropertyValue<T>(T obj, string propertyName)
        {
            // 创建参数表达式
            ParameterExpression param = Expression.Parameter(typeof(T), "x");

            // 创建属性访问表达式
            MemberExpression memberExpression = Expression.Property(param, propertyName);

            // 创建 Lambda 表达式
            Expression<Func<T, object>> lambda = Expression.Lambda<Func<T, object>>(
                Expression.Convert(memberExpression, typeof(object)),
                param
            );

            // 编译 Lambda 表达式并获取委托
            Func<T, object> func = lambda.Compile();

            // 调用委托获取属性值
            return func(obj);
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
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        static public void SetProperty<T, TValue>(T obj, string propertyName, TValue value)
        {
            // 创建参数表达式
            ParameterExpression param = Expression.Parameter(typeof(T), "x");

            // 创建属性访问表达式
            MemberExpression propertyExpression = Expression.Property(param, propertyName);

            // 创建值表达式
            ConstantExpression valueExpression = Expression.Constant(value, typeof(TValue));

            // 创建赋值表达式
            BinaryExpression assignmentExpression = Expression.Assign(propertyExpression, valueExpression);

            // 创建 Lambda 表达式
            Expression<Action<T>> lambda = Expression.Lambda<Action<T>>(assignmentExpression, param);

            // 编译 Lambda 表达式并获取委托
            Action<T> setPropertyAction = lambda.Compile();

            // 调用委托设置属性值
            setPropertyAction(obj);
        }
        
        /// <summary>
        /// 表达式树加法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="operand"></param>
        /// <typeparam name="T"></typeparam>
        static public void PropertyAddition<T>(T obj, string propertyName, long operand)
        {
            // 创建参数表达式
            ParameterExpression param = Expression.Parameter(typeof(T), "x");

            // 创建属性访问表达式
            MemberExpression propertyExpression = Expression.Property(param, propertyName);

            // 创建常量表达式
            ConstantExpression operandExpression = Expression.Constant(operand, typeof(long));

            // 创建加法操作表达式
            BinaryExpression additionExpression = Expression.Add(propertyExpression, operandExpression);

            // 创建 Lambda 表达式
            Expression<Action<T>> lambda = Expression.Lambda<Action<T>>(
                Expression.Assign(propertyExpression, additionExpression),
                param
            );

            // 编译 Lambda 表达式并获取委托
            Action<T> performAdditionAction = lambda.Compile();

            // 调用委托执行加法操作
            performAdditionAction(obj);
        }
    }
}
