using Newtonsoft.Json;

namespace Tomsoft_zadatak.Models.ObracuniArtikliViewModels
{
    public class ObracuniArtikliResultViewModel
    {
        [JsonProperty("obracun_artikli")]
        public List<ObracuniArtikliViewModel> ObracunArtikli { get; set; }
    }

}
