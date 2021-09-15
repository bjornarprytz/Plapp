using System;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Plapp.ViewModels.Infrastructure
{
    public class PlappCommand : AsyncCommand
    {
        public PlappCommand(
            Func<Task> execute,
            Func<object?, bool>? canExecute = null, 
            Action<Exception>? onException = null
            )
            : base(
            execute, 
            canExecute, 
            onException, 
            continueOnCapturedContext: false, 
            allowsMultipleExecutions: false)
        {
            
        }
        
        public PlappCommand(
            Func<Task> execute,
            Func<bool> canExecute, 
            Action<Exception>? onException = null
        )
            : base(
                execute, 
                canExecute, 
                onException, 
                continueOnCapturedContext: false, 
                allowsMultipleExecutions: false)
        {
            
        }
    }
}