using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Web.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Ad Soyad zorunludur.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email adresi zorunludur.")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre zorunludur.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Şifre Tekrar")]
    [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
