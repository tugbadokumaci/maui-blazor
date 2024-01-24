using System;
using System.ComponentModel.DataAnnotations;

namespace MauiBlazor.Shared.Models;

public class SavedModel
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string UserId { get; set; }
    public int CardId { get; set; }
}

