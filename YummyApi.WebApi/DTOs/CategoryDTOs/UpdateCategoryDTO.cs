namespace YummyApi.WebApi.DTOs.CategoryDTOs
{
    public class UpdateCategoryDTO
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        // CategoryID, güncelleme işlemi için gerekli olan benzersiz kimliktir.
        // CategoryName, kategorinin yeni adını temsil eder.
    }
}
