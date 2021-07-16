using Plapp.BusinessLogic.Commands;
using Plapp.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plapp.BusinessLogic
{
    public interface ICommandFactory<T>
        where T : DomainObject
    {
        // Create, Update, Delete actions

        SaveCommand<T> CreateSave(T entity);
        SaveAllCommand<T> CreateSaveAll(IEnumerable<T> entities);
        DeleteCommand<T> CreateDelete(int id);
        DeleteCommand<T> CreateDelete(T entity);
    }
}
