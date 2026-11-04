using ClientLibrary.ViewModels;
namespace SomeApp;

public partial class MainPage : ContentPage
{
	private WeatherView vm;
	public MainPage(WeatherView viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		vm = viewModel;
	}
}
