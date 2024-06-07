using Newtonsoft.Json;
using System;

namespace Tomsoft_zadatak.Models.ObracuniArtikliViewModels
{
    public class ObracuniArtikliViewModel
    {
        [JsonProperty("artikl_uid")]
        public string? ArtiklUid { get; set; }

        [JsonProperty("naziv_artikla")]
        public string? NazivArtikla { get; set; }

        [JsonProperty("kolicina")]
        public decimal? Kolicina { get; set; }

        [JsonProperty("iznos")]
        public decimal? Iznos { get; set; }

        [JsonProperty("usluga")]
        public string? Usluga { get; set; }
    }
}
