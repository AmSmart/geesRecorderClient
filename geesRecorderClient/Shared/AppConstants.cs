using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared
{
    public class AppConstants
    {
        //public const string ApiBaseAddress = "https://localhost:3001/";
        public const string ApiBaseAddress = "https://gees-recorder.herokuapp.com";
        public const string ApiSignUp = "/auth/signup";
        public const string ApiSignIn = "/auth/signin";
        public const string ApiRefreshToken = "/auth/refresh";
    }
}