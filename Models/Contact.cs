using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models;

public class Contact
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Имя обязательно")]
    [StringLength(100, ErrorMessage = "Имя не должно превышать 100 символов")]
    [Display(Name = "Имя")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Номер телефона обязателен")]
    [Phone(ErrorMessage = "Неверный формат номера телефона")]
    [Display(Name = "Телефон")]
    public string PhoneNumber { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Неверный формат email")]
    [Display(Name = "Email")]
    public string? Email { get; set; }
}
