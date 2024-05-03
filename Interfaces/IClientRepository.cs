using System.Threading.Tasks.Dataflow;
using reservations.Models;

namespace reservations.Interfaces
{
    public interface IClientRepository
    {
         long AddClient(Client client);
         IEnumerable<Client> GetAll();
         Client Find(long id);
         Client Find(string? firstName, string? lastName);
         Client Find(string? email);
         bool RemoveClient(long id);
         bool Update(Client client);
    }
}