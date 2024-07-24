using CineMagic.Validators;
using System.ComponentModel.DataAnnotations;

namespace CineMagic.DTOs
{
    public class GenreCreationDTO
    {
        [Required]
        [StringLength(50)]
        [FirstLetterUpperCaseValidator(ErrorMessage = "First letter should be uppercase")]
        public string Name { get; set; }
    }
}
