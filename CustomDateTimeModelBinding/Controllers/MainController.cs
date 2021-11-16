using CustomDateTimeModelBinding.Helpers;
using CustomDateTimeModelBinding.Model;
using System;
using System.Web.Http;

namespace CustomDateTimeModelBinding.Controllers
{
    [RoutePrefix("api/main")]
    public class MainController : ApiController
    {
        [Route("echo-date/{date}")]
        [HttpGet]
        public DateTime? EchoDate(
            [DateTimeParameter]
            DateTime? date)
        {
            return date;
        }

        [Route("echo-custom-date/{date}")]
        [HttpGet]
        public DateTime? EchoCustomDateFormat(
            [DateTimeParameter(DateFormat = "yyyy-MM-dd")]
            DateTime? date)
        {
            return date;
        }

        [Route("echo-model")]
        [HttpPost]
        public PostData EchoModel(PostData model)
        {
            return model;
        }
    }
}
