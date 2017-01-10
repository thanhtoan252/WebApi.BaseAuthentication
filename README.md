## Base Authentication with Web Api C##
###### 1. Create user class inherit from interface "IIdentity" and "IPrincipal" (**Users.cs**)
```
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
```
###### 2. Create authentication user class (**UserRepository.cs**)
Set default : uid : admin & pwd : 1111 
```
public class UserRepository
    {
        public static Users Authentication(string uid, string pwd)
        {
            if (uid == "admin" && pwd == "1111")
                return new Users(uid);
            return null;
        }
    }
```
###### 3. Create filter (**BaseAuthenticationAttribute.cs**)
```
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
```
###### 4. Create account controller (**AccountController.cs**)
```
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
```
###### 5. Client request
```
Call Url :  http://localhost:1234/api/account
Method :  Get
Header : - user : admin
         - password : 1111
         - Content-Type : application/x-www-form-urlencoded
```
