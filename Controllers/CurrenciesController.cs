using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SGS.Models;

namespace SGS.Controllers
{
    [ApiController]
    [Route("currencies")]
    public class CurrenciesController(IMemoryCache memoryCache) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCurrencies(int pageSize = 10, int pageNumber = 1)
        {
            CurrencyRate currencyRate = await CurrencyDataFetcher.GetCurrencies(memoryCache);

            int startIndex = (pageNumber - 1) * pageSize;
            int count = Math.Min(pageSize, currencyRate.Valute.Count - startIndex);

            var currenciesOnPage = currencyRate.Valute.Skip(startIndex).Take(count).ToList();

            return Ok(currenciesOnPage);
        }
    }
}
