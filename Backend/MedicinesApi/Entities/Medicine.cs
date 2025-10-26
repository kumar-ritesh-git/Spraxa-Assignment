using System.ComponentModel.DataAnnotations;

public class Medicine
{
    [Key]
    public int MedicineId { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
