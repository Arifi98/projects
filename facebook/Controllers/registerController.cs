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
    public class registerController : Controller
    {
        // GET: register

        public static string MD5Hash(string itemToHash)
        {

            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(itemToHash)).Select(s => s.ToString("x2")));
        }
        public ActionResult register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult register(UserViewModel objinput)
        {
            using (var ctx = new appEntities())
            {
                var register = ctx.useris.Where(a => a.email == objinput.email).FirstOrDefault();
                if (register !=null)             
                return Content("<script language='javascript' type='text/javascript'>alert('ky email ekziston');</script>");
                else
                {
                    useri registeo = new useri();
                    registeo.email = objinput.email;
                    registeo.emer = objinput.emer;
                    registeo.mbiemer = objinput.mbiemer;
                    registeo.password = MD5Hash(objinput.password);
                    ctx.useris.Add(registeo);
                    ctx.SaveChanges();
                    return RedirectToAction("Index", "Home");

                }
            }



        }
    }
}