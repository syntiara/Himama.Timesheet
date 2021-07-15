using System;
using System.Net;
using Himama.Timesheet.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Himama.Timesheet.Controllers
{
    public class ErrorController : Controller
    {
        protected ActionResult ShowNotFound(string message = "Sorry! request page not found")
        {
            return CustomError(HttpStatusCode.NotFound, message);
        }

        protected ActionResult ShowServerError()
        {
            return CustomError(HttpStatusCode.InternalServerError, "Oh no! It's the infamous server error. I am sorry...");
        }

        protected ActionResult CustomError(HttpStatusCode statusCode, string message)
        {
            var error = new ErrorViewModel
            {
                StatusCode = statusCode switch
                {
                    HttpStatusCode.NotFound => 400,
                    HttpStatusCode.Forbidden => 400,
                    _ => 500,
                },
                ErrorMessage = message,
            };
            return View($"{nameof(CustomError)}", error);
        }

    }
}
