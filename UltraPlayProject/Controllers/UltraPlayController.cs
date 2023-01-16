using Microsoft.AspNetCore.Mvc;

namespace UltraPlayProject.Controllers
{
    [ApiController]
    [Route("api/UltraPlay")]
    public class UltraPlayController : ControllerBase
    {
        [HttpGet]
        public IActionResult AllMatchesStartingBy24Hours()
        {
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
