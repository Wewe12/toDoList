using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Entity;
using Windows.Storage;

namespace ToDo.ViewModels
{
    class MainViewModel : ViewModel
    {
        //CONST
        private const string REST_BASE_URL = "http://windowsphoneuam.azurewebsites.net/";
        private const string REST_PATH = "api/todotasks/";

        private const string LOCAL_SETTINGS_TAG = "Owner";

        private string ownerId { get; set; }
        public string OwnerId { get { return ownerId; } set { ownerId = value; } }


        private ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
       

       



        private static MainViewModel instance { get; set; }


        public static MainViewModel I()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }
            return instance;
        }



        private ObservableCollection<ToDoTask> itemsCollection;
        public ObservableCollection<ToDoTask> ItemsCollection
        {
            get
            {
                return itemsCollection;
            }
            set
            {
                itemsCollection = value;
                OnPropertyChanged("ItemsCollection");
            }
        }


        
















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
