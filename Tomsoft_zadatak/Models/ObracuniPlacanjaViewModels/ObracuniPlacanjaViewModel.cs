using Newtonsoft.Json;
using System;

namespace Tomsoft_zadatak.Models.ObracuniPlacanjaViewModels
{
    public class ObracuniPlacanjaViewModel
    {
        [JsonProperty("vrste_placanja_uid")]
        public string? VrstaPlacnjaUid { get; set; }

        [JsonProperty("naziv")]
        public string? Naziv { get; set; }

        [JsonProperty("iznos")]
        public decimal Iznos { get; set; }

        [JsonProperty("nadgrupa_placanja_uid")]
        public string? NadgrupaPlacanjaUid { get; set; }

        [JsonProperty("nadgrupa_placanja_naziv")]
        public string? NadgrupaPlacanjaNaziv { get; set; }
    }
}
