// Clayton DeSimone
// .NET Applications
// Final Project
// 4/29/2024

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceMAUI.Models;
using FinanceMAUI.Services;
using FinanceMAUI.ViewModels;
using FinanceMAUI.ViewModels.Base;
using FinanceMAUI.Views;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinanceMAUI.ViewModels.Base
{
    public partial class ViewModelBase : ObservableValidator, IViewModelBase
    {
        [ObservableProperty]
        private bool _isLoading;

        public IAsyncRelayCommand InitializeAsyncCommand { get; }

        public ViewModelBase()
        {
            InitializeAsyncCommand = new AsyncRelayCommand(
                async () =>
                {
                    IsLoading = true;
                    await Loading(LoadAsync);
                    IsLoading = false;
                });
        }

        protected async Task Loading(Func<Task> unitOfWork)
        {
            await unitOfWork();
        }

        public virtual Task LoadAsync()
        {
            return Task.CompletedTask;
        }
    }
}
