namespace YummyApi.WebUI.DTOs.TestimonialDTOs
{
    public class GetTestimonialByIDDTO
    {
        public int testimonialID { get; set; } //Testimonial ID
        public string testimonialNameSurname { get; set; } //Testimonial Ad Soyad
        public string testimonialTitle { get; set; } //Testimonial Başlık
        public string testimonialComment { get; set; } //Testimonial Yorum 
        public string testimonialImageURL { get; set; } //Testimonial Resim URL
    }
}
