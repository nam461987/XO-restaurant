using XO.Entities.Models;
using XO.Repository.Interfaces;
using System.Threading.Tasks;

namespace XO.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly XOContext _context;

        public UnitOfWork(XOContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
