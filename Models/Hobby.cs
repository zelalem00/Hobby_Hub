using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hobby_Hub.Models
{
    public class Hobby
    {
        [Key]
        public int HobbyId{get;set;}

        [Required(ErrorMessage="Title is required!")]
        public string Title{get;set;}

        [Required(ErrorMessage="Description is required!")]
        public string Description{get;set;}
        public int UserId {get;set;}
        public User HobbyPoster {get;set;}
        public List<Eventss> Guests {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}