using Newtonsoft.Json;

namespace Tomsoft_zadatak.Models.ObracuniPlacanjaViewModels
{
    public class ObracuniPlacanjaRootViewModel
    {
        [JsonProperty("result")]
        public List<ObracuniPlacanjaResultViewModel> Result { get; set; }
    }
}
