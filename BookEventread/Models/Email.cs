using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookEventread.Models
{
    public class Email
    {

        [Key]
        public int inviteId { get; set; }

        [Required]
        public int eventId { get; set; }

        [Required]

        public string emailId { get; set; }
    }
}