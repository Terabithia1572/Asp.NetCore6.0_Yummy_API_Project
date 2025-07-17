namespace YummyApi.WebUI.DTOs.AboutDTOs
{
    public class CreateAboutDTO
    {
        public string AboutTitle { get; set; } //Hakkında Başlık
        public string AboutImageURL { get; set; } //Hakkında Resim URL
        public string AboutVideoCoverImageURL { get; set; } //Hakkında Vide Kapak Görsel URL'i
        public string AboutVideoURL { get; set; } //Hakkında Video URL
        public string AboutDescription { get; set; } //Hakkında Açıklama
        public string ReservationNumber { get; set; } //Hakkında Rezervasyon Numarası
    }
}
