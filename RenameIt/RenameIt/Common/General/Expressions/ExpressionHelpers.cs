using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Common.Expressions
{
    /// <summary>
    /// Helper for expressions
    /// </summary>
    public static class ExpressionHelpers
    {
        /// <summary>
        /// Compiles the expression and gets the return value
        /// </summary>
        /// <typeparam name="T">The return value</typeparam>
        /// <param name="lambda">The expression</param>
        /// <returns></returns>
        public static T GetPropertyValue<T> (this Expression<Func<T>> lambda)
        {
            return lambda.Compile().Invoke();
        }

        /// <summary>
        /// Set the underlying properties value to the given value, 
        /// from an expression that contains the property
        /// </summary>
        /// <typeparam name="T">The type of the value to set</typeparam>
        /// <param name="lambda">The expression</param>
        /// <param name="value">The value to set the property to</param>
        public static void SetPropertyValue<T>(this Expression<Func<T>> lambda, T value)
        {
            // converts a lambda (() => some.Property) to some.Property
            var expression = (lambda as LambdaExpression).Body as MemberExpression;

            // get the property information so we can set it
            var propertyInfo = (PropertyInfo)expression.Member;

            // get class property is attached to
            var target = Expression.Lambda(expression.Expression).Compile().DynamicInvoke();

            // set the value
            propertyInfo.SetValue(target, value);
        }
    }
}
