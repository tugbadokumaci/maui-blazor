using System;
using System.ComponentModel.DataAnnotations;
using MauiBlazor.Shared.Models.Common;

namespace MauiBlazor.Shared.Models;
public class CardModel : BaseModel
{
    [Key]
    public int Id { get; set; }
    public String CreatorId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Mail { get; set; } = string.Empty;
    public string WebsiteUrl { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}