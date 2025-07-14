namespace YummyApi.WebApi.DTOs.NotificationDTOs
{
    public class CreateNotificationDTO
    {
       
        public string NotificationDescription { get; set; }
        public string NotificationIconURL { get; set; }
        public DateTime NotificationDate { get; set; }
        public bool NotificationIsRead { get; set; }
    }
}
