using System.Threading.Tasks.Dataflow;
using reservations.Models;

namespace reservations.Interfaces
{
    public interface IAppointmentRepository
    {
          
         long AddAppointment(Appointment appointment);
         IEnumerable<Appointment> GetAll();
         IEnumerable<Appointment> GetBySpecialty(string? specialty);
         Appointment Find(long Id);
         Appointment FindByProvider(long providerId, DateTime dateTime);
         Appointment FindByClient(long clientId, DateTime dateTime);
         Appointment FindByProviderClient(long providerId, long clientId, DateTime dateTime);
         IEnumerable<Appointment> FindByDate(DateTime dateTime);
         bool RemoveAppointment(long Id);
         bool Update(Appointment appointment);
         bool Confirm(long appointmentId);
    }
}