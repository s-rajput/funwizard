using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Config
{
    public class ConfigSettings
    {

        public class FaceboookOperations
        {
            public static string TokenAccessPath(string FacebookBasePath,string AppId, string AppSecret)
            {
                return $"{FacebookBasePath}/oauth/access_token?client_id={AppId}&client_secret={AppSecret}&grant_type=client_credentials";
            }

            public static string TokenValidationPath(string FacebookBasePath, string Token, string AccessToken)
            {
                return $"{FacebookBasePath}/debug_token?input_token={Token}&access_token={AccessToken}";
            }

            public static string UserInfo(string FacebookBasePath, string Token)
            {
                return $"{FacebookBasePath}/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={Token}";
            }

            public static string Search() => "/Search";
        }

        public class GoogleOperations
        {
            public static string Get() => "/Get";
            public static string Search() => "/Search";
        }

        //facebook
        public string FacebookBaseURL { get; set; }
        public string FacebookAppId { get; set; }
        public string FacebookAppSecret { get; set; }


        //google
        public string GoogleBaseURL { get; set; }
        //


    }
}
