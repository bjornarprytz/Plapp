using System.Threading.Tasks;

namespace Plapp.Core
{
    public interface IPrompter
    {
        Task<TViewModel> CreateAsync<TViewModel>() where TViewModel : IViewModel;

        Task PopupAsync<TViewModel>() where TViewModel : ITaskViewModel;

        Task AlertAsync(string title, string alert, string confirm);

        Task<bool> DilemmaAsync(string title, string question, string yes, string no);

        Task<string> ChooseAsync(string title, string cancel, string destruction, params string[] options);

        Task<string> AnswerAsync(string title, string question);
        Task<string> AnswerNumericAsync(string title, string question);

        Task PopAsync();
    }
}
