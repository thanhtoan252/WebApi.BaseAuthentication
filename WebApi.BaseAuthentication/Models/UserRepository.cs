using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.BaseAuthentication.Models
{
    public class UserRepository
    {
        public static Users Authentication(string uid, string pwd)
        {
            if (uid == "admin" && pwd == "1111")
                return new Users(uid);
            return null;
        }
    }
}