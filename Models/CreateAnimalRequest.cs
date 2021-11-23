using System;
using Zoo_Management.Models.Database;

namespace Zoo_Management.Models
{
    public class CreateAnimalRequest
    {
        public string AnimalName { get; set; }
        public int SpeciesId { get; set; }
        public Sex Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateAcquired { get; set; }
        public string AcquiredFrom { get; set; } 
    }
}
