using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace TechStoreMVC.Entities;

[Table("tbReviews")]
public class Review
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("productId", TypeName = "int(11)")]
    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }

    [Column("text")]
    [StringLength(2000)]
    public string Text { get; set; } = null!;

    [Column("rating")]
    public int Rating { get; set; }

    public Review(int id, Product product, string text, int rating)
    {
        Id = id;
        ProductId = product.Id;
        Product = product;
        Text = text;
        Rating = rating;
    }

    public Review()
    {
        Id = 0;
        ProductId = 0;
        Product = null!;
        Text = null!;
        Rating = 3;
    }
}
