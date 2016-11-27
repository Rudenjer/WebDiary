using System.ComponentModel.DataAnnotations;

namespace WebDiary.ViewModels.NoteViewModels
{
    public class NoteUpdateViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public bool Privacy { get; set; }

        public string TagsString { get; set; }
    }
}