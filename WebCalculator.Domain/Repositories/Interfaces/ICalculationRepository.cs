using System.Collections.Generic;
using WebCalculator.Domain.Models;

namespace WebCalculator.Domain.Repositories.Interfaces
{
    public interface ICalculationRepository: IBasicRepository<Calculation, int>
    {
        IEnumerable<Calculation> GetListByUserIpAddress(string ipAddress);
    }
}
