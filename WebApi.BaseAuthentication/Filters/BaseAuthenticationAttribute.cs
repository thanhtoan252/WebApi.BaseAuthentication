using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using WebApi.BaseAuthentication.Models;

namespace WebApi.BaseAuthentication.Filters
{
    public class BaseAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple { get; }
        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            context.Principal = null;
            var authentication = context.Request.Headers.Authorization;
            if (authentication != null && authentication.Scheme == "Basic")
            {
                var auths = Encoding.ASCII.GetString(Convert.FromBase64String(authentication.Parameter)).Split(':');
                string uid = auths[0];
                string pwd = auths[1];
               context.Principal =  UserRepository.Authentication(uid, pwd);
            }
            if (context.Principal == null)
            {
                context.ErrorResult = new UnauthorizedResult(new[] { new AuthenticationHeaderValue("Basic") }, context.Request);
            }
            return Task.FromResult<object>(null);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(null);
        }
    }
}