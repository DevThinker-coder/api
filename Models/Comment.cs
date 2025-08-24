using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public class Comment
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }=String.Empty;
    public string Content { get; set; }=String.Empty;
    public DateTime CreatedAt { get; set; }=DateTime.UtcNow;
    public int? StockId { get; set; }
    public Stock? Stock { get; set; }
}
