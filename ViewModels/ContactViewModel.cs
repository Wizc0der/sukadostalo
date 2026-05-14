using System.ComponentModel.DataAnnotations;

namespace PhoneBook.ViewModels;

/// <summary>
/// ViewModel для формы создания / редактирования контакта.
/// Телефон и Email — обязательные поля (по заданию).
/// </summary>
public class ContactViewModel
{
    // ── ФИО ─────────────────────────────────────────────────────────────────

    [Required(ErrorMessage = "Фамилия обязательна")]
    [StringLength(50, ErrorMessage = "Не более 50 символов")]
    [Display(Name = "Фамилия")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Имя обязательно")]
    [StringLength(50, ErrorMessage = "Не более 50 символов")]
    [Display(Name = "Имя")]
    public string FirstName { get; set; } = string.Empty;

    [StringLength(50, ErrorMessage = "Не более 50 символов")]
    [Display(Name = "Отчество")]
    public string? MiddleName { get; set; }

    // ── Контактные данные ────────────────────────────────────────────────────

    [Required(ErrorMessage = "Номер телефона обязателен")]
    [Phone(ErrorMessage = "Неверный формат номера телефона")]
    [Display(Name = "Телефон")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Неверный формат email")]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    // ── Вспомогательное свойство ─────────────────────────────────────────────

    /// <summary>Полное ФИО одной строкой.</summary>
    public string FullName =>
        string.Join(" ", new[] { LastName, FirstName, MiddleName }
            .Where(s => !string.IsNullOrWhiteSpace(s)));
}
