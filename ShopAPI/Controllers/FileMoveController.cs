using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAPI.Controllers
{
    public class FileMoveController : Controller
    {
        //
        // GET: /FileMove/
        [HttpPost]
        public JsonResult File(FileMove model)
        {
            


            string output = "";


            switch (model.DataType)
            {
                case "1":
                    string InboundFilePath = "~/APIFiles/Inbound";
                    if (model.File != null)
                    {
                        model.File.SaveAs(Path.Combine(Server.MapPath(InboundFilePath), model.File.FileName));
                        output = "Success";
                    }
                    break;
                case "2":
                    output = "Faliure";
                    break;
            }





        




            return Json(output);
        }
    }
}