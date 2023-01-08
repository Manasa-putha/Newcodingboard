using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookEventread.Models
{
    public class CommentModel
    {
        [Key]
        public int commentId { get; set; }
        public int EventId { get; set; }

        public string Message { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}