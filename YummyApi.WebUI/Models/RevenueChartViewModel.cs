namespace YummyApi.WebUI.Models
{
    public class RevenueChartViewModel
    {
        public List<string> Labels { get; set; } = new(); // Ay isimleri veya tarih etiketleri
        public List<int> Income { get; set; } = new(); // Gelir verileri
        public List<int> Expense { get; set; } = new(); // Gider verileri

        // İstersen ileride rezervasyonlardan hesaplanacak ek veriler için
        public int TotalReservations { get; set; } // Toplam rezervasyon sayısı
        public int ApprovedReservations { get; set; } // Onaylanmış rezervasyon sayısı
        public int CanceledReservations { get; set; } // İptal edilmiş rezervasyon sayısı
                                                      //    // Alt kutucuk verileri
        public decimal WeeklyEarnings { get; set; }
        public decimal MonthlyEarnings { get; set; }
        public decimal YearlyEarnings { get; set; }

        public int TotalCustomers { get; set; }
        public decimal TotalIncome { get; set; }
        public int ProjectCompleted { get; set; }
        public decimal TotalExpense { get; set; }
        public int NewCustomers { get; set; }
    }
}
//public class RevenueChartViewModel
//{
//    public List<string> Labels { get; set; } = new();
//    public List<int> Income { get; set; } = new();
//    public List<int> Expense { get; set; } = new();

//    // Alt kutucuk verileri
//    public decimal WeeklyEarnings { get; set; }
//    public decimal MonthlyEarnings { get; set; }
//    public decimal YearlyEarnings { get; set; }

//    public int TotalCustomers { get; set; }
//    public decimal TotalIncome { get; set; }
//    public int ProjectCompleted { get; set; }
//    public decimal TotalExpense { get; set; }
//    public int NewCustomers { get; set; }
//}