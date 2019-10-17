using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using facebook.Models;
using System.Security.Cryptography;
using System.Text;
using facebook.ViewModels;

namespace facebook.Controllers
{
    public class HomeController : Controller
    {
        public static string MD5Hash(string itemToHash)
        {
            if  (itemToHash == null)
            {
                return string.Join("", "<script language='javascript' type='text/javascript'>alert('login Fail :USER:'); window.location.href='home/index'</script>");
            }
            else
            {
                return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(itemToHash)).Select(s => s.ToString("x2")));

            }
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserViewModel objInputi)
        {

            appEntities contex = new appEntities();

            string aa = (MD5Hash(objInputi.password)).ToString();

            var person = (from a in contex.useris
                          where a.email == objInputi.email && a.password == aa
                          select a).FirstOrDefault();

            if (person != null)
            {
                var adminOrUser = contex.useris.Where(a => a.AdminOrUser == 0 && a.email == objInputi.email).FirstOrDefault();



                if (adminOrUser != null)
                {
                    Session["UserEmail"] = objInputi.email;
                    return Content("<script language='javascript' type='text/javascript'>alert('login succees :USER:'); window.location.href='home/index'</script>");
                }

                else
                {
                    Session["UserEmail"] = objInputi.email;
                    return Content("<script language='javascript' type='text/javascript'>alert('login succees :ADMIN:'); window.location.href='admin/admin'</script>");

                }


            }

            else
                return Content("<script language='javascript' type='text/javascript'>alert('login fail');window.location.href='https://localhost:44303/home/index'</script>");

        }

        public ActionResult logohu()
        {

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}