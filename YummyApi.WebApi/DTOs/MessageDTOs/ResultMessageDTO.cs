namespace YummyApi.WebApi.DTOs.MessageDTO
{
    public class ResultMessageDTO
    {
        public int MessageID { get; set; } //Mesaj ID
        public string MessageNameSurname { get; set; } //Mesaj Ad Soyad
        public string MessageEmail { get; set; } //Mesaj Email
        public string MessageSubject { get; set; } //Mesaj Konu
        public string MessageDetail { get; set; } //Mesaj Detay
        public DateTime SendDate { get; set; } //Mesaj Tarihi
        public bool MessageIsRead { get; set; } //Mesaj Okundu mu 
        public string? MessageStatus { get; set; } //Mesaj Durumu
    }
}
