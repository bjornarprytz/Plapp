namespace Plapp.Core
{
    public interface ICreateViewModel<TViewModel> : ITaskViewModel
        where TViewModel : IViewModel
    {
        TViewModel Partial { get; set; }
        TViewModel GetResult();
    }
}
