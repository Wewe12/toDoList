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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ToDo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = MainViewModel.I();
            
            getViewModel().loadLocalSettings();

            //getViewModel().getOwnerTasks();
           // getViewModel().getTasks();
            
           

        }
    private MainViewModel getViewModel()
        {
            return DataContext as MainViewModel;
        }

        private async void Owner_Task_Click(object sender, RoutedEventArgs e)
        {
            progressRing.IsActive = true;
            await getViewModel().getOwnerTasks();
            progressRing.IsActive = false;
        }
        private async void All_Task_Click(object sender, RoutedEventArgs e)
        {
            progressRing.IsActive = true;
            await getViewModel().getTasks();
            progressRing.IsActive = false;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
           
            this.Frame.Navigate(typeof(AddPage));
        }


        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            getViewModel().removeLocalSettings();
            this.Frame.GoBack();
        }
        private void ListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox1.SelectedItem == null)
            {
                return;
            }
            else
            {
                getViewModel().CurrentObject = (ToDoTask)ListBox1.SelectedItem;
               var nere =  getViewModel().CurrentObject.title;

                Frame.Navigate(typeof(EditPage));
            }
        }




    }
}
