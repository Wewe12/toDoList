using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ToDo.Entity;
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
    public sealed partial class EditPage : Page
    {
        public EditPage()
        {
            this.InitializeComponent();
            DataContext = new MainViewModel();
        }


        private MainViewModel getViewModel()
        {
            return DataContext as MainViewModel;
        }

        private async void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog("Are you sure you want to cancel?");
            dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
            dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });
            var result = await dialog.ShowAsync();

            if ((int)result.Id == 0)
            {
                this.Frame.GoBack();
            }
        }


        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            ToDoTask myTask = new ToDoTask(titleTextBox.Text, valueTextBox.Text);
            getViewModel().updateTask(myTask);
            this.Frame.GoBack();
        }
    }
}
