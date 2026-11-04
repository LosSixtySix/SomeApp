using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientLibrary.ViewModels
{
    public partial class WeatherView(IWeatherService service): ObservableObject
    {

        [ObservableProperty]
        private WeatherDto? forecast;

        [RelayCommand]
        public async Task GetForecast()
        {
            Forecast = await service.GetForecast();
        }
    }
}
