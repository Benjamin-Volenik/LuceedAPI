using Newtonsoft.Json;

namespace Tomsoft_zadatak.Models.ArtikliViewModels
{
    public class ArtikliRootViewModel
    {
        [JsonProperty("result")]
        public List<ArtikliResultViewModel> Result { get; set; }
    }
}
