using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ToDo.Entity;
using Windows.Storage;

namespace ToDo.ViewModels
{
    class MainViewModel : ViewModel
    {
    
        //CONST LOCAL SETTINGS
        private const string LOCAL_SETTINGS_TAG = "Owner";

        private string ownerId { get; set; }
        public string OwnerId { get { return ownerId; } set { ownerId = value; } }

        private bool allTask { get; set; } = false;
        public bool AllTask { get { return allTask; } set { allTask = value; } }

        private ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        private static MainViewModel instance { get; set; }

        private ToDoTask currentObject { get; set; }
        public ToDoTask CurrentObject
        {
            get { return currentObject; }
            set { currentObject = value; OnPropertyChanged("CurrentObject"); }
        }

        private ObservableCollection<ToDoTask> itemsCollection;
        public ObservableCollection<ToDoTask> ItemsCollection
        {
            get { return itemsCollection;}
            set { itemsCollection = value;
                OnPropertyChanged("ItemsCollection");
            }
        }

        public static MainViewModel I()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }
            return instance;
        }

        public MainViewModel()
        {

        }

        //Local setting

        public void saveLocalSettings(String username)
        {
            localSettings.Values[LOCAL_SETTINGS_TAG] = username;
        }

        public void loadLocalSettings()
        {
            Object value = localSettings.Values[LOCAL_SETTINGS_TAG];
            if (value == null)
            {
                ownerId = ""; 
            }
            else
            {
                ownerId = value.ToString();
            }
        }

        public void removeLocalSettings()
        {
            localSettings.Values.Remove(LOCAL_SETTINGS_TAG);
            ownerId = "";
        }

    }
}
