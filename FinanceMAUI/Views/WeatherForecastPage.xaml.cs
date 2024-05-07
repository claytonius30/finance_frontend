using FinanceMAUI.ViewModels;

namespace FinanceMAUI.Views;

public partial class WeatherForecastPage : ContentPage
{
	public WeatherForecastPage(WeatherForecastViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}