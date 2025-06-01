using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Models;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryItemsController(InventoryItemContext context) : ControllerBase
    {
        private readonly InventoryItemContext _context = context;

        // GET: api/InventoryItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItem>>> GetInventoryItems()
        {
            return await _context.InventoryItems.ToListAsync();
        }

        // GET: api/InventoryItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryItem>> GetInventoryItem(string id)
        {
            var inventoryItem = await _context.InventoryItems.FindAsync(id);

            if (inventoryItem == null)
            {
                return NotFound();
            }

            return inventoryItem;
        }

        // PUT: api/InventoryItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventoryItem(string id, CreateInventoryItemDTO inventoryItem)
        {

            var fetchedItem = await _context.InventoryItems.FindAsync(id);
            if (fetchedItem == null)
            {
                return NotFound();
            }

            var item = new InventoryItem
            {
                Id = Guid.NewGuid().ToString(),
                Name = inventoryItem.Name,
                Quantity = inventoryItem.Quantity ?? fetchedItem.Quantity,
                Price = inventoryItem.Price ?? fetchedItem.Price
            };
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryItemExists(id))
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

        // POST: api/InventoryItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InventoryItem>> PostInventoryItem(CreateInventoryItemDTO inventoryItemDTO)
        {
            var item = new InventoryItem
            {
                Id = Guid.NewGuid().ToString(),
                Name = inventoryItemDTO.Name,
                Quantity = inventoryItemDTO.Quantity ?? 0,
                Price = inventoryItemDTO.Price ?? 0
            };

            _context.InventoryItems.Add(item);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InventoryItemExists(item.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetInventoryItem), new { id = item.Id }, item);
        }

        // DELETE: api/InventoryItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryItem(string id)
        {
            var inventoryItem = await _context.InventoryItems.FindAsync(id);
            if (inventoryItem == null)
            {
                return NotFound();
            }

            _context.InventoryItems.Remove(inventoryItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryItemExists(string id)
        {
            return _context.InventoryItems.Any(e => e.Id == id);
        }
    }
}
