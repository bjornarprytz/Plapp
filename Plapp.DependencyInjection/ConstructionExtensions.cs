using System;
using Dna;
using Plapp.BusinessLogic;

namespace Plapp.DependencyInjection
{
    public static class ConstructionExtensions
    {

        public static FrameworkConstruction AddModules(this FrameworkConstruction construction)
        {
            // TODO: Scan assembly for DependencyModules and add them

            construction.RegisterModule<BusinessLogicModule>()
                .RegisterModule<ViewModelsModule>()
                .RegisterModule<ValidationModule>()
                .RegisterModule<PersistModule>()
                .RegisterModule<DataMapperModule>();
            
            return construction;
        }
        
                public static FrameworkConstruction RegisterModule<T>(this FrameworkConstruction construction)
                    where T : IDependencyModule, new()
                {
                    var module = new T();
                    
                    return construction.RegisterModule<T>(module);
                }
                
                public static FrameworkConstruction RegisterModule<T>(this FrameworkConstruction construction, T module)
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