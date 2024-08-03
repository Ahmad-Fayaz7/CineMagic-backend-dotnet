namespace CineMagic.DTOs
{
    public class MovieFilterDTO
    {
        public int Page { get; set; }
        public int RecordsPerPage { get; set; }

        public PaginationDTO paginationDTO
        {
            get { return new PaginationDTO() { page = Page, RecordsPerPage = RecordsPerPage }; }
        }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public bool InTheaters { get; set; }
        public bool UpcomingReleases { get; set; }
    }

}
