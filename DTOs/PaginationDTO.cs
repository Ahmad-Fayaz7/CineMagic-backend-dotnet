namespace CineMagic.DTOs
{
    public class PaginationDTO
    {
        public int page { get; set; } = 1;
        private int recordsPerPage = 10;
        // Default page size
        private readonly int pageSize = 50;
        public int RecordsPerPage
        {
            get { return recordsPerPage; }
            set { recordsPerPage = (value > pageSize) ? pageSize : value; }
        }
    }
}
