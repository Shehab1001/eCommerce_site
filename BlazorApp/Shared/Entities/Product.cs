namespace BlazorApp.Shared.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        //public string PictureFileName { get; set; }

        public string PictureUri { get; set; }

        public string ProductType { get; set; }

        //public string ProductBrand { get; set; }

        // Quantity in stock
        public int AvailableStock { get; set; }

        // Available stock at which we should reorder
        public int RestockThreshold { get; set; }


        // Maximum number of units that can be in-stock at any time (due to physicial/logistical constraints in warehouses)
        public int MaxStockThreshold { get; set; }

        /// <summary>
        /// True if item is on reorder
        /// </summary>
        public bool OnReorder { get; set; }

        public Product() { }

        public int RemoveStock(int quantityDesired)
        {
            if (AvailableStock == 0)
            {
                throw new Exception($"Empty stock, product item {Name} is sold out");
            }

            if (quantityDesired <= 0)
            {
                throw new Exception($"Item units desired should be greater than zero");
            }

            int removed = Math.Min(quantityDesired, AvailableStock);

            AvailableStock -= removed;

            return removed;
        }

        /// <summary>
        /// Increments the quantity of a particular item in inventory.
        /// <param name="quantity"></param>
        /// <returns>int: Returns the quantity that has been added to stock</returns>
        /// </summary>
        public int AddStock(int quantity)
        {
            int original = AvailableStock;

            // The quantity that the client is trying to add to stock is greater than what can be physically accommodated in the Warehouse
            if (AvailableStock + quantity > MaxStockThreshold)
            {
                // For now, this method only adds new units up maximum stock threshold. In an expanded version of this application, we
                //could include tracking for the remaining units and store information about overstock elsewhere. 
                AvailableStock += MaxStockThreshold - AvailableStock;
            }
            else
            {
                AvailableStock += quantity;
            }

            OnReorder = false;

            return AvailableStock - original;
        }

    }
}
