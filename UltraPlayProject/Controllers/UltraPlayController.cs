using Microsoft.AspNetCore.Mvc;
using UltraPlayProject.Domain.Interfaces;

namespace UltraPlayProject.Controllers
{
    [ApiController]
    [Route("api/UltraPlay")]
    public class UltraPlayController : ControllerBase
    {
        public readonly IUltraPlayProjectService _ultraPlayProjectService;
        public readonly IUltraPlayRepository _ultraPlayRepository;
        public UltraPlayController(IUltraPlayProjectService ultraPlayProjectService, IUltraPlayRepository ultraPlayRepository)
        {
            _ultraPlayProjectService = ultraPlayProjectService;
            _ultraPlayRepository = ultraPlayRepository;
        }

        [HttpGet]
        public IActionResult AllMatchesStartingBy24Hours()
        {
           // _ultraPlayRepository.GetDataFromXmlFile();
            _ultraPlayProjectService.GetAllMarkets24Hours();
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
