using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SGS.Models;
using System.Text.Json;

namespace SGS
{
    public static class CurrencyDataFetcher
    {
        public static async Task<CurrencyRate> GetCurrencies(IMemoryCache cache)
        {
            var httpClient = new HttpClient();

            if (!cache.TryGetValue("CurrencyData", out CurrencyRate cachedData))
            {
                Console.WriteLine("Кэш пустой, получаю запрос");
                var response = await httpClient.GetAsync("https://www.cbr-xml-daily.ru/daily_json.js");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Ошибка при считывании: {response.StatusCode}");
                }

                var responseData = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                CurrencyRate currencyData = JsonSerializer.Deserialize<CurrencyRate>(responseData, options);

                cache.Set("CurrencyData", currencyData, TimeSpan.FromMinutes(1));

                return currencyData;
            }

            return cachedData;
        }
    }
}
