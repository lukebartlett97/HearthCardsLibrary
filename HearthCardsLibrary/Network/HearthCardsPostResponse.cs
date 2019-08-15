using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthCardsLibrary.Network
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class HearthCardsPostResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("cardid")]
        public string Cardid { get; set; }
    }

    public partial class HearthCardsPostResponse
    {
        public static HearthCardsPostResponse FromJson(string json) => JsonConvert.DeserializeObject<HearthCardsPostResponse>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this HearthCardsPostResponse self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
