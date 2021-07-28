using Plapp.Core;
using System.Collections.Generic;

namespace Plapp.BusinessLogic
{
    public class CommandFactory<T> : ICommandFactory<T>
        where T : DomainObject
    {
        public DeleteCommand<T> CreateDelete(int id)
        {
            return new DeleteCommand<T>(id);
        }

        public DeleteCommand<T> CreateDelete(T entity)
        {
            return new DeleteCommand<T>(entity);
        }

        public SaveCommand<T> CreateSave(T entity)
        {
            return new SaveCommand<T>(entity);
        }

        public SaveAllCommand<T> CreateSaveAll(IEnumerable<T> entities)
        {
            return new SaveAllCommand<T>(entities);
        }
    }
}
