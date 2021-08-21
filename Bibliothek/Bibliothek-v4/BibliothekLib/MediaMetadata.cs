using System;
using System.ComponentModel.DataAnnotations;

namespace BibliothekLib
{
    [MetadataType(typeof(MediaMetadata))]
    public partial class Media
    {
    }

    public partial class MediaMetadata
    {
        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Number")]
        public string Number { get; set; }

        [Display(Name = "Work")]
        public Work Work { get; set; }

        [Display(Name = "User")]
        public User User { get; set; }

    }
}
