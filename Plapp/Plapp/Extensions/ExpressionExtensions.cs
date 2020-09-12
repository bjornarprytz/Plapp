using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Plapp
{
    public static class ExpressionHelpers
    {
        public static T GetPropertyValue<T>(this Expression<Func<T>> lambda)
        {
            return lambda.Compile().Invoke();
        }
        public static T GetPropertyValue<In, T>(this Expression<Func<In, T>> lambda, In input)
        {
            return lambda.Compile().Invoke(input);
        }
        public static void SetPropertyValue<T>(this Expression<Func<T>> lambda, T value)
        {
            // Converts a lambda () => some.Property, to some.Property
            var expression = (lambda as LambdaExpression).Body as MemberExpression;

            // Get the property information so we can set it
            var propertyInfo = (PropertyInfo)expression.Member;
            var target = Expression.Lambda(expression.Expression).Compile().DynamicInvoke();

            // Set the property value
            propertyInfo.SetValue(target, value);
        }

        public static void SetPropertyValue<In, T>(this Expression<Func<In, T>> lambda, T value, In input)
        {
            // Converts a lambda () => some.Property, to some.Property
            var expression = (lambda as LambdaExpression).Body as MemberExpression;

            // Get the property information so we can set it
            var propertyInfo = (PropertyInfo)expression.Member;

            // Set the property value
            propertyInfo.SetValue(input, value);
        }
    }
}
