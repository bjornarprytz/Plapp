using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plapp.ViewModels
{
    public interface IHydrate<in TDomainObject> where TDomainObject : DomainObject
    {
        void Hydrate(TDomainObject domainObject);
    }
}
