namespace YummyApi.WebApi.DTOs.RezervationDTOs
{
    public class CreateRezervationDTO
    {
        public string ReservationNameSurname { get; set; } //Rezervasyon Ad Soyad
        public string ReservationEmail { get; set; } //Rezervasyon Email
        public string ReservationPhoneNumber { get; set; } //Rezervasyon Telefon Numarası
        public DateTime ReservationDate { get; set; } //Rezervasyon Tarihi
        public string ReservationTime { get; set; } //Rezervasyon Saati
        public int CountOfPeople { get; set; } //Kişi Sayısı
        public string ReservationMessage { get; set; } //Rezervasyon Mesajı
        public string ReservationStatus { get; set; } //Rezervasyon Durumu
    }
}
