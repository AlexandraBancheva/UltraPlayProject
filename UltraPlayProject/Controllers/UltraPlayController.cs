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
        public IActionResult AllMatchingStartingNext24Hours()
        {
            return Ok(_ultraPlayProjectService.GetAllMatchesStartingNext24H());
        }

        [HttpGet("matchId")]
        public IActionResult MatchByGivenUniqueIdentifier([FromQuery] int matchId) 
        {
            return Ok(_ultraPlayProjectService.GetMatchById(matchId));
        }
    }
}
