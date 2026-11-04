using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Shared
{
    public partial class WeatherDto : ObservableObject
    {
        [ObservableProperty]
        private int temperatureF;
        [ObservableProperty]
        private DateOnly date;
        [ObservableProperty]
        private string? summary;

    }
}
