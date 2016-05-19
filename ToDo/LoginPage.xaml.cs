using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ToDo.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ToDo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
            DataContext = MainViewModel.I();
        }
        private MainViewModel getViewModel()
        {
            return DataContext as MainViewModel;
        }

        private async void onClickLoginButton(object sender, RoutedEventArgs e)
        {
            if (usernameTextBox.Text != "")
            {
                getViewModel().saveLocalSettings(usernameTextBox.Text);
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                MessageDialog error = new MessageDialog("Username cannot be empty");
                await error.ShowAsync();
            }
           
        }
        private async void onClickAboutButton(object sender, RoutedEventArgs e)
        {
            MessageDialog aboutDialog = new MessageDialog(" ");
            await aboutDialog.ShowAsync();

        }
    }
}
