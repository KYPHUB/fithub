using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitnessApp.Web.Data;

namespace FitnessApp.Web.Controllers;

public class TrainersController : Controller
{
    private readonly ApplicationDbContext _context;

    public TrainersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /Trainers veya /Trainers?serviceId=1
    public async Task<IActionResult> Index(int? serviceId)
    {
        var query = _context.Trainers.Include(t => t.Specialties).AsQueryable();
        
        // Eğer serviceId varsa, sadece o hizmeti veren antrenörleri filtrele
        if (serviceId.HasValue)
        {
            query = query.Where(t => t.Specialties.Any(s => s.Id == serviceId.Value));
            
            // Hizmet adını ViewBag'e ekle
            var service = await _context.Services.FindAsync(serviceId.Value);
            ViewBag.FilteredServiceName = service?.Name;
            ViewBag.FilteredServiceId = serviceId.Value;
        }
        
        var trainers = await query.ToListAsync();
        return View(trainers);
    }

    public async Task<IActionResult> Details(int? id, int? serviceId)
    {
        if (id == null) return NotFound();

        var trainer = await _context.Trainers
            .Include(t => t.Specialties)
            .Include(t => t.Availabilities)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (trainer == null) return NotFound();

        // Seçilen hizmet ID'sini ViewBag'e aktar
        ViewBag.SelectedServiceId = serviceId ?? trainer.Specialties.FirstOrDefault()?.Id;

        return View(trainer);
    }
}
