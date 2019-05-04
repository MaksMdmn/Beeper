using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCalculator.Repositories;
using WebCalculator.Repositories.Interfaces;

namespace WebCalculator.App_Start
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