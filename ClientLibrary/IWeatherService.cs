using Shared;

namespace ClientLibrary
{
    public interface IWeatherService
    {
        Task<WeatherDto> GetForecast();
    }
}
