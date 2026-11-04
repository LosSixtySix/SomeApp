using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using Shared;

namespace ClientLibrary
{
    public class WeatherService(HttpClient client) : IWeatherService
    {
        public async Task<WeatherDto> GetForecast()
        {
            string requestUri = "/weatherforecast";

            var response = await client.GetAsync(requestUri);
            var responseContent = await response.Content.ReadFromJsonAsync<List<WeatherDto>>();

            return responseContent.FirstOrDefault();
        }
    }
}
