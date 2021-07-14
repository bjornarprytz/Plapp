using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plapp.BusinessLogic
{
    public interface IRequestWrapper<T> : IRequest<Response<T>> 
    { }
    public interface IRequestWrapper : IRequest<Response<Unit>> 
    { }

    public interface IHandlerWrapper<TIn, TOut> : IRequestHandler<TIn, Response<TOut>>
        where TIn : IRequestWrapper<TOut>
    { }
    
    public interface IHandlerWrapper<TIn> : IRequestHandler<TIn, Response<Unit>>
        where TIn : IRequestWrapper<Unit>
    { }
}
