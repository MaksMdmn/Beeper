using Ninject.Modules;
using WebCalculator.Domain.Repositories;
using WebCalculator.Domain.Repositories.Interfaces;

namespace WebCalculator.Web.App_Start
{
    public class NinjectRegistration : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserRepository>().To<UserRepository>();
            Bind<ICalculationRepository>().To<CalculationRepository>();
        }
    }
}