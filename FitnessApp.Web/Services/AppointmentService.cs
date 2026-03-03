using FitnessApp.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Web.Services;

public class AppointmentService
{
    private readonly ApplicationDbContext _context;

    public AppointmentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TimeSpan>> GetAvailableSlotsAsync(int trainerId, DateTime date, int durationMinutes)
    {
        if (date.Date < DateTime.Today)
        {
            return new List<TimeSpan>();
        }

        var availability = await _context.TrainerAvailabilities
            .FirstOrDefaultAsync(a => a.TrainerId == trainerId && a.DayOfWeek == date.DayOfWeek);

        if (availability == null)
        {
            return new List<TimeSpan>();
        }

        var existingAppointments = await _context.Appointments
            .Where(a => a.TrainerId == trainerId && a.Date.Date == date.Date && 
                        a.Status != AppointmentStatus.Cancelled && 
                        a.Status != AppointmentStatus.Rejected)
            .ToListAsync();

        var slots = new List<TimeSpan>();
        var currentTime = availability.StartTime;
        var endTime = availability.EndTime;

        while (currentTime.Add(TimeSpan.FromMinutes(durationMinutes)) <= endTime)
        {
            var slotEnd = currentTime.Add(TimeSpan.FromMinutes(durationMinutes));

            if (date.Date == DateTime.Today && currentTime < DateTime.Now.TimeOfDay)
            {
                currentTime = currentTime.Add(TimeSpan.FromMinutes(30));
                continue;
            }

            bool isConflict = existingAppointments.Any(a =>
                (currentTime >= a.StartTime && currentTime < a.EndTime) ||
                (slotEnd > a.StartTime && slotEnd <= a.EndTime) ||
                (currentTime <= a.StartTime && slotEnd >= a.EndTime)
            );

            if (!isConflict)
            {
                slots.Add(currentTime);
            }

            currentTime = currentTime.Add(TimeSpan.FromMinutes(30));
        }

        return slots;
    }
}
