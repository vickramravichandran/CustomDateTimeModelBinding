using System.Web;
using System.Web.Http;

namespace CustomDateTimeModelBinding
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
