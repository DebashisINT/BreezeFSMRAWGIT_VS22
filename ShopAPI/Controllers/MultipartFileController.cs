using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


using System.Configuration;
using System.IO;

using System.Web;
//using System.Web.Mvc;
using ShopAPI.Models;
//using System.Web.Mvc;

namespace ShopAPI.Controllers
{
    public class MultipartFileController : ApiController
    {
        //  [AcceptVerbs("POST")]


        [HttpPost]
        public HttpResponseMessage upload()
        {
            RegisterShopOutput omoed = new RegisterShopOutput();
            //string ImageName = file.FileName;
            //string UploadFileDirectory = "~/CommonFolder/";
            //string vPath = Path.Combine(UploadFileDirectory + "/", ImageName);


            //file.SaveAs(vPath);
            //omoed.message = "Ok";
            //omoed.status = "200";
            //return Request.CreateResponse(HttpStatusCode.BadRequest, omoed);


            var file = HttpContext.Current.Request.Files.Count > 0 ?
        HttpContext.Current.Request.Files[0] : null;

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);

                var path = Path.Combine(
                    HttpContext.Current.Server.MapPath("~/CommonFolder"),
                    fileName
                );

                file.SaveAs(path);


            }
            omoed.message = "Ok";
            omoed.status = "200";
            return Request.CreateResponse(HttpStatusCode.BadRequest, omoed);

        }
    }
}