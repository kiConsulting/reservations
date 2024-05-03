using System.Threading.Tasks.Dataflow;
using reservations.Helpers;
using reservations.Interfaces;
using reservations.Models;

namespace reservations.Repositories
{
    public class AvailabilityRepository : IAvailabilityRepository
    {
        private readonly DataContext _context;

        public AvailabilityRepository(DataContext context)
        {
            _context = context;
        }
        public long AddAvailability(Availability availability) 
        {
            _context.Availabilities.Add(availability);
            _context.SaveChanges();
            return _context.Availabilities.Max(m => m.Id);
        }
        public IEnumerable<TimeOnly> GetAvailableTimes(DateOnly date)
        {
            var helper = new Helper(_context);

            var times = helper.GetAvailableTimes(date);
            return times;
        }
        public  IEnumerable<Availability> GetAvailabilities()
        {
            return _context.Availabilities.ToList();
        }
        public Availability Find(long id)
        {
            return _context.Availabilities.First(t => t.Id == id);
        }
        public  IEnumerable<Availability> FindAvailability(long providerId, DateOnly date)
        {
            return _context.Availabilities.Where(w => w.ProviderId == providerId && w.AvailableDate == date).ToList();
        }
        public  bool RemoveAvailability(long Id)
        {
            try
            {
                var a = _context.Availabilities.First(t => t.Id == Id);
                _context.Availabilities.Remove(a);
                _context.SaveChanges();
                return true;
            } catch(Exception ex) {
                return false;
            }
        }
        public  bool RemoveAvailability(long providerId, DateOnly date)
        {
            try
            {
                var availabilities = _context.Availabilities.Where(t => t.ProviderId == providerId && t.AvailableDate == date);
                foreach(var availability in availabilities)
                {
                    _context.Availabilities.Remove(availability);
                }
                _context.SaveChanges();
                return true;
            } catch(Exception ex) {
                return false;
            }
        }
        public  bool Update(Availability availability)
        {
            try
            {
                _context.Availabilities.Update(availability);
                _context.SaveChanges();
                return true;
            } catch(Exception ex) {
                return false;
            }
        }
    }
}