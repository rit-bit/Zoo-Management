using System;
using System.Collections;
using System.Collections.Generic;
using Zoo_Management.Models.Database;

namespace Zoo_Management.Models.Request
{
    public class CreateZooKeeperRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<int> AnimalIds { get; set; }
    }
}