using Microsoft.EntityFrameworkCore;
using PhoneBook.Data;
using PhoneBook.Models;
using PhoneBook.ViewModels;

namespace PhoneBook.Services;

/// <summary>
/// Реализация <see cref="IContactService"/> поверх Entity Framework Core.
/// Все операции асинхронные — не блокируют поток запроса.
/// </summary>
public class ContactService : IContactService
{
    private readonly AppDbContext _db;

    public ContactService(AppDbContext db)
    {
        _db = db;
    }

    // ── READ ────────────────────────────────────────────────────────────────

    public async Task<IEnumerable<Contact>> GetAllAsync() =>
        await _db.Contacts.OrderBy(c => c.Name).ToListAsync();

    public async Task<IEnumerable<Contact>> SearchAsync(string name) =>
        await _db.Contacts
                 .Where(c => c.Name.Contains(name))
                 .OrderBy(c => c.Name)
                 .ToListAsync();

    public async Task<Contact?> GetByIdAsync(int id) =>
        await _db.Contacts.FindAsync(id);

    // ── CREATE ───────────────────────────────────────────────────────────────

    public async Task<Contact> AddAsync(ContactViewModel vm)
    {
        var contact = new Contact
        {
            Name        = vm.FullName,
            PhoneNumber = vm.PhoneNumber.Trim(),
            Email       = vm.Email.Trim()
        };
        _db.Contacts.Add(contact);
        await _db.SaveChangesAsync();
        return contact;
    }

    // ── UPDATE ───────────────────────────────────────────────────────────────

    public async Task<bool> UpdateAsync(int id, ContactViewModel vm)
    {
        var contact = await _db.Contacts.FindAsync(id);
        if (contact is null) return false;

        contact.Name        = vm.FullName;
        contact.PhoneNumber = vm.PhoneNumber.Trim();
        contact.Email       = vm.Email.Trim();

        await _db.SaveChangesAsync();
        return true;
    }

    // ── DELETE ───────────────────────────────────────────────────────────────

    public async Task<bool> DeleteAsync(int id)
    {
        var contact = await _db.Contacts.FindAsync(id);
        if (contact is null) return false;

        _db.Contacts.Remove(contact);
        await _db.SaveChangesAsync();
        return true;
    }
}
