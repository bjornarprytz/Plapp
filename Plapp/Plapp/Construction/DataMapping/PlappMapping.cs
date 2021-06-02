using AutoMapper;
using Plapp.Core;
using Plapp.ViewModels;
using System;

namespace Plapp
{
    public static class PlappMapping
    {
        public static IMapper Configure(IServiceProvider serviceProvider)
        {
            return new Mapper(
                    new MapperConfiguration(cfg =>
                    {
                        cfg
                        .CreateTwoWayMapWithInterface<Topic, TopicViewModel, ITopicViewModel>(serviceProvider)
                        .CreateTwoWayMapWithInterface<Tag, TagViewModel, ITagViewModel>(serviceProvider)
                        .CreateTwoWayMapWithInterface<DataSeries, DataSeriesViewModel, IDataSeriesViewModel>(serviceProvider)
                        .CreateTwoWayMapWithInterface<DataPoint, DataPointViewModel, IDataPointViewModel>(serviceProvider);
                    }
                ));
        }

        private static IMapperConfigurationExpression CreateTwoWayMapWithInterface<TSrc, TDst, TDstInterface>(this IMapperConfigurationExpression mapperExpression, IServiceProvider serviceProvider)
            where TDst : TDstInterface
        
        {
            mapperExpression.CreateMap<TSrc, TDst>()
                .DisableCtorValidation()
                .PreserveReferences()
                .IgnoreAllPropertiesWithAnInaccessibleSetter(); // Ignore ViewModel specific properties (e.g. IsShowing)
                
            mapperExpression.CreateMap<TDst, TSrc>()
                .PreserveReferences(); // ViewModel -> DomainObject

            // Help resolve from concrete type to interface
            mapperExpression.CreateMap<TSrc, TDstInterface>().As<TDst>();

            return mapperExpression;
        }
    }
}
