using System.ComponentModel.DataAnnotations;

namespace CineMagic.Models
{
    public class MovieActor
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        [StringLength(50)]
        public string? Character { get; set; }
        public int? Order { get; set; }
        public Movie Movie { get; set; }
        public Actor Actor { get; set; }
    }
}
