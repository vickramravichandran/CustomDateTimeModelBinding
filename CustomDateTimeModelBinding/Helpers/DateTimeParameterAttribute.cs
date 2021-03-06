using System;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace CustomDateTimeModelBinding.Helpers
{
    public class DateTimeParameterAttribute : ParameterBindingAttribute
    {
        public string Name { get; set; }
        public string DateFormat { get; set; }
        public bool FromUri { get; set; }

        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            if (parameter.ParameterType == typeof(DateTime?))
            {
                return new DateTimeParameterBinding(parameter)
                {
                    ModelName = Name,
                    DateFormat = DateFormat,
                    FromUri = FromUri
                };
            }

            return parameter.BindAsError("Expected type DateTime?");
        }
    }
}