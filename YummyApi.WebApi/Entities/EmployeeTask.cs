namespace YummyApi.WebApi.Entities
{
    public class EmployeeTask
    {
        public int EmployeeTaskID { get; set; } // Görev ID
        public string TaskName { get; set; } // Görev Adı
        public byte TaskStatusValue { get; set; } // Görev Durumu (0: Beklemede, 1: Devam Ediyor, 2: Tamamlandı)
        public DateTime AssignDate { get; set; } // Atanma Tarihi
        public DateTime DueDate { get; set; } // Bitiş Tarihi
        public string Priority { get; set; } // Öncelik (Düşük, Orta, Yüksek)
        public string TaskStatus { get; set; } // Görev Durumu (Beklemede, Devam Ediyor, Tamamlandı)
        public int ChefID { get; set; } // Şef ID (Yabancı Anahtar)
        public Chef Chef { get; set; } // İlişkili Şef


    }
}
