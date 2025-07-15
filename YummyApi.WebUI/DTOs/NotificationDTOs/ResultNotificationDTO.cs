namespace YummyApi.WebUI.DTOs.NotificationDTOs
{
    public class ResultNotificationDTO
    {
        public int NotificationID { get; set; } //Bildirim ID'sini tutuyoruz burada
        public string NotificationDescription { get; set; } //Bildirim açıklamasını tutuyoruz
        public string NotificationIconURL { get; set; } //Bildirim simge URL'sini tutuyoruz
        public DateTime NotificationDate { get; set; } //Bildirim tarihini tutuyoruz
        public bool NotificationIsRead { get; set; } //Bildirim okunmuş mu kontrol ediyoruz
    }
}
