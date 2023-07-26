namespace BlazorApp.Shared.Models
{
    public class GetProductsListQuery
    {
        public string? ProductName { get; set; }
        public string? ProductType { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
    }
}
