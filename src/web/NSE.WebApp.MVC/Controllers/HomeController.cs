using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSE.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("error/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var errorModel = new ErrorViewModel();

            if (id == 500)
            {
                errorModel.Message = "An error has ocured! Try again later or contact our support.";
                errorModel.Message = "An error has ocured!";
                errorModel.ErrorCode = id;
            }
            else if (id == 404)
            {
                errorModel.Message = "The page you are looking for doesn't exist! <br /> If you have any questions, contact our support team.";
                errorModel.Message = "Ops! Page not found!";
                errorModel.ErrorCode = id;
            }
            else if (id == 403)
            {
                errorModel.Message = "You don't have permission to do that";
                errorModel.Message = "Unauthorized access.";
                errorModel.ErrorCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", errorModel);
        }
    }
}
