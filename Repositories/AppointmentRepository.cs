using System.Threading.Tasks.Dataflow;
using reservations.Interfaces;
using reservations.Models;

namespace reservations.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DataContext _context;

        public AppointmentRepository(DataContext context)
        {
            _context = context;
        }
         public long AddAppointment(Appointment appointment)
         {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            return _context.Appointments.Max(m => m.Id);
         }
         public IEnumerable<Appointment> GetAll()
         {
            return _context.Appointments.ToList();
         }
         public IEnumerable<Appointment> GetBySpecialty(string? specialty)
         {
            if (!string.IsNullOrEmpty(specialty))
            {
                return _context.Appointments
                  .Join(
                    _context.Providers,
                    appointment => appointment.ProviderId,
                    provider => provider.Id,
                    (appointment, provider) => new
                    {
                        appointment,
                        provider.Specialty
                    }
                  ).Where(w => w.Specialty == specialty)
                  .Select(a => a.appointment).ToList();
            }
            else
            {
                return _context.Appointments.ToList();
            }
         }
         public Appointment Find(long Id)
         {
            return _context.Appointments.FirstOrDefault(t => t.Id == Id);
         }
         public Appointment FindByProvider(long providerId, DateTime dateTime)
         {
            return _context.Appointments.FirstOrDefault(t => t.ProviderId == providerId && t.AppointmentStart == dateTime);
         }
         public Appointment FindByClient(long clientId, DateTime dateTime)
         {
            return _context.Appointments.FirstOrDefault(t => t.ClientId == clientId && t.AppointmentStart == dateTime);
         }
         public Appointment FindByProviderClient(long providerId, long clientId, DateTime dateTime)
         {
            return _context.Appointments.FirstOrDefault(t => t.ProviderId == providerId && t.ClientId == clientId && t.AppointmentStart == dateTime);
         }
         public IEnumerable<Appointment> FindByDate(DateTime dateTime)
         {
            return _context.Appointments.Where(t => t.AppointmentStart == dateTime).ToList();
         }
         public bool RemoveAppointment(long Id)
         {
            try
            {
              var app = _context.Appointments.First(t => t.Id == Id);
                _context.Appointments.Remove(app);
                _context.SaveChanges();
                return true;
            } catch(Exception ex) {
                return false;
            }
         }
         public bool Update(Appointment appointment)
         {
            try
            {
                _context.Appointments.Update(appointment);
                _context.SaveChanges();
                return true;
            } catch (Exception ex) {
                return false;
            }
         }
         public bool Confirm(long appointmentId)
         {
            try 
            {
                var apt = _context.Appointments.First(t => t.Id == appointmentId);
                apt.IsConfirmed = true;
                _context.Appointments.Update(apt);
                _context.SaveChanges();
                return true;
            } catch (Exception ex) {
                return false;
            }

         }
    }
}