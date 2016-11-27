using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebDiary.DAL.Entities
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    }
}
