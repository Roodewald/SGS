using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SGS.Models;

namespace SGS.Controllers
{

    [ApiController]
    [Route("currency")]
    public class CurrencyController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        public CurrencyController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        [HttpGet("{name}")]
        async public Task<IActionResult> GetCuurencyVlaue(string name = "USD")
        {
            CurrencyRate currencyRate;
            try
            {
                currencyRate = await CurrencyDataFetcher.GetCurrencies(_cache);
            }
            catch (HttpRequestException)
            {
                return new StatusCodeResult(503);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
            return Ok(currencyRate.Valute[name].Value);
        }
    }
}
