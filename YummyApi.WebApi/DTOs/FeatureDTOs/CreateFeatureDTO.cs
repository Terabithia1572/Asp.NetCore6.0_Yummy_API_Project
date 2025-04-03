﻿namespace YummyApi.WebApi.DTOs.FeatureDTOs
{
    public class CreateFeatureDTO
    {
        
        public string FeatureTitle { get; set; } //Öne Çıkan Başlık
        public string FeatureSubTitle { get; set; } //Öne Çıkan Alt Başlık
        public string FeatureDescription { get; set; } //Öne Çıkan Açıklama
        public string FeatureVideoURL { get; set; } //Öne Çıkan Video URL
        public string FeatureImage { get; set; } //Öne Çıkan Resim
    }
}
