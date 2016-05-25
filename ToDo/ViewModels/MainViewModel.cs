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
        //CONST REST
        private const string REST_BASE_URL = "http://windowsphoneuam.azurewebsites.net/";
        private const string REST_PATH = "api/todotasks/";
        private const string OWNER_SEARCH_PATH = "?OwnerId=";

        //CONST LOCAL SETTINGS
        private const string LOCAL_SETTINGS_TAG = "Owner";

        private string ownerId { get; set; }
        public string OwnerId { get { return ownerId; } set { ownerId = value; } }
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
            set { itemsCollection = value; OnPropertyChanged("ItemsCollection");}
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

        //REST

        //GET
        public async Task getTasks()
        {    
            using (HttpClient client = new HttpClient())
            {
                //var result = await client.GetAsync(REST_BASE_URL + "/" + REST_PATH + "?OwnerId=" + ownerId);
                var result = await client.GetAsync(REST_BASE_URL + "/" + REST_PATH );
                var items = await result.Content.ReadAsStringAsync();
                itemsCollection =  JsonConvert.DeserializeObject<ObservableCollection<ToDoTask>>(items);
            }
        }
        //GET 
        public async Task getOwnerTasks()
        {
      
            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetAsync(REST_BASE_URL + "/" + REST_PATH + OWNER_SEARCH_PATH + ownerId);
                var items = await result.Content.ReadAsStringAsync();
                itemsCollection = JsonConvert.DeserializeObject<ObservableCollection<ToDoTask>>(items);
            }
        }

        //POST
        public async void postTask(ToDoTask myTask)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(REST_BASE_URL);
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, REST_PATH);
                request.Content = new StringContent(myTask.SerializeToDoTask(), Encoding.UTF8, "application/json");
                await client.SendAsync(request);
            }
        }


        //PUT
        public async void updateTask(ToDoTask myTask)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(REST_BASE_URL);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, REST_PATH + myTask.id);
                request.Content = new StringContent(myTask.SerializeToDoTask(), Encoding.UTF8, "application/json");
                await client.SendAsync(request);
            }
        }


        //DELETE
        public async Task deleteTask(ToDoTask myTask)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(REST_BASE_URL);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, REST_PATH + myTask.id);
                await client.SendAsync(request);
            }
        }

    }
}
