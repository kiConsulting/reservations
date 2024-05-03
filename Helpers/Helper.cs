using System.Threading.Tasks.Dataflow;
using reservations.Models;

namespace reservations.Helpers
{
    public class Helper
    {
         private readonly DataContext _context;

        public Helper(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<TimeOnly> GetAvailableTimes(DateOnly date)
        {
            List<TimeOnly> times = new List<TimeOnly>();
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            if (date.Day <= today.Day)
                return times;
            var booked = GetBookedTimes(date);
            var qry = _context.Availabilities.Where(w => w.AvailableDate == date);
            TimeOnly startTime = qry.Min(m => m.Start);
            TimeOnly endTime = qry.Max(m => m.End);
            for (TimeOnly currentTime = startTime; currentTime < endTime; currentTime = currentTime.AddMinutes(15))
            {
                if (!booked.Any(t => TimeOnly.FromDateTime(t.AppointmentStart) == currentTime))
                {
                    times.Add(currentTime);
                }
            }
            return times;
        }
        private IEnumerable<Appointment> GetBookedTimes(DateOnly date)
        {
            var qry = _context.Appointments.Where(w => w.AppointmentStart.Year == date.Year && w.AppointmentStart.Month == date.Month && w.AppointmentStart.Day == date.Day && (!w.IsConfirmed));
            return qry;
        }
    }
}