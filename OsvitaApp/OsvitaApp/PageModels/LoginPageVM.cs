using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OsvitaApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.PageModels
{
    public partial class LoginPageVM : ObservableObject
    {
        #region Properties

        private readonly IAccountService _authService;

        [ObservableProperty] private string _email = "admin@gmail.com";
        [ObservableProperty] private string _password = "Qwerty_1";


        #endregion

        #region ctor
        public LoginPageVM(IAccountService authService)
        {
            _authService = authService;
        }
        #endregion

        #region Methods

        [RelayCommand]
        public async Task Login()
        {
            var authResult = await _authService.LoginAsync(Email, Password);
            if(authResult.IsSuccess)
            {
                await Shell.Current.GoToAsync("///subjects", true);
            }
            else
            {
                await AppShell.DisplaySnackbarAsync(authResult.ErrorMessage);
            }
        }

        [RelayCommand]
        public async Task ContinueWithGoogle()
        {
            await Task.Delay(1000);
        }

        [RelayCommand]
        public  Task CreateAccount()
        {
            try
            {
                return Shell.Current.GoToAsync("///registration", true);
            }
            catch(Exception ex)
            {
                return AppShell.DisplaySnackbarAsync(ex.Message);
            }
        }


        #endregion


    }
}
