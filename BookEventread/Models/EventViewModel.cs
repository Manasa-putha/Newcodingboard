using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookEventread.Models
{
    public class EventViewModel
    {

        public EventModel eventModel { get; set; }
        public List<CommentModel> Comments { get; set; }
    }
}