using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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

    public Review(int id, Product product, string text)
    {
        Id = id;
        ProductId = product.Id;
        Product = product;
        Text = text;
    }

    public Review()
    {
        Id = 0;
        ProductId = 0;
        Product = null!;
        Text = null!;
    }
}
