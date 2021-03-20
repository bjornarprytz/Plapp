using System.Threading;
using System.Threading.Tasks;

namespace Plapp
{
    public interface IConfigurationManager
    {
        Task<Configuration> GetAsync();
    }
}
