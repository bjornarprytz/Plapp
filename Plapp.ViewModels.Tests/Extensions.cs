using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plapp.ViewModels.Tests
{
    internal static class Extensions
    {
        internal static void SetupService<T>(this Mock<IServiceProvider> providerMock, Func<T> serviceFunc)
        {
            providerMock.Setup(sp => sp.GetService(It.Is<Type>(t => t == typeof(T)))).Returns(serviceFunc);
        }
    }
}
