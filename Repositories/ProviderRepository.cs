using System.Threading.Tasks.Dataflow;
using reservations.Interfaces;
using reservations.Models;

namespace reservations.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly DataContext _context;

        public ProviderRepository(DataContext context)
        {
            _context = context;
        }
         
         public long AddProvider(Provider provider)
         {
            provider.CreatedDate = DateTime.Now;
            _context.Providers.Add(provider);
            _context.SaveChanges();
            return _context.Providers.Max(m => m.Id);
         }
         public IEnumerable<Provider> GetAll()
         {
            return _context.Providers.ToList();
         }
         public IEnumerable<Provider> GetBySpecialty(string? specialty)
         {
            return _context.Providers.Where(w => w.Specialty == specialty).ToList();
         }
         public Provider Find(long Id)
         {
            return _context.Providers.FirstOrDefault(t => t.Id == Id);
         }
         public Provider Find(string? FirstName, string? LastName)
         {
            return _context.Providers.FirstOrDefault(t => t.FirstName == FirstName && t.LastName == LastName);
         }
         public bool RemoveProvider(long Id)
         {
            try
            {
                var p = _context.Providers.FirstOrDefault(p => p.Id == Id);
                if (p == null)
                    return false;
                _context.Providers.Remove(p);
                _context.SaveChanges();
                return true;
            } catch(Exception ex) {
                return false;
            }
         }
         public bool Update(Provider provider)
         {
            try
            {
                _context.Providers.Update(provider);
                _context.SaveChanges();
                return true;
            } catch(Exception ex) {
                return false;
            }
         }
    }
}