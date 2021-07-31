using Dna;
using FluentValidation;

namespace Plapp.Validation
{
    public static class ConstructionExtensions
    {
        public static FrameworkConstruction AddValidation(this FrameworkConstruction construction)
        {
            construction.Services.AddValidatorsFromAssemblyContaining<AssemblyInfo>();
            
            return construction;
        }
    }
}