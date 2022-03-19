using System.Collections.Generic;

namespace NSE.WebApp.MVC.Extensions
{
    public class AppSettings
    {
        public AuthenticationAPI AuthenticationAPI { get; set; }
        public string CatalogueUrl { get; set; }
    }

    public class AuthenticationAPI
    {
        public string BaseUrl { get; set; }
        public string AuthenticatePath { get; set; }
        public string NewAccountPath { get; set; }

    }
}
