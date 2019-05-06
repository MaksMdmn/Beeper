using System;
using System.Linq;
using System.Web.Mvc;
using WebCalculator.Domain.DTOs;
using WebCalculator.Domain.Helpers;
using WebCalculator.Domain.Models;
using WebCalculator.Domain.Repositories.Interfaces;

namespace WebCalculator.Web.Controllers
{
    public class CalcController : Controller
    {
        IUserRepository _userRepo;
        ICalculationRepository _calcRepo;

        public CalcController(IUserRepository userRepo, ICalculationRepository calcRepo)
        {
            _userRepo = userRepo;
            _calcRepo = calcRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Calculation(string mathExpression)
        {
            string userIpAddress = Request.UserHostAddress; // ::1 is also ok, when it's running on local machine (it's equal to 127.0.0.1)

            //Create user or get it frod database if exists
            User user = _userRepo.GetUserByIpAddress(userIpAddress);
            if (user == null)
            {
                _userRepo.Create(new User { IpAddress = userIpAddress });
                user = _userRepo.GetUserByIpAddress(userIpAddress);
            }

            //Form calculation, put it into the database
            Calculation userCalculation = mathExpression.ToCalculaton();
            userCalculation.CreationDate = DateTime.Now;
            userCalculation.UserId = user.UserId;
            _calcRepo.Create(userCalculation);

            return Json(userCalculation.ToCalculationDto(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CalculationHistory()
        {
            string userIpAddress = Request.UserHostAddress;
            CalculationDto[] calculations = _calcRepo
                        .GetListByUserIpAddress(userIpAddress)
                        .Where(calc => calc.CreationDate.Date == DateTime.Now.Date)
                        .Select(calc => calc.ToCalculationDto())
                        .ToArray();

            return Json(calculations, JsonRequestBehavior.AllowGet);
        }
    }
}