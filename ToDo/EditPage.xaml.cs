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
            DataContext = MainViewModel.I(); 

        }


        private MainViewModel getViewModel()
        {
            return DataContext as MainViewModel;
        }

        private async void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var yesTag = loader.GetString("yesTag");
            var noTag = loader.GetString("noTag");
            var cancelNotification = loader.GetString("cancelNotification");
            var dialog = new MessageDialog(cancelNotification);
            dialog.Commands.Add(new UICommand { Label = yesTag, Id = 0 });
            dialog.Commands.Add(new UICommand { Label = noTag, Id = 1 });
            var result = await dialog.ShowAsync();

            if ((int)result.Id == 0)
            {
                this.Frame.GoBack();
            }
        }

        private async void Accept_Click(object sender, RoutedEventArgs e)
        {
            ToDoTask myTask = new ToDoTask(titleTextBox.Text, valueTextBox.Text, getViewModel().CurrentObject.id);
            if (getViewModel().CurrentObject.ownerId.Equals(myTask.ownerId))
            {
                getViewModel().updateTask(myTask);
                Frame.GoBack();
            }
              
            else {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                var warnDialog = loader.GetString("yesTag");
                var dialog = new MessageDialog(warnDialog);
                await dialog.ShowAsync();
            }
        }


        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var deleteWarnDialog = loader.GetString("deleteWarnDialog");
            var yesTag = loader.GetString("yesTag");
            var noTag = loader.GetString("noTag");
            var dialog = new MessageDialog(deleteWarnDialog);
            dialog.Commands.Add(new UICommand { Label = yesTag, Id = 1 });
            dialog.Commands.Add(new UICommand { Label = noTag, Id = 0 });
            var result = await dialog.ShowAsync();

            if ((int)result.Id == 0)
            {
                this.Frame.GoBack();
            }

            if ((int)result.Id == 1)
            {
                //delete post
                ToDoTask myTask = new ToDoTask(titleTextBox.Text, valueTextBox.Text, getViewModel().CurrentObject.id);
                await getViewModel().deleteTask(myTask);
                this.Frame.Navigate(typeof(MainPage));
            }

        }
    }
}
