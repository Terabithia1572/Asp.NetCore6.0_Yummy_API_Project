namespace YummyApi.WebUI.DTOs.ChefDTOs
{
    public class CreateChefDTO
    {
        public string ChefNameSurname { get; set; } // Şef Adı Soyadı
        public string ChefTitle { get; set; } //Şef Başlık
        public string ChefDescription { get; set; } //Şef Açıklama
        public string ChefImageURL { get; set; } //Şef Resmi URL
    }
}
