using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plapp.Tests.DataMapping
{
    [TestClass]
    public class DataMappingTests
    {
        [TestMethod]
        public void DataMappingConfig_IsValid()
        {
            var serviceMock = new Mock<IServiceProvider>();

            var mappingConfig = PlappMapping.Configure(serviceMock.Object);

            mappingConfig.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
