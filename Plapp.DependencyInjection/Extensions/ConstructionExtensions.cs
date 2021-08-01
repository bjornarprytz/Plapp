using System;
using System.Linq;
using AutoMapper.Internal;
using Dna;
using Plapp.DependencyInjection.Extensions;

namespace Plapp.DependencyInjection
{
    public static class ConstructionExtensions
    {
        public static FrameworkConstruction AddModules(this FrameworkConstruction construction)
        {
            var assembly = typeof(AssemblyInfo).Assembly;

            foreach (var type in assembly.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IDependencyModule))
                                && t.HasParameterlessConstructor()))
            {
                construction.RegisterModule(type);
            }
            
            return construction;
        }

        private static FrameworkConstruction RegisterModule<T>(this FrameworkConstruction construction)
            where T : IDependencyModule, new()
        {
            var module = new T();
            
            return construction.RegisterModule<T>(module);
        }

        private static FrameworkConstruction RegisterModule(this FrameworkConstruction construction, Type moduleType)
        {
            if (moduleType == null)
            {
                throw new Exception("Trying to register module of null type");
            }

            var module = Activator.CreateInstance(moduleType) as IDependencyModule;

            return construction.RegisterModule(module);
        }
        
        private static FrameworkConstruction RegisterModule<T>(this FrameworkConstruction construction, T module)
            where T : IDependencyModule
        {
            if (module == null)
            {
                throw new Exception("Trying to register null module");
            }                      

            module.LoadConfiguration(construction.Configuration);
            module.ConfigureServices(construction.Services);
            
            return construction;
        }
    }
}