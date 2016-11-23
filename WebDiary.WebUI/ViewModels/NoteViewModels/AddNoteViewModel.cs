using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebDiary.ViewModels.NoteViewModels
{
    public class AddNoteViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public bool Privacy { get; set; }

        public string TagsString { get; set; }
    }
}