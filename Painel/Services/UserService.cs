using Microsoft.EntityFrameworkCore;
using Painel.Entities;
using Painel.Interfaces.Services;
using System.Threading.Tasks;

namespace Painel.Services
{
    public class UserService : IUserService
    {
        private readonly PainelDBContext _painelDBContext;

        public UserService(PainelDBContext painelDBContext)
        {
            _painelDBContext = painelDBContext;
        }

        public async Task<User> AuthenticateAsync(string login, string password)
        {
            var user = await _painelDBContext.Users.AsQueryable().FirstOrDefaultAsync(x => x.Login == login && x.Password == password);

            return user;
        }
    }
}