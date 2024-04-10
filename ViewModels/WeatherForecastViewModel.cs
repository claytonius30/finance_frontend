using FinanceMAUI.Models;
using FinanceMAUI.Services;
using FinanceMAUI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.ViewModels
{
    public class WeatherForecastViewModel : ViewModelBase
    {
        private readonly ClientService _clientService;


        public ObservableCollection<WeatherForecast> WeatherForecasts { get; set; } = [];

        public WeatherForecastViewModel(ClientService clientService)
        {
            _clientService = clientService;
            LoadWeatherForecastData();
        }

        private async void LoadWeatherForecastData()
        {
            var response = await _clientService.GetWeatherForeCastData();
            WeatherForecasts?.Clear();
            if (response.Any())
            {
                foreach (var weatherForecast in response)
                    WeatherForecasts!.Add(weatherForecast);
            }
        }
    }
}
