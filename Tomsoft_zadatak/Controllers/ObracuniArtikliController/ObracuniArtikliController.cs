using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using Tomsoft_zadatak.Models.ObracuniArtikliViewModels;


[ApiController]
[Route("api/obracuni-artikli")]
public class ObracuniArtikliController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ObracuniArtikliController(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    [HttpGet("{poslovnaJedinicaUid}/{datumOd}/{datumDo?}")]
    public async Task<IActionResult> GetArticlesAsync(string poslovnaJedinicaUid, string datumOd, string? datumDo)
    {
        DateTime DateFrom = DateTime.ParseExact(datumOd, "yyyy-MM-dd", null);
        string formattedDateFrom = DateFrom.ToString("d.M.yyyy");

        string apiUrl = _configuration["API:UrlObracunArtikli"];

        var url = $"{apiUrl}/{poslovnaJedinicaUid}/{formattedDateFrom}";

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

            ObracuniArtikliRootViewModel root = JsonConvert.DeserializeObject<ObracuniArtikliRootViewModel>(content);
            if (root == null)
            {
                return BadRequest(new { Error = "Failed to deserialize JSON" });
            }

            return Ok(root.Result);
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