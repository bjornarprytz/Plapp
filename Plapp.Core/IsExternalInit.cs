namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}

/*
 This class probably looks unnecessary, but the reason is that
 we're compiling ".NET 5 code against older .NET Framework version"
    - https://stackoverflow.com/questions/64749385/predefined-type-system-runtime-compilerservices-isexternalinit-is-not-defined

 Without this, we cannot use the Property { get; _init;_ } syntax for records.
 Hopefully this file can be deleted when we're on .NET 5
 */