namespace YummyApi.WebUI.DTOs.MessageDTOs
{
    public class ResultMessageByIsReadFalseDTO
    {
        public int MessageID { get; set; } //Mesaj ID
        public string MessageNameSurname { get; set; } //Mesaj Ad Soyad
        public string MessageEmail { get; set; } //Mesaj Email
        public string MessageSubject { get; set; } //Mesaj Konu
        public string MessageDetail { get; set; } //Mesaj Detay
        public DateTime SendDate { get; set; } //Mesaj Tarihi
        public bool MessageIsRead { get; set; } //Mesaj Okundu mu 
    }
}
