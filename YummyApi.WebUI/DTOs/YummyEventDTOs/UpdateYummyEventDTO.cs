namespace YummyApi.WebUI.DTOs.YummyEventDTOs
{
    public class UpdateYummyEventDTO
    {
        public int YummyEventID { get; set; } //Yemek Etkinliği ID'si
        public string YummyEventTitle { get; set; } //Yemek Etkinliği Başlığı
        public string YummyEventDescription { get; set; } //Yemek Etkinliği Açıklaması
        public string YummyEventImageURL { get; set; } //Yemek Etkinliği Resim URL'si
        public bool YummyEventStatus { get; set; }  //Yemek Etkinliği Durumu (Aktif/Pasif)
        public decimal YummyEventPrice { get; set; } //Yemek Etkinliği Fiyatı
    }
}
