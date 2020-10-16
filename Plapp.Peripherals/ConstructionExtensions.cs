using Dna;
using Microsoft.Extensions.DependencyInjection;

namespace Plapp.Peripherals
{
    public static class ConstructionExtensions
    {
        public static FrameworkConstruction AddCamera(this FrameworkConstruction construction)
        {
            construction.Services.AddSingleton<ICamera>(new Camera());

            return construction;
        }
    }
}
