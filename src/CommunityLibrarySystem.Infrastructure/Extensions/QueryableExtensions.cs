namespace CommunityLibrarySystem.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static (IEnumerable<T> Items, int Total) Paginar<T>(this IQueryable<T> query, int page, int pageSize)
        {
            var total = query.Count();
            var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return (items, total);
        }
    }
}
