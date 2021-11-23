using System;

namespace Zoo_Management.Models.Database
{
    public class Animal
    {
        public int AnimalId { get; set; }
        public string AnimalName { get; set; }
        public Species Species { get; set; }
        public Sex Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateAcquired { get; set; }
        public string AcquiredFrom { get; set; }
        public Enclosure Enclosure { get; set; }
    }
}
