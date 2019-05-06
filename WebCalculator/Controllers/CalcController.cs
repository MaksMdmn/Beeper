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

        [HttpPost]
        public ActionResult Calculation(string mathExpression)
        {
            // ::1 is also ok, when it's running on local machine (it's equal to 127.0.0.1)
            string userIpAddress = Request.UserHostAddress; 

            //Create user or get it frod database if exists
            User user = _userRepo.GetUserByIpAddress(userIpAddress);
            if (user == null)
            {
                _userRepo.Create(new User { IpAddress = userIpAddress });
                user = _userRepo.GetUserByIpAddress(userIpAddress);
            }

            //Form calculation, put it into the database
            try
            {
                Calculation userCalculation = mathExpression.ToCalculaton();
                userCalculation.CreationDate = DateTime.Now;
                userCalculation.UserId = user.UserId;
                _calcRepo.Create(userCalculation);

                return Json(userCalculation.ToCalculationDto(), JsonRequestBehavior.AllowGet);
            }
            catch (ArgumentException)
            {
                return View("Cannot parse such expression. Please update the page and try again.");
            }
            catch (Exception)
            {
                return View("Connection problems...Please update the page and try again.");
            }
        }

        [HttpPost]
        public JsonResult CalculationHistory()
        {
            //Get all previous calculation from database (select by ipAddres and today's date)
            //Convert all data to DTOs and send back to front-side.
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