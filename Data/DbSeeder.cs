using PhoneBook.Models;

namespace PhoneBook.Data;

/// <summary>
/// Заполняет БД начальными данными, если таблица пустая.
/// Вызывается один раз при старте после EnsureCreated().
/// </summary>
public static class DbSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (db.Contacts.Any()) return; // уже есть данные — пропускаем

        db.Contacts.AddRange(
            new Contact { Name = "Алексей Петров",  PhoneNumber = "+7 (999) 123-45-67", Email = "aleksey@example.com" },
            new Contact { Name = "Мария Иванова",   PhoneNumber = "+7 (999) 234-56-78", Email = "maria@example.com"   },
            new Contact { Name = "Дмитрий Сидоров", PhoneNumber = "+7 (999) 345-67-89", Email = null                  },
            new Contact { Name = "Елена Козлова",   PhoneNumber = "+7 (999) 456-78-90", Email = "elena@example.com"   },
            new Contact { Name = "Андрей Новиков",  PhoneNumber = "+7 (999) 567-89-01", Email = "andrey@example.com"  }
        );
        db.SaveChanges();
    }
}
