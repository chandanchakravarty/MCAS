using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCAS.Controllers
{
    public class ErrorController : Controller
    {
        //public ViewResult Index()
        //{
        //    return View("Error");
        //}
        //public ViewResult NotFound()
        //{
        //    Response.StatusCode = 404;  //you may want to set this to 200
        //    return View("NotFound");
        //}

        //
        // GET: /Errors/

        public ActionResult Index(int statusCode, Exception exception)
        {
            Response.StatusCode = statusCode;
            return View("Index");

        }
        public ActionResult SecurityIndex()
        {
            //Response.StatusCode = statusCode;
            return View("SecurityIndex");

        }
      
    }
}
