using Newtonsoft.Json;
using System;

namespace Tomsoft_zadatak.Models.ArtikliViewModels
{
    public class ArtikliViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("artikl")]
        public string? Artikl { get; set; }

        [JsonProperty("naziv")]
        public string? Naziv { get; set; }
    }
}
