using System;
using System.ComponentModel.DataAnnotations;

namespace Hobby_Hub.Models
{
    public class Eventss
    {
        [Key]
        public int EventsId {get;set;}
        public int UserId {get;set;}
        public User Creator {get;set;}
        public int HobbyId {get;set;}
        public Hobby Hobby {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}