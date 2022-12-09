using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ShopAPI.Models
{
    public class Attribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actntext)
        {
            if (actntext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                actntext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                {
                    ReasonPhrase = "Need of HTTPS"
                };
            }
            else
            {
                base.OnAuthorization(actntext);
            }
        }
    }
}