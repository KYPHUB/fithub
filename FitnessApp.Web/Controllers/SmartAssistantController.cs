using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FitnessApp.Web.Services;
using FitnessApp.Web.ViewModels;

namespace FitnessApp.Web.Controllers;

[Authorize(Roles = "Member")]
public class SmartAssistantController : Controller
{
    private readonly IAIService _aiService;

    public SmartAssistantController(IAIService aiService)
    {
        _aiService = aiService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new UserStatsViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Generate(UserStatsViewModel model, IFormFile? UserPhoto)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }

        var plan = await _aiService.GeneratePlanAsync(model);
        
        // Eğer kullanıcı bir fotoğraf yüklediyse, dosya adından numara çıkar
        if (UserPhoto != null && UserPhoto.Length > 0)
        {
            var fileName = Path.GetFileNameWithoutExtension(UserPhoto.FileName).ToLower();
            
            // before_1, before_2 vb. formatındaki dosya adlarından numara çıkar
            if (fileName.StartsWith("before_"))
            {
                var numPart = fileName.Replace("before_", "");
                if (int.TryParse(numPart, out int imageId) && imageId >= 1 && imageId <= 10)
                {
                    ViewBag.TransformationImage = $"/images/transformations/after_{imageId}.jpg";
                    ViewBag.BeforeImage = $"/images/transformations/before_{imageId}.jpg";
                }
            }
            // Sadece rakam ile başlayan dosyalar için de destek (1.jpg, 2.jpg vb.)
            else if (int.TryParse(fileName.Split('_')[0], out int simpleId) && simpleId >= 1 && simpleId <= 10)
            {
                ViewBag.TransformationImage = $"/images/transformations/after_{simpleId}.jpg";
                ViewBag.BeforeImage = $"/images/transformations/before_{simpleId}.jpg";
            }
        }
        
        return View("Result", plan); 
    }
}