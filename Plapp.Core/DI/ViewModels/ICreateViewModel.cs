namespace Plapp.Core
{
    public interface ICreateViewModel<TViewModel> : ITaskViewModel, IIOViewModel
        where TViewModel : IViewModel
    {
        TViewModel Partial { get; set; }
        TViewModel GetResult();
    }
}
