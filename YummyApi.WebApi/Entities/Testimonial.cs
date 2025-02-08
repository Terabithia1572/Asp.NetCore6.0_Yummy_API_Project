namespace YummyApi.WebApi.Entities
{
    public class Testimonial
    {
        public int TestimonialID { get; set; } //
        public string TestimonialNameSurname { get; set; }
        public string TestimonialTitle { get; set; }
        public string TestimonialComment { get; set; }
        public string TestimonialImageURL { get; set; }
    }
}
