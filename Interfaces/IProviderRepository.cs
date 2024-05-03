using System.Threading.Tasks.Dataflow;
using reservations.Models;

namespace reservations.Interfaces
{
    public interface IProviderRepository
    {
         
         long AddProvider(Provider provider);
         IEnumerable<Provider> GetAll();
         IEnumerable<Provider> GetBySpecialty(string? specialty);
         Provider Find(long Id);
         Provider Find(string? FirstName, string? LastName);
         bool RemoveProvider(long Id);
         bool Update(Provider provider);
    }
}