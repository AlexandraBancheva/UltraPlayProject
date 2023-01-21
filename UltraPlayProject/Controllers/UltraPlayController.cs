using Microsoft.AspNetCore.Mvc;
using UltraPlayProject.Domain.Interfaces;

namespace UltraPlayProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UltraPlayController : ControllerBase
    {
        public readonly IUltraPlayProjectService _ultraPlayProjectService;

        public UltraPlayController(IUltraPlayProjectService ultraPlayProjectService)
        {
            _ultraPlayProjectService = ultraPlayProjectService;
        }

        [HttpGet]
        public IActionResult AllMatchesStartingBy24Hours()
        {
            var result = _ultraPlayProjectService.GetAllMarkets24Hours().ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult MatchByGivenUniqueIdentifier([FromQuery] int id) 
        {
            var result = _ultraPlayProjectService.GetMatchById(id);
            return Ok(result);
        }
    }
}
