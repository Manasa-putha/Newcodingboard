using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookEventread.Models
{
    public class EventModel
    {
        public enum typeOfEvent
        {
            Public,
            Private
        }

        public string UserId { get; set; }
        [Key]
        public int EventId { get; set; }

        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Location { get; set; }

        [DataType(DataType.Time)]
        [Required]
        public string StartTime { get; set; }
        public typeOfEvent Type { get; set; } = typeOfEvent.Public;

        [Display(Name = "Duration In Hours")]
        [Range(0, 4)]
        public int Duration { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
        [MaxLength(500)]
        [Display(Name = "Other Details")]
        public string OtherDetails { get; set; }
        public string Invites { get; set; }
    }
}