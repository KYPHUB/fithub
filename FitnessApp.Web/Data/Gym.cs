using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Web.Data;

public class Gym
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Salon adı zorunludur.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Adres zorunludur.")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Telefon zorunludur.")]
    public string Phone { get; set; } = string.Empty;

    public string OpeningHours { get; set; } = string.Empty; // Örn: 09:00 - 22:00
}
