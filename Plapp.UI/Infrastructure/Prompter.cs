using System.Threading.Tasks;
using Plapp.Core;
using Xamarin.Forms;

namespace Plapp.UI.Infrastructure
{
    public class Prompter : IPrompter
    {
        public Task AlertAsync(string title, string alert, string confirm)
        {
            return Shell.Current.DisplayAlert(title, alert, confirm);
        }

        public Task<bool> DilemmaAsync(string title, string question, string yes, string no)
        {
            return Shell.Current.DisplayAlert(title, question, yes, no);
        }

        public Task<string> ChooseAsync(string title, string cancel, string destruction, params string[] options)
        {
            return Shell.Current.DisplayActionSheet(title, cancel, destruction, options);
        }
    }
}