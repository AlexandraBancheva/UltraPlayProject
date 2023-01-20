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
            var result = _ultraPlayProjectService.GetAllMarkets24Hours().ToList();
            return Ok(result);
        }

        [HttpGet("id")]
        public IActionResult MatchByGivenUniqueIdentifier([FromQuery] int id) 
        {
            _ultraPlayProjectService.GetMatchById(id);
            return Ok();
        }
    }
}
