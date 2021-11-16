using CustomDateTimeModelBinding.Converters;
using Newtonsoft.Json;
using System;

namespace CustomDateTimeModelBinding.Model
{
    public class PostData
    {
        [JsonConverter(typeof(CustomDateTimeConverter), new object[] { "yyyy-MM-dd" })]
        public DateTime DateFrom { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter), new object[] { "yyyy-MM-dd" })]
        public DateTime? DateTo { get; set; }
    }
}
