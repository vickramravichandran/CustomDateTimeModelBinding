using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace CustomDateTimeModelBinding.Helpers
{
    public class DateTimeParameterBinding : HttpParameterBinding
    {
        public string ModelName { get; set; }
        public string DateFormat { get; set; }
        public bool ReadFromQueryString { get; set; }

        public DateTimeParameterBinding(HttpParameterDescriptor descriptor)
            : base(descriptor) { }

        public override Task ExecuteBindingAsync(
            ModelMetadataProvider metadataProvider,
            HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            string dateToParse = null;
            var paramName = !string.IsNullOrEmpty(ModelName) ? ModelName : this.Descriptor.ParameterName;

            if (ReadFromQueryString)
            {
                // reading from query string
                var nameVal = actionContext.Request.GetQueryNameValuePairs();
                var queryData = nameVal.Where(q => q.Key.Equals(paramName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                dateToParse = queryData.Value;
            }
            else
            {
                // reading from route
                var routeData = actionContext.Request.GetRouteData();
                if (routeData.Values.TryGetValue(paramName, out var dateObj))
                {
                    dateToParse = Convert.ToString(dateObj);
                }
            }

            DateTime? dateTime = null;
            if (!string.IsNullOrEmpty(dateToParse))
            {
                if (string.IsNullOrEmpty(DateFormat))
                {
                    dateTime = ParseDateTime(dateToParse);
                }
                else
                {
                    dateTime = ParseDateTime(dateToParse, new string[] { DateFormat });
                }
            }

            SetValue(actionContext, dateTime);

            return Task.FromResult<object>(null);
        }

        public DateTime? ParseDateTime(
            string dateToParse,
            string[] formats = null,
            IFormatProvider provider = null,
            DateTimeStyles styles = DateTimeStyles.AssumeLocal)
        {
            var CUSTOM_DATE_FORMATS = new string[]
            {
                "yyyyMMddTHHmmssZ",
                "yyyyMMddTHHmmZ",
                "yyyyMMddTHHmmss",
                "yyyyMMddTHHmm",
                "yyyyMMddHHmmss",
                "yyyyMMddHHmm",
                "yyyyMMdd",
                "yyyy-MM-dd-HH-mm-ss",
                "yyyy-MM-dd-HH-mm",
                "yyyy-MM-dd",
                "MM-dd-yyyy"
            };

            if (formats == null)
            {
                formats = CUSTOM_DATE_FORMATS;
            }

            DateTime validDate;

            foreach (var format in formats)
            {
                if (format.EndsWith("Z"))
                {
                    if (DateTime.TryParseExact(dateToParse, format,
                             provider,
                             DateTimeStyles.AssumeUniversal,
                             out validDate))
                    {
                        return validDate;
                    }
                }
                else
                {
                    if (DateTime.TryParseExact(dateToParse, format,
                             provider, styles, out validDate))
                    {
                        return validDate;
                    }
                }
            }

            return null;
        }
    }
}
