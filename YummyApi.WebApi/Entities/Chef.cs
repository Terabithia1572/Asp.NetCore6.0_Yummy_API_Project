namespace YummyApi.WebApi.Entities
{
    public class Chef
    {
        public int ChefID { get; set; } //Şef ID
        public string ChefNameSurname { get; set; } // Şef Adı Soyadı
        public string ChefTitle { get; set; } //Şef Başlık
        public string ChefDescription { get; set; } //Şef Açıklama
        public string ChefImageURL { get; set; } //Şef Resmi URL
    }
}
