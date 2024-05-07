using System.ComponentModel.DataAnnotations;

namespace WebApplication2;

public class AddBook
{
    [MaxLength(100)]
    public string Title { get; set; }
    [MaxLength(100)]
    public string authorname { get; set; }
    [MaxLength(100)]
    public string authorsurname { get; set; }
}