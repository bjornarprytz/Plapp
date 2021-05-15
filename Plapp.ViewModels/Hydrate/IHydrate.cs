using Plapp.Core;

namespace Plapp.ViewModels
{
    public interface IHydrate<in TDomainObject> where TDomainObject : DomainObject
    {
        void Hydrate(TDomainObject domainObject);
    }
}
