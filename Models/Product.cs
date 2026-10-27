using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(100)]
    [Display(Name = "Product Name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "SKU is required.")]
    [StringLength(40)]
    [Display(Name = "SKU / Product Code")]
    public string SKU { get; set; } = string.Empty;

    [Required]
    [StringLength(60)]
    public string Category { get; set; } = string.Empty;

    [Range(0.01, 99999999, ErrorMessage = "Price must be greater than zero.")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [Range(0, 1000000, ErrorMessage = "Quantity cannot be negative.")]
    public int Quantity { get; set; }

    [Range(0, 1000000, ErrorMessage = "Reorder level cannot be negative.")]
    [Display(Name = "Low Stock Alert At")]
    public int ReorderLevel { get; set; } = 5;

    [StringLength(100)]
    public string? Supplier { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Last Updated")]
    public DateTime UpdatedAt { get; set; } = DateTime.Today;
}
