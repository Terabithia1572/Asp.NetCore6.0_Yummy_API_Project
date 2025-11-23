namespace YummyApi.WebApi.DTOs.GroupReservationDTOs
{
    public class GetGroupReservationByIDDTO
    {
        public int GroupReservationID { get; set; } // Grup rezervasyonunun benzersiz kimliği
        public string ResponsibleCustomerName { get; set; } // Sorumlu müşteri adı
        public string GroupReservationTitle { get; set; } // Grup rezervasyon başlığı
        public DateTime GroupReservationDate { get; set; } // Grup rezervasyon tarihi
        public DateTime GroupReservationLastProcessDate { get; set; } // Grup rezervasyon son işlem tarihi
        public string GroupReservationPriority { get; set; } // Grup rezervasyon önceliği
        public string GroupReservationDetails { get; set; } // Grup rezervasyon detayları
        public string GroupReservationStatus { get; set; } // Grup rezervasyon durumu
    }
}
