using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;
using WebApi.BaseAuthentication.Filters;

namespace WebApi.BaseAuthentication.Controllers
{
    public class AccountController : ApiController
    {
        [BaseAuthentication]
        public string Gets()
        {
            string auth = Request.Headers.Authorization.Parameter;
            if (auth == null)
            {
                return "No authentication";
            }
            return Encoding.ASCII.GetString(Convert.FromBase64String(auth));
        }
    }
}
