using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TodoWebApp.Models
{
    [Table("TodoTable")]
    public class Todo
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Have you done it?")]
        public bool IsDone { get; set; } = false;

        [Required]
        public DateTime DateAdded { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(128)]
        public string UserId { get; set; }
    }
}