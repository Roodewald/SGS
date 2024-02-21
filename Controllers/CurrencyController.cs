using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SGS.Models;

namespace SGS.Controllers
{

    [ApiController]
    [Route("currency")]
    public class CurrencyController(IMemoryCache memoryCache) : ControllerBase
    {
        [HttpGet("{name}")]
        async public Task<IActionResult> GetCuurencyVlaue(string name = "USD")
        {
            CurrencyRate currencyRate = await CurrencyDataFetcher.GetCurrencies(memoryCache);


            return Ok(currencyRate.Valute[name].Value);
        }
    }
}
