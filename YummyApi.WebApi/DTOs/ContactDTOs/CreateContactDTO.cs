﻿namespace YummyApi.WebApi.DTOs.ContactDTOs
{
    public class CreateContactDTO
    {
       
        public string ContactMapLocation { get; set; } //Harita Konumu
        public string ContactAddress { get; set; } //Adres
        public string ContactPhone { get; set; } //Telefon
        public string ContactEmail { get; set; } //E-Posta
        public string ContactOpenHours { get; set; } //Açılış Saatleri
    }
}
