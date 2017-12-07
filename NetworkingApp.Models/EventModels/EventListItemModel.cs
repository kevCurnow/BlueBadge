using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingApp.Models.EventModels
{
    public class EventListItemModel
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
    }
}
