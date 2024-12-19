using System.ComponentModel.DataAnnotations;

namespace TodoRestApiV2.Models;

public class TodoItem
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Todo { get; set; } = null!;
    public bool IsComplete { get; set; } = false;
}