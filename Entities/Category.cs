using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStoreMVC.Entities;

[Table("tbCategories")]
public class Category
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(32)]
    public string Name { get; set; }

    public virtual List<Product> Products { get; set; }

    public Category(int id, string name)
    {
        Id = id;
        Name = name;
        Products = new List<Product>();
    }

    public Category()
    {
        Id = 0;
        Name = null!;
        Products = new List<Product>();
    }
}
