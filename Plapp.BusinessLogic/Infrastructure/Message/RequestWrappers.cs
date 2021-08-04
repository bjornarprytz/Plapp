using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plapp.BusinessLogic
{
    public interface IRequestWrapper<out TOut> : IRequest<IResponseWrapper<TOut>> 
    { }
    public interface IRequestWrapper : IRequest<IResponseWrapper> 
    { }

    public interface IHandlerWrapper<in TIn, TOut> : IRequestHandler<TIn, IResponseWrapper<TOut>>
        where TIn : IRequestWrapper<TOut>
    { }
    
    public interface IHandlerWrapper<in TIn> : IRequestHandler<TIn, IResponseWrapper>
        where TIn : IRequestWrapper
    { }
}
