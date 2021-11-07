using Painel.Entities;
using System.Threading.Tasks;

namespace Painel.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string login, string password);
    }
}