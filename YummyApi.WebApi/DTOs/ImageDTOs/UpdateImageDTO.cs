namespace YummyApi.WebApi.DTOs.ImageDTOs
{
    public class UpdateImageDTO
    {
        public int ImageID { get; set; } //Resmin ID'si
        public string ImageTitle { get; set; }
        public string ImageURL { get; set; } //Resmin URL'si
    }
}
