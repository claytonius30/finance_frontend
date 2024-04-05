using CommunityToolkit.Mvvm.ComponentModel;
using FinanceMAUI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using FinanceMAUI.Services;
using FinanceMAUI.Models;

namespace FinanceMAUI.ViewModels
{
    public partial class IncomeAddEditViewModel : ViewModelBase
    {
        private readonly IIncomeService _incomeService;

        public IncomeModel? incomeDetail;

        [ObservableProperty]
        private string _pageTitle = default!;

        [ObservableProperty]
        private int _incomeId;

        [ObservableProperty]
        private string? _source;

        [ObservableProperty]
        private double _amount;

        [ObservableProperty]
        private DateTime _dateReceived;

        public ObservableCollection<string> Incomes { get; set; } = new();

        //[ObservableProperty]
        //[NotifyCanExecuteChangedFor(nameof(AddIncomeCommand))]
        //private string _addedIncome = default!;

        //[RelayCommand(CanExecute = nameof(CanAddIncome))]
        //private void AddIncome()
        //{
        //    Incomes.Add(AddedIncome);
        //    AddedIncome = string.Empty;
        //}

        //private bool CanAddIncome() => !string.IsNullOrWhiteSpace(AddedIncome);

        //[RelayCommand(CanExecute = nameof(CanSubmitIncome))]
        //private async Task Submit()
        //{
        //}

        private bool CanSubmitEvent() => true;

        public IncomeAddEditViewModel(IIncomeService eventService)
        {
            _incomeService = eventService;
        }

        //public override async Task LoadAsync()
        //{
        //    await Loading(
        //        async () =>
        //        {
        //            if (IncomeDetail is null && IncomeId != int.Empty)
        //            {
        //                incomeDetail = await _incomeService.GetIncome(IncomeId);
        //            }
        //            MapEvent(incomeDetail);
        //        });
        //}

        //private void MapEvent(IncomeModel? model)
        //{
        //    if (model is not null)
        //    {
        //        IncomeId = model.IncomeId;
        //        Source = model.Source;
        //        (decimal) Amount = model.Amount;
        //        Date = model.Date;
        //        Description = model.Description;
        //        Category = Categories.FirstOrDefault(c => c.Id == model.Category.Id && c.Name == model.Category.Name);
        //        foreach (string artist in model.Artists)
        //        {
        //            Artists.Add(artist);
        //        }
        //    }

        //    PageTitle = Id != Guid.Empty ? "Edit event" : "Add event";
        //}
    }
}
