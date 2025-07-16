namespace YummyApi.WebUI.DTOs.FeatureDTOs
{
    public class GetFeatureByIDDTO
    {
        public int FeatureID { get; set; } //Öne Çıkan ID
        public string FeatureTitle { get; set; } //Öne Çıkan Başlık
        public string FeatureSubTitle { get; set; } //Öne Çıkan Alt Başlık
        public string FeatureDescription { get; set; } //Öne Çıkan Açıklama
        public string FeatureVideoURL { get; set; } //Öne Çıkan Video URL
        public string FeatureImage { get; set; } //Öne Çıkan Resim
    }
}
