using System;
using System.Collections.Generic;

namespace Zoo_Management.Models.Database
{
    public class ZooKeeper
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Animal> AnimalIds { get; set; }
    }
}