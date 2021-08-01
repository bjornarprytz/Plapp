using System;
using System.Collections.Generic;

namespace Plapp.Views.Infrastructure
{
    public interface IViewLayoutBuilder
    {
        Dictionary<Type, Type> BuildPages();
        Dictionary<Type, Type> BuildPopups();
    }
}