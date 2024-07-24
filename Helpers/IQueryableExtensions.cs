using CineMagic.DTOs;

namespace CineMagic.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            return queryable.Skip((paginationDTO.page - 1) * paginationDTO.RecordsPerPage).Take(paginationDTO.RecordsPerPage);
        }
    }
}
