namespace BlazorApp.Shared.Models
{
    public class CreateProductCommand
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUri { get; set; }
        public string ProductType { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }

    }
}
