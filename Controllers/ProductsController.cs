using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers;

public class ProductsController : Controller
{
    private readonly ApplicationDbContext _db;
    public ProductsController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index(string? search, string? category, string? stock)
    {
        var query = _db.Products.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(p => p.Name.Contains(search) || p.SKU.Contains(search) ||
                                     (p.Supplier != null && p.Supplier.Contains(search)));
        if (!string.IsNullOrWhiteSpace(category))
            query = query.Where(p => p.Category == category);
        if (stock == "low")
            query = query.Where(p => p.Quantity <= p.ReorderLevel);
        ViewBag.Search = search;
        ViewBag.Category = category;
        ViewBag.Stock = stock;
        ViewBag.Categories = await _db.Products.Select(p => p.Category).Distinct().OrderBy(c => c).ToListAsync();
        return View(await query.OrderBy(p => p.Name).ToListAsync());
    }

    public IActionResult Create() => View(new Product());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        product.UpdatedAt = DateTime.Today;
        if (await _db.Products.AnyAsync(p => p.SKU == product.SKU))
            ModelState.AddModelError(nameof(product.SKU), "This SKU already exists. Please use a unique code.");

        if (!ModelState.IsValid) return View(product);
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Product added successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var product = await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        return product == null ? NotFound() : View(product);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var product = await _db.Products.FindAsync(id);
        return product == null ? NotFound() : View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product)
    {
        if (id != product.Id) return NotFound();
        product.UpdatedAt = DateTime.Today;
        if (await _db.Products.AnyAsync(p => p.SKU == product.SKU && p.Id != product.Id))
            ModelState.AddModelError(nameof(product.SKU), "This SKU is already used by another product.");

        if (!ModelState.IsValid) return View(product);
        try
        {
            _db.Update(product);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Product updated successfully.";
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _db.Products.AnyAsync(p => p.Id == id)) return NotFound();
            throw;
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var product = await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        return product == null ? NotFound() : View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product != null)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Product deleted successfully.";
        }
        return RedirectToAction(nameof(Index));
    }
}
