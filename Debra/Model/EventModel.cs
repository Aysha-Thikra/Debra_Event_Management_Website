using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Debra.Model
{
    public class EventModel
    {
        public string EventID { get; set; }
        public string EventName { get; set; }
        public string EventPoster { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; } 
    }


}