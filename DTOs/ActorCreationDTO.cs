namespace CineMagic.DTOs
{
    public class ActorCreationDTO
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Biography { get; set; }
        public IFormFile? picture { get; set; }
    }
}
