using FinanceMAUI.Services;
using FinanceMAUI.ViewModels;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Tests.ViewModels
{
    public class UserDetailViewModelTests
    {
        [Fact]
        public async Task UserDetailWithId_IsInitialized_GetUserIsCalled()
        {
            // Arrange
            var id = Guid.NewGuid();

            var userService = Substitute.For<IUserService>();
            var navigationService = Substitute.For<INavigationService>();
            var dialogService = Substitute.For<IDialogService>();

            var sut = new UserDetailViewModel(userService, navigationService, dialogService);
            sut.Id = id;

            // Act
            await sut.LoadAsync();

            // Assert
            await userService
                .Received(1)
                .GetUser(id);
        }

        [Fact]
        public async Task UserDetailWithGuidEmptyId_IsInitialized_GetUserIsNotCalled()
        {
            // Arrange
            var id = Guid.Empty;

            var eventService = Substitute.For<IUserService>();
            var navigationService = Substitute.For<INavigationService>();
            var dialogService = Substitute.For<IDialogService>();

            var sut = new UserDetailViewModel(eventService, navigationService, dialogService);
            sut.Id = id;

            // Act
            await sut.LoadAsync();

            // Assert
            await eventService
                .DidNotReceive()
                .GetUser(Arg.Any<Guid>());
        }
    }
}
