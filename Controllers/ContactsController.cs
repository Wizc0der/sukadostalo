using Microsoft.AspNetCore.Mvc;
using PhoneBook.Services;
using PhoneBook.ViewModels;

namespace PhoneBook.Controllers;

public class ContactsController : Controller
{
    private readonly IContactService _contactService;

    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
    }

    // ── INDEX ────────────────────────────────────────────────────────────────
    // GET /Contacts
    public async Task<IActionResult> Index()
    {
        var contacts = await _contactService.GetAllAsync();
        if (TempData["Message"] is string msg)
            ViewBag.Message = msg;
        return View(contacts);
    }

    // ── SEARCH ───────────────────────────────────────────────────────────────
    // GET /Contacts/Search/{name}
    [HttpGet("Contacts/Search/{name?}")]
    public async Task<IActionResult> Search(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return RedirectToAction(nameof(Index));

        var results = await _contactService.SearchAsync(name);
        ViewBag.SearchName = name;
        ViewBag.Message    = $"Поиск «{name}»: найдено {results.Count()} контакт(ов).";
        return View("Index", results);
    }

    // ── CREATE ───────────────────────────────────────────────────────────────
    // GET /Contacts/Create
    public IActionResult Create() => View(new ContactViewModel());

    // POST /Contacts/Create
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ContactViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Message     = "⚠ Исправьте ошибки в форме.";
            ViewBag.MessageType = "error";
            return View(vm);
        }
        var added = await _contactService.AddAsync(vm);
        TempData["Message"] = $"✓ Контакт «{added.Name}» добавлен!";
        return RedirectToAction(nameof(Index));
    }

    // ── EDIT ─────────────────────────────────────────────────────────────────
    // GET /Contacts/Edit/{id}
    public async Task<IActionResult> Edit(int id)
    {
        var contact = await _contactService.GetByIdAsync(id);
        if (contact is null) return NotFound();

        // Разбиваем Name обратно в поля ViewModel (Фамилия Имя [Отчество])
        var parts = contact.Name.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
        var vm = new ContactViewModel
        {
            LastName    = parts.ElementAtOrDefault(0) ?? string.Empty,
            FirstName   = parts.ElementAtOrDefault(1) ?? string.Empty,
            MiddleName  = parts.ElementAtOrDefault(2),
            PhoneNumber = contact.PhoneNumber,
            Email       = contact.Email ?? string.Empty
        };
        ViewBag.ContactId = id;
        return View(vm);
    }

    // POST /Contacts/Edit/{id}
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ContactViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Message     = "⚠ Исправьте ошибки в форме.";
            ViewBag.MessageType = "error";
            ViewBag.ContactId   = id;
            return View(vm);
        }
        var ok = await _contactService.UpdateAsync(id, vm);
        if (!ok) return NotFound();

        TempData["Message"] = $"✏ Контакт «{vm.FullName}» обновлён!";
        return RedirectToAction(nameof(Index));
    }

    // ── DELETE ────────────────────────────────────────────────────────────────
    // POST /Contacts/Delete/{id}
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _contactService.DeleteAsync(id);
        TempData["Message"] = "🗑 Контакт удалён.";
        return RedirectToAction(nameof(Index));
    }
}
