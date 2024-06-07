using Newtonsoft.Json;

namespace Tomsoft_zadatak.Models.ObracuniArtikliViewModels
{
    public class ObracuniArtikliRootViewModel
    {
        [JsonProperty("result")]
        public List<ObracuniArtikliResultViewModel> Result { get; set; }
    }
}
