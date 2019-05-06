using WebCalculator.Domain.Models;

namespace WebCalculator.Domain.Repositories.Interfaces
{
    public interface IUserRepository:IBasicRepository<User, int>
    {
        User GetUserByIpAddress(string ipAddress);
    }
}
