using System;

namespace Plapp.DependencyInjection.Extensions
{
    public static class TypeExtensions
    {
        public static bool HasParameterlessConstructor(this Type type) => type.GetConstructor(Type.EmptyTypes) is not null;

        public static bool IsAssignableTo(this Type type, Type other) => other.IsAssignableFrom(type);
    }
}