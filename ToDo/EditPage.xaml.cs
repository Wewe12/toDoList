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
            var dialog = new MessageDialog("Are you sure you want to cancel?");
            dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
            dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });
            var result = await dialog.ShowAsync();

            if ((int)result.Id == 0)
            {
                this.Frame.GoBack();
            }
        }

        // TO DO problem with sending put
        private async void Accept_Click(object sender, RoutedEventArgs e)
        {
            ToDoTask myTask = new ToDoTask(titleTextBox.Text, valueTextBox.Text, getViewModel().CurrentObject.id);
            if (getViewModel().CurrentObject.ownerId.Equals(myTask.ownerId))
            {
                getViewModel().updateTask(myTask);
                Frame.GoBack();
            }
              
            else {
                var dialog = new MessageDialog("That's not you task. You don't have right to modify them");
                await dialog.ShowAsync();
            }
        }


        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog("Are you sure you want to delete this task?");
            dialog.Commands.Add(new UICommand { Label = "Yes", Id = 1 });
            dialog.Commands.Add(new UICommand { Label = "No", Id = 0 });
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
