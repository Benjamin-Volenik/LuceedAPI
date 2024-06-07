using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tomsoft_zadatak.Models.ArtikliViewModels;


[ApiController]
[Route("api/artikli")]
public class ArtikliController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ArtikliController(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    [HttpGet("{naziv?}")]
    public async Task<IActionResult> GetArticlesAsync(string? naziv)
    {
        string apiUrl = _configuration["API:UrlArtikli"];

        var url = $"{apiUrl}/{naziv}";

        var response = await _httpClient.GetAsync(url);

        try
        {
            var content = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(content))
            {
                return NotFound();
            }

            ArtikliRootViewModel root = JsonConvert.DeserializeObject<ArtikliRootViewModel>(content);
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