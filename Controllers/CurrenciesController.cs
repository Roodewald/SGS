using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SGS.Models;

namespace SGS.Controllers
{
    [ApiController]
    [Route("currencies")]
    public class CurrenciesController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        public CurrenciesController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrencies(int pageSize = 10, int pageNumber = 1)
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


            int startIndex = (pageNumber - 1) * pageSize;
            int count = Math.Min(pageSize, currencyRate.Valute.Count - startIndex);

            var currenciesOnPage = currencyRate.Valute.Skip(startIndex).Take(count).ToList();

            return Ok(currenciesOnPage);
        }
    }
}
