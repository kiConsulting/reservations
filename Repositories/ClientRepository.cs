using System.Threading.Tasks.Dataflow;
using reservations.Interfaces;
using reservations.Models;

namespace reservations.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _context;

        public ClientRepository(DataContext context)
        {
            _context = context;
        }
        public long AddClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
            return _context.Clients.Max(m => m.Id);
        }
        public IEnumerable<Client> GetAll()
        {
            return _context.Clients.ToList();
        }
        public Client Find(long Id)
        {
            return _context.Clients.FirstOrDefault(t => t.Id == Id);
        }
        public Client Find(string? FirstName, string? LastName)
        {
            return _context.Clients.FirstOrDefault(t => t.FirstName == FirstName && t.LastName == LastName);
        }
        public Client Find(string? Email)
        {
            return _context.Clients.FirstOrDefault(t => t.Email == Email);
        }
        public bool RemoveClient(long Id)
        {
            try 
            {
                var c = _context.Clients.First(t => t.Id == Id);
                _context.Clients.Remove(c);
                _context.SaveChanges();
                return true;
            } catch(Exception ex) {
                return false;
            }
        }
        public bool Update(Client client)
        {
            try 
            {
                _context.Clients.Update(client);
                _context.SaveChanges();
                return true;
            } catch(Exception ex) {
                return false;
            }
        }
    }
}