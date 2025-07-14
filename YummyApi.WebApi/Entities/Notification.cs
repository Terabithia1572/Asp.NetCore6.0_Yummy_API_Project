namespace YummyApi.WebApi.Entities
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public string NotificationDescription { get; set; }
        public string NotificationIconURL { get; set; }
        public DateTime NotificationDate { get; set; }
        public bool NotificationIsRead { get; set; }
    }
}
