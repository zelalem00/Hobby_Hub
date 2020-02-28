using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
namespace Hobby_Hub.Models
{
        public class User
    {
        [Key]
        public int UserId {get;set;}
        [Required]
        [MinLength(3, ErrorMessage="First name must be 3 characters or longer!")]
        public string FirstName {get;set;}
        [Required]
        [MinLength(3, ErrorMessage="Last name must be 3 characters or longer!")]
        public string LastName {get;set;}
        [Required]
        [RegularExpression(@"^[\s\S]{3,15}$", ErrorMessage = "User name must be between 3 to 15 charactors")]
        public string UserName {get;set;}
        [EmailAddress]
        [Required]
        public string Email {get;set;}
        [DataType(DataType.Password)]
        [Required]
        [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
        public string Password {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        public List<Hobby> HobbyPosted {get;set;}
        public List<Eventss> EventPosted {get;set;}
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}
        public int AccountId{get; set;}
    } 
}