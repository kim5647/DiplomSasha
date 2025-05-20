using Core.Core.ModelsSasha;
using HablonProject.ServicesSasha.BaseServise;
using SQLServer.DatabaseContext;
using SQLServer.Repository.RepositorySasha;

namespace HablonProject.ServicesSasha
{
    public class AuthViewServices : BaseServices
    {
        public bool AuthorizationUser(Users users) => _usersRepository.CheckUser(users) != null;
        public Users GetUsers(Users users) => _usersRepository.GetUserByLogin(users);
    }
}
