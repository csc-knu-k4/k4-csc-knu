using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.PageModels
{
    public partial class LoginPageVM : ObservableObject
    {


        #region ctor
        public LoginPageVM()
        {

        }
        #endregion

        #region Methods

        [RelayCommand]
        public async Task Login()
        {
            await Task.Delay(1000);
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
