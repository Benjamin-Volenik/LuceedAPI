using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tomsoft_zadatak.Models.ObracuniArtikliViewModels;
using Tomsoft_zadatak.Models.ObracuniPlacanjaViewModels;


[ApiController]
[Route("api/obracuni")]
public class ObracuniController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ObracuniController(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    [HttpGet("{obracunPrometa}/{poslovnaJedinicaUid}/{datumOd}/{datumDo?}")]
    public async Task<IActionResult> GetAsync(string obracunPrometa, string poslovnaJedinicaUid, string datumOd, string? datumDo)
    {
        DateTime DateFrom = DateTime.ParseExact(datumOd, "yyyy-MM-dd", null);
        string formattedDateFrom = DateFrom.ToString("d.M.yyyy");

        string apiUrl = _configuration["API:UrlObracunPrometa"];

        string urlDodatak = "";

        if (obracunPrometa == "vrstaplacanja")
        {
            urlDodatak = "placanja";
        } else if (obracunPrometa == "artikli")
        {
            urlDodatak = "artikli";
        }

        var url = $"{apiUrl}/{urlDodatak}/{poslovnaJedinicaUid}/{formattedDateFrom}";

        if (datumDo != null)
        {
            DateTime DateTo = DateTime.ParseExact(datumDo, "yyyy-MM-dd", null);
            string formattedDateDateTo = DateTo.ToString("d.M.yyyy");

            url = url + $"/{formattedDateDateTo}";
        }

        var response = await _httpClient.GetAsync(url);

        try
        {
            var content = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(content))
            {
                return NotFound();
            }

            object root = null;

            if (obracunPrometa == "vrstaplacanja")
            {
                root = JsonConvert.DeserializeObject<ObracuniPlacanjaRootViewModel>(content);
            }
            else if (obracunPrometa == "artikli")
            {
                root = JsonConvert.DeserializeObject<ObracuniArtikliRootViewModel>(content);
            }

            if (root is ObracuniPlacanjaRootViewModel vrstaplacanjaRoot)
            {
                return Ok(vrstaplacanjaRoot.Result);
            }
            else if (root is ObracuniArtikliRootViewModel artikliRoot)
            {
                return Ok(artikliRoot.Result);
            }
            else
            {
                return BadRequest(new { Error = "Invalid root object type" });
            }
        }
        catch (JsonException ex)
        {
            return BadRequest(new { Error = "Failed to deserialize JSON: " + ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new { Error = "An error occurred: " + ex.Message });
        }
    }
}