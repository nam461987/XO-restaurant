using XO.Business.Interfaces.Paginated;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace XO.Business.Paginated
{
    public static class PaginatedListExtension
    {
        public static async Task<IPaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize)
        {
            var countTask = source.CountAsync();
            var itemsTask = source.Skip((pageIndex) * pageSize).Take(pageSize).ToListAsync();
            await Task.WhenAll(countTask, itemsTask);
            IPaginatedList<T> result = new PaginatedList<T>(itemsTask.Result, countTask.Result, pageIndex, pageSize);
            return result;
        }
    }
}
