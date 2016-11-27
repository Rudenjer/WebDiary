using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebDiary.DAL.Entities
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public bool Privacy { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    }
}
