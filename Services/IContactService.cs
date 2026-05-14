using PhoneBook.Models;
using PhoneBook.ViewModels;

namespace PhoneBook.Services;

/// <summary>
/// Сервис для работы с контактами (полный CRUD).
/// </summary>
public interface IContactService
{
    /// <summary>Вернуть все контакты.</summary>
    Task<IEnumerable<Contact>> GetAllAsync();

    /// <summary>Найти контакты по имени (регистронезависимо).</summary>
    Task<IEnumerable<Contact>> SearchAsync(string name);

    /// <summary>Получить контакт по Id (или null).</summary>
    Task<Contact?> GetByIdAsync(int id);

    /// <summary>Добавить контакт из ViewModel.</summary>
    Task<Contact> AddAsync(ContactViewModel viewModel);

    /// <summary>Обновить существующий контакт. Возвращает false, если не найден.</summary>
    Task<bool> UpdateAsync(int id, ContactViewModel viewModel);

    /// <summary>Удалить контакт по Id. Возвращает false, если не найден.</summary>
    Task<bool> DeleteAsync(int id);
}
