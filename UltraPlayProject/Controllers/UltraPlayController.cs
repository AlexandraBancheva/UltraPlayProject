using Microsoft.AspNetCore.Mvc;
using UltraPlayProject.Domain.Interfaces;

namespace UltraPlayProject.Controllers
{
    [ApiController]
    [Route("api/UltraPlay")]
    public class UltraPlayController : ControllerBase
    {
        public readonly IUltraPlayRepository _ultraPlayRepository;
        public UltraPlayController(IUltraPlayRepository ultraPlayRepository)
        {
            _ultraPlayRepository = ultraPlayRepository;
        }

        [HttpGet]
        public IActionResult AllMatchesStartingBy24Hours()
        {
            _ultraPlayRepository.UpdateDatabase();
            return Ok();
        }

        [HttpGet("id")]
        //i.match name
        //ii.match start date
        //iii.match's active markets with all their active odds
        //iv.match's inactive markets with their odds
        // Polymorphysm
        public IActionResult MatchByGivenUniqueIdentifier(string id) 
        {
            return Ok();
        }
    }
}
