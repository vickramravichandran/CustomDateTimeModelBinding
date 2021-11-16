using CustomDateTimeModelBinding.Helpers;
using System;
using System.Web.Http;

namespace CustomDateTimeModelBinding
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            // add the rule to the collection
            config.ParameterBindingRules.Add(typeof(DateTime?), des => new DateTimeParameterBinding(des));
        }
    }
}
