using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace facebook.Controllers
{
    public class downloadPDFController : Controller
    {
        // GET: downloadPDF
        public ActionResult Index()
        {

            return View();
        }
    }
}