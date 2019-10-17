using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using facebook.Models;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace facebook.Controllers
{
    public class adminController : Controller
    {
        public ActionResult admin()
        {
            using (var contex = new appEntities())
            {
                var objDBB = contex.useris.ToList();
                if (objDBB != null)
                {
                    return View("admin", objDBB);
                }
            }
            return View();
        }
        [HttpPost]
        public JsonResult admin(string search)
        {
            
            appEntities contex = new appEntities();
            //Filtrim sipas inputit             
                var RecordFiltred = contex.useris.Where(x => x.emer.Contains(search) || x.email.Contains(search)
                 || x.ID.ToString().Contains(search) || x.mbiemer.Contains(search) || x.password.Contains(search) || x.AdminOrUser.ToString().Contains(search)).ToList();

            System.IO.FileStream fs = new FileStream(Server.MapPath("~/App_Data") + "\\" + "document.pdf", FileMode.Create);

            
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
         

            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            PdfPTable table = new PdfPTable(6);

            PdfPCell cell = new PdfPCell(new Phrase("Raport Data Table"));
            cell.Colspan = 6;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            table.AddCell("PersonID");
            table.AddCell("Emer");
            table.AddCell("Mbiemer");
            table.AddCell("Email");
            table.AddCell("Password Encrypted");
            table.AddCell("Admin Or User");

            foreach (var item in RecordFiltred)
            {
                table.AddCell((item.ID).ToString());
                table.AddCell(item.emer);
                table.AddCell(item.mbiemer);
                table.AddCell(item.email);
                table.AddCell(item.password);
                table.AddCell((item.AdminOrUser).ToString());

            }

            // Open the document to enable you to write to the document  
            document.Open();
            //document.Add(new Paragraph());
            document.Add(table);
            // Close the document  
            document.Close();
            // Close the writer instance  
            writer.Close();
            // Always close open filehandles explicity  
            fs.Close();
            Download();
            return Json("ok");
        }


        public FileResult Download()
        {
            string path = Server.MapPath("~/App_Data");
            string filename = Path.GetFileName("document.pdf");
            string fullpath = Path.Combine(path, filename);
            return File(fullpath, "application/pdf", "document.pdf");
        }
        
    }
}