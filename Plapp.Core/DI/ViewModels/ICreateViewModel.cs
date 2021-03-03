namespace Plapp.Core
{
    public interface ICreateViewModel<TViewModel> : ITaskViewModel, IRootViewModel
        where TViewModel : IViewModel
    {
        TViewModel Partial { get; set; }
        TViewModel GetResult();
    }
}
