using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalculator.Models;

namespace WebCalculator.Repositories.Interfaces
{
    public interface IUserRepository:IBasicRepository<User, int>
    {
        User GetUserByIpAddress(string ipAddress);
    }
}
