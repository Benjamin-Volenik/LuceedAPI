using Newtonsoft.Json;

namespace Tomsoft_zadatak.Models.ObracuniPlacanjaViewModels
{
    public class ObracuniPlacanjaResultViewModel
    {
        [JsonProperty("obracun_placanja")]
        public List<ObracuniPlacanjaViewModel> ObracunPlacanja { get; set; }
    }

}
