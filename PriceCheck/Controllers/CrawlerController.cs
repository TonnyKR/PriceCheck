using Microsoft.AspNetCore.Mvc;
using PriceCheck.BusinessLogic.Interfaces;
namespace PriceCheck.API.Controllers
{
    [Route("api/Crawler")]
    public class CrawlerController : BaseController
    {
        private readonly ICrawlerService _crawlerService;
        public CrawlerController(ICrawlerService crawlerService)
        {
            _crawlerService = crawlerService;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Crawl()
        {
            await _crawlerService.Run();

        }
    }
}
