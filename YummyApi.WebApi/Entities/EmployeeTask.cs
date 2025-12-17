using System.ComponentModel.DataAnnotations;

namespace YummyApi.WebApi.Entities
{
    public class EmployeeTask
    {
        [Key]
        public int EmployeeTaskId { get; set; } // Birincil Anahtar
        public string TaskName { get; set; } // Görev Adı
        public byte TaskStatusValue { get; set; } // Görev Durumu (0: Atanmadı, 1: Atandı, 2: Tamamlandı)
        public DateTime AssignDate { get; set; } // Atanma Tarihi
        public DateTime DueDate { get; set; } // Bitiş Tarihi
        public string Priority { get; set; } // Öncelik (Düşük, Orta, Yüksek)
        public string TaskStatus { get; set; } // Görev Durumu (Atanmadı, Atandı, Tamamlandı)

        // İlişkiler çoka çok ilişki
        public List<EmployeeTaskChef?> EmployeeTaskChefs { get; set; }


    }
}
