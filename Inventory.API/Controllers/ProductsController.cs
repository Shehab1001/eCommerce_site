using BlazorApp.Shared.Entities;
using BlazorApp.Shared.Models;
using Inventory.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly InventoryDBContext _context;

        public ProductsController(InventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct([FromQuery]GetProductsListQuery getProductsListQuery)
        {
            if (_context.Product == null)
            {
                return NotFound();
            }
            var query = _context.Product.AsQueryable();
            if (!string.IsNullOrWhiteSpace(getProductsListQuery.ProductName))
            {
                query = query.Where(p => p.Name.Contains(getProductsListQuery.ProductName));
            }
            if (!string.IsNullOrWhiteSpace(getProductsListQuery.ProductType))
            {
                query = query.Where(p => p.ProductType.Contains(getProductsListQuery.ProductType));
            }
            if (getProductsListQuery.PriceFrom.HasValue)
            {
                query = query.Where(p => p.Price >= getProductsListQuery.PriceFrom.Value);
            }
            if (getProductsListQuery.PriceTo.HasValue)
            {
                query = query.Where(p => p.Price <= getProductsListQuery.PriceTo.Value);
            }
            return await query.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            if (_context.Product == null)
            {
                return NotFound();
            }
            var product = await _context.Product.FindAsync(Guid.Parse(id));

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(string id, Product product)
        {
            if (id != product.Id.ToString())
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'InventoryDBContext.Product'  is null.");
            }
            _context.Product.Add(product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.Id.ToString()))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            if (_context.Product == null)
            {
                return NotFound();
            }
            var product = await _context.Product.FindAsync(Guid.Parse(id));
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(string id)
        {
            return (_context.Product?.Any(e => e.Id.ToString() == id)).GetValueOrDefault();
        }
    }
}
