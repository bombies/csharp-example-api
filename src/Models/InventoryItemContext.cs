
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Models;

public class InventoryItemContext(DbContextOptions<InventoryItemContext> options) : DbContext(options)
{
    public DbSet<InventoryItem> InventoryItems { get; set; } = null!;
}