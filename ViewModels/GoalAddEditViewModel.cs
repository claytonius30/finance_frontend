using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FinanceMAUI.Messages;
using FinanceMAUI.Models;
using FinanceMAUI.Services;
using FinanceMAUI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.ViewModels
{
    public partial class GoalAddEditViewModel : ViewModelBase, IQueryAttributable
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        public GoalModel? goalDetail;

        [ObservableProperty]
        private string _pageTitle = default!;

        [ObservableProperty]
        private int _goalId;

        //[Required]
        [MinLength(1)]
        [MaxLength(50)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string? _description;

        [Required]
        //[Range(0, 1000000000)]
        [CustomValidation(typeof(IncomeAddEditViewModel), nameof(ValidateAmount))]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private decimal _amount;

        public static ValidationResult? ValidateAmount(decimal amount, ValidationContext context)
        {
            if (amount < (decimal)0.01 || amount > 999999999)
            {
                return new("Amount must be above 0 and below 1 billion.");
            }

            return ValidationResult.Success;
        }

        [Required]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private DateTime _date = DateTime.Now.AddDays(1);

        [ObservableProperty]
        private DateTime _minDate = DateTime.Now;

        [ObservableProperty]
        private Guid _userId;

        public ObservableCollection<ValidationResult> Errors { get; } = new();

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

        [RelayCommand]
        private async Task Back()
        {
            if (GoalId > 0)
            {
                await _navigationService.GoToGoals(UserId);
            }
            else
            {
                await _navigationService.GoToUserDetail(UserId);
            }
        }

        [RelayCommand(CanExecute = nameof(CanSubmitGoal))]
        private async Task Submit()
        {
            ValidateAllProperties();
            if (Errors.Any())
            {
                return;
            }

            GoalModel model = MapDataToGoalModel();

            if (goalDetail == null)
            {
                if (await _userService.CreateGoal(model))
                {
                    WeakReferenceMessenger.Default.Send(new GoalAddedOrChangedMessage());
                    await _dialogService.Notify("Success", "The goal is added.");
                    await _navigationService.GoToGoals(UserId);
                }
                else
                {
                    await _dialogService.Notify("Failed", "Description field is empty.");
                }
            }
            else
            {
                if (await _userService.EditGoal(model))
                {
                    WeakReferenceMessenger.Default.Send(new GoalAddedOrChangedMessage());
                    await _dialogService.Notify("Success", "The goal is updated.");
                    //await _navigationService.GoBack();
                    //await _navigationService.GoToUserIncomes(UserId)
                    await _navigationService.GoToGoals(UserId);
                }
                else
                {
                    await _dialogService.Notify("Failed", "Editing the goal failed.");
                }
            }
        }

        private bool CanSubmitGoal() => !HasErrors;



        public GoalAddEditViewModel(IUserService userService, INavigationService navigationService, IDialogService dialogService)
        {
            _userService = userService;
            _navigationService = navigationService;
            _dialogService = dialogService;

            _amount = 0.01m;

            ErrorsChanged += AddGoalViewModel_ErrorsChanged;
        }

        public override async Task LoadAsync()
        {
            await Loading(
                async () =>
                {
                    if (goalDetail is null && GoalId > 0)
                    {
                        goalDetail = await _userService.GetGoal(UserId, GoalId);
                    }
                    MapGoal(goalDetail);

                    ValidateAllProperties();
                });
        }

        private void AddGoalViewModel_ErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            Errors.Clear();
            GetErrors().ToList().ForEach(Errors.Add);
            SubmitCommand.NotifyCanExecuteChanged();
        }

        private void MapGoal(GoalModel? model)
        {
            if (model is not null)
            {
                GoalId = model.GoalId;
                Date = model.Date;
                Amount = model.Amount;
                Description = model.Description;
                UserId = model.Id;
            }

            PageTitle = GoalId > 0 ? "Edit Goal" : "Add Goal";
        }

        private GoalModel MapDataToGoalModel()
        {
            return new GoalModel
            {
                GoalId = GoalId,
                Date = Date,
                Amount = Amount,
                Description = Description ?? string.Empty,
                Id = UserId
            };
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.Count > 0)
            {
                if (query.ContainsKey("UserId"))
                {
                    Guid userId = (Guid)query["UserId"];

                    UserId = userId;
                }
                else
                {
                    goalDetail = query["Goal"] as GoalModel;
                }
            }
        }
    }
}
