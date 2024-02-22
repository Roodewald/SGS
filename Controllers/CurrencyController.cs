using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SGS.Models;

namespace SGS.Controllers
{

    [ApiController]
    [Route("currency")]
    public class CurrencyController(CurrencyDataFetcher currencyDataFetcher) : ControllerBase
    {
        [HttpGet("{name}")]
        async public Task<IActionResult> GetCuurencyVlaue(string name = "USD")
        {
            CurrencyRate currencyRate = await currencyDataFetcher.GetCurrencies();

            if (currencyRate.Valute.ContainsKey(name))
            {
                return Ok(currencyRate.Valute[name].Value);
			}
            else
            {
                return BadRequest("Invaid key: " + name);
			}
        }
    }
}
