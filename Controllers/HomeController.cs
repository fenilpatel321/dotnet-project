using InventoryManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;
    public HomeController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var products = await _db.Products.AsNoTracking().ToListAsync();
        ViewBag.ProductCount = products.Count;
        ViewBag.StockUnits = products.Sum(p => p.Quantity);
        ViewBag.InventoryValue = products.Sum(p => p.Price * p.Quantity);
        ViewBag.LowStockCount = products.Count(p => p.Quantity <= p.ReorderLevel);
        ViewBag.LowStockProducts = products.Where(p => p.Quantity <= p.ReorderLevel)
            .OrderBy(p => p.Quantity).Take(5).ToList();
        return View();
    }

    public IActionResult Error() => View();
}
