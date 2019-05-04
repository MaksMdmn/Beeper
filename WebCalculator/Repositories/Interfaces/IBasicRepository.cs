using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalculator.Repositories.Interfaces
{
    public interface IBasicRepository<TModel, TKey>
        where TModel : class
    {
        TModel Get(TKey id);
        void Create(TModel model);
        void Update(TModel model);
        void Delete(TKey id);
    }
}
