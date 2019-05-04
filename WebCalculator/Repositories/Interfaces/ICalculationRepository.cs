using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalculator.Models;

namespace WebCalculator.Repositories.Interfaces
{
    public interface ICalculationRepository: IBasicRepository<Calculation, int>
    {
        IEnumerable<Calculation> GetListByUserIpAddress(string ipAddress);
    }
}
