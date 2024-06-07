using Newtonsoft.Json;
using System.Reflection.Emit;

namespace Tomsoft_zadatak.Models.ArtikliViewModels
{
    public class ArtikliResultViewModel
    {
        [JsonProperty("artikli")]
        public List<ArtikliViewModel> Artikli { get; set; }
    }

}
