using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStoreMVC.Entities;

[Table("tbAccounts")]
public class Account
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("username")]
    [StringLength(64)]
    public string Username { get; set; }

    [Column("password")]
    [StringLength(64)]
    public string Password { get; set; }

    [Column("role")]
    [StringLength(16)]
    public string Role { get; set; } = null!;

    [Column("email")]
    [StringLength(128)]
    public string? Email { get; set; }

    public virtual List<Order> Orders { get; set; }

    public Account(int id, string username, string password, string role, string? email)
    {
        Id = id;
        Username = username;
        Password = password;
        Role = role;
        Email = email;
        Orders = new List<Order>();
    }

    public Account()
    {
        Id = 0;
        Username = null!;
        Password = null!;
        Role = null!;
        Email = null;
        Orders = new List<Order>();
    }
}
