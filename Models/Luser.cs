using System;
using System.ComponentModel.DataAnnotations;

namespace Hobby_Hub.Models
{


public class Luser
{
    [Required]
    [EmailAddress]
    public string LEmail {get; set;}
    [Required]
    [DataType(DataType.Password)]
    public string LPassword { get; set;}
    
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
}
}