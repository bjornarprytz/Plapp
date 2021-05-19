using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var mappingConfig = PlappMapping.Configure();

            mappingConfig.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
