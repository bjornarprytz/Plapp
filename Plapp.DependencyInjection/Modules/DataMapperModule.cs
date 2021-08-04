using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Plapp.Core;
using Plapp.DependencyInjection;
using Plapp.ViewModels;

namespace Plapp.DependencyInjection
{
    public class DataMapperModule : DependencyModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(Configure);
            
            IMapper Configure(IServiceProvider provider)
            {
                
                return new Mapper(
                    new MapperConfiguration(cfg =>
                        {
                            cfg
                                .CreateTwoWayMapWithInterface<Topic, TopicViewModel, ITopicViewModel>(provider)
                                .CreateTwoWayMapWithInterface<Tag, TagViewModel, ITagViewModel>(provider)
                                .CreateTwoWayMapWithInterface<DataSeries, DataSeriesViewModel, IDataSeriesViewModel>(provider)
                                .CreateTwoWayMapWithInterface<DataPoint, DataPointViewModel, IDataPointViewModel>(provider);
                        }
                    ));
            }
        }
    }
    internal static class MapperExtensions
    {
        internal static IMapperConfigurationExpression CreateTwoWayMapWithInterface<TSrc, TDst, TDstInterface>(this IMapperConfigurationExpression mapperExpression, IServiceProvider provider)
            where TDst : TDstInterface
        
        {
            mapperExpression.CreateMap<TSrc, TDst>()
                //.DisableCtorValidation()
                .ConstructUsing(c => (TDst) provider.GetService<TDstInterface>())
                .PreserveReferences()
                .IgnoreAllPropertiesWithAnInaccessibleSetter(); // Ignore ViewModel specific properties (e.g. IsShowing)
                
            mapperExpression.CreateMap<TDst, TSrc>()
                .PreserveReferences(); // ViewModel -> DomainObject

            // Help resolve from concrete type to interface
            mapperExpression.CreateMap<TSrc, TDstInterface>()
                .ConstructUsing(c => provider.GetService<TDstInterface>())
                .As<TDst>();

            return mapperExpression;
        }
    }
}
