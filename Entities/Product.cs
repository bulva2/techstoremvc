using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStoreMVC.Entities;

[Table("tbProducts")]
[Index("CategoryId", Name = "categoryId")]
public class Product
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("categoryId", TypeName = "int(11)")]
    public int? CategoryId { get; set; }

    [Column("brand")]
    [StringLength(64)]
    public string? Brand { get; set; }

    [Column("model")]
    [StringLength(255)]
    public string Model { get; set; }

    [Column("type")]
    [StringLength(64)]
    public string? Type { get; set; }

    [Column("price")]
    [Precision(11)]
    public decimal Price { get; set; }

    public string Description { get; set; }

    [ForeignKey("CategoryId")]
    public virtual Category? Category { get; set; }

    public string PriceText => $"{Price} CZK";

    public Product(int id, string? brand, string model, string? type, decimal price, string description, Category? category)
    {
        Id = id;
        CategoryId = category?.Id;
        Brand = brand;
        Model = model;
        Type = type;
        Price = price;
        Category = category;
        Description = description;
    }

    public Product()
    {
        Id = 0;
        CategoryId = 0;
        Brand = string.Empty;
        Model = string.Empty;
        Type = string.Empty;
        Price = 1;
        Category = null;
        Description = string.Empty;
    }
}
