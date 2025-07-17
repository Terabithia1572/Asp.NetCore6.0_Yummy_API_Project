namespace YummyApi.WebUI.DTOs.WhyChooseYummyDTOs
{
    public class UpdateWhyChooseYummyDTO
    {
        public int ServiceID { get; set; } //Servis ID
        public string ServiceTitle { get; set; } //Servis Başlık
        public string ServiceDescription { get; set; } //Servis Açıklama
        public string ServiceIconURL { get; set; } //Servis İkon URL
    }
}
