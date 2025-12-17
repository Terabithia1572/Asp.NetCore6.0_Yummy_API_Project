using Microsoft.EntityFrameworkCore;
using YummyApi.WebApi.Entities;

namespace YummyApi.WebApi.Context
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;initial Catalog=ApiYummyDB;integrated security=true;");
        }
        public DbSet<Category> Categories { get; set; } // Kategoriler tablosu
        public DbSet<Chef> Chefs { get; set; } // Şefler tablosu
        public DbSet<Contact> Contacts { get; set; } // İletişim tablosu
        public DbSet<Feature> Features { get; set; } // Özellikler tablosu
        public DbSet<Image> Images { get; set; } // Resimler tablosu
        public DbSet<Message> Messages { get; set; } // Mesajlar tablosu
        public DbSet<Product> Products { get; set; } // Ürünler tablosu
        public DbSet<Reservation> Reservations { get; set; } // Rezervasyonlar tablosu
        public DbSet<Service> Services { get; set; } // Hizmetler tablosu
        public DbSet<Testimonial> Testimonials { get; set; } // Referanslar tablosu
        public DbSet<YummyEvent> YummyEvents { get; set; } // Etkinlikler tablosu
        public DbSet<Notification> Notifications { get; set; } // Bildirimler tablosu
        public DbSet<About> Abouts { get; set; } // Hakkımızda tablosu
        public DbSet<EmployeeTask> EmployeeTasks { get; set; } // Çalışan Görevleri tablosu
        public DbSet<EmployeeTaskChef> EmployeeTaskChefs { get; set; } // İlişkiler çoka çok ilişki tablosu 
        public DbSet<GroupReservation> GroupReservations { get; set; } // Grup Rezervasyonları tablosu


    }
}
