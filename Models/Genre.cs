using CineMagic.Validators;
using System.ComponentModel.DataAnnotations;

namespace CineMagic.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [FirstLetterUpperCaseValidator(ErrorMessage = "First letter should be uppercase")]
        public string Name { get; set; }
    }
}
