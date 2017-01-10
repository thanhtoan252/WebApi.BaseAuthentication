using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WebApi.BaseAuthentication.Models
{
    public class Users : IIdentity, IPrincipal
    {
        public Users( string userName)
        {
            this.Name = userName;
            this.AuthenticationType = "Basic";
            this.IsAuthenticated = true;
        }
        public string Name { get; }
        public string AuthenticationType { get; }
        public bool IsAuthenticated { get; }
        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }

        public IIdentity Identity => this;
    }
}