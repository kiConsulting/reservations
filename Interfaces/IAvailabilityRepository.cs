using reservations.Models;

namespace reservations.Interfaces
{
    public interface IAvailabilityRepository
    {
         long AddAvailability(Availability availability);
         IEnumerable<Availability> GetAvailabilities();
         IEnumerable<TimeOnly> GetAvailableTimes(DateOnly date);
         Availability Find(long Id);
         IEnumerable<Availability> FindAvailability(long providerId, DateOnly date);
         bool RemoveAvailability(long Id);
         bool RemoveAvailability(long providerId, DateOnly date);
         bool Update(Availability availability);
    }
}