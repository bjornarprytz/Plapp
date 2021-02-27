namespace Plapp.Core
{
    public interface ICreateViewModel<TViewModel> : ITaskViewModel
        where TViewModel : IViewModel
    {
        TViewModel Result { get; set; }
    }
}
