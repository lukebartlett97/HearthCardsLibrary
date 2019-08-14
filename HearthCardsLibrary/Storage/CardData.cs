﻿using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HearthCardsLibrary.Storage
{

    public partial class CardData
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("cardtext")]
        public string Cardtext { get; set; }

        [JsonProperty("swirl")]
        public string Swirl { get; set; }

        [JsonProperty("mana")]
        public string Mana { get; set; }

        [JsonProperty("attack")]
        public string Attack { get; set; }

        [JsonProperty("health")]
        public string Health { get; set; }

        [JsonProperty("cardtype")]
        public string Cardtype { get; set; }

        [JsonProperty("gem")]
        public string Gem { get; set; }
    }

    public partial class CardData
    {
        public static CardData[] FromJson(string json) => JsonConvert.DeserializeObject<CardData[]>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this CardData[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
