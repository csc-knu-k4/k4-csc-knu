using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.PageModels
{
    public partial class RegistrationPageVM : ObservableObject
    {

        public RegistrationPageVM()
        {

        }

        [RelayCommand]
        public Task LogIn()
        {
            try
            {
                return Shell.Current.GoToAsync("///login", true);
            }
            catch(Exception ex)
            {
                return AppShell.DisplaySnackbarAsync(ex.Message);
            }
        }

    }
}
