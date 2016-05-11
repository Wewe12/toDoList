using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.ViewModels;

namespace ToDo.Entity
{
    class ToDoTask
    {
        public string id { get; set; }
        public string title { get; set; }
        public string value { get; set; }
        public string ownerId { get; set; }
        public string createdAt { get; set; }

        public ToDoTask ( string title, string value)
        {
            this.title = title;
            this.value = value;
            this.ownerId = MainViewModel.I().OwnerId;

        }
    }
}
