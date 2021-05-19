﻿using AutoMapper;
using Plapp.Core;
using Plapp.ViewModels;

namespace Plapp
{
    public static class PlappMapping
    {
        public static IMapper Configure()
        {
            return new Mapper(
                    new MapperConfiguration(cfg =>
                    {
                        cfg
                        .CreateTwoWayMapWithInterface<Topic, TopicViewModel, ITopicViewModel>()
                        .CreateTwoWayMapWithInterface<Tag, TagViewModel, ITagViewModel>()
                        .CreateTwoWayMapWithInterface<DataSeries, DataSeriesViewModel, IDataSeriesViewModel>()
                        .CreateTwoWayMapWithInterface<DataPoint, DataPointViewModel, IDataPointViewModel>();
                    }
                ));
        }

        private static IMapperConfigurationExpression CreateTwoWayMapWithInterface<TSrc, TDst, TDstInterface>( this IMapperConfigurationExpression mapperExpression)
            where TDst : TDstInterface
        
        {
            mapperExpression.CreateMap<TSrc, TDst>()
                .ConstructUsingServiceLocator()
                .IgnoreAllPropertiesWithAnInaccessibleSetter() // Ignore ViewModel specific properties (e.g. IsShowing)
                .ReverseMap(); // ViewModel -> DomainObject

            // Help resolve from concrete type to interface
            mapperExpression.CreateMap<TSrc, TDstInterface>().As<TDst>();

            return mapperExpression;
        }
    }
}