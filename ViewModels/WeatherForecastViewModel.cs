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
    public partial class WeatherForecastViewModel : ViewModelBase
    {
        private readonly IClientService _clientService;


        public ObservableCollection<WeatherForecast> WeatherForecasts { get; set; } = [];

        public WeatherForecastViewModel(IClientService clientService)
        {
            _clientService = clientService;
        }
    }
}
