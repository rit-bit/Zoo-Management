using System;
using Zoo_Management.Models.Database;

namespace Zoo_Management.Models
{
    public class AnimalResponse
    {
        private readonly Animal _animal;

        public AnimalResponse(Animal animal)
        {
            _animal = animal;
        }

        public int Id => _animal.AnimalId;
        public string Name => _animal.AnimalName;
        public Species Species => _animal.Species;
        public Sex Sex => SexEnumHelper.IntToEnum(_animal.Sex);
        public DateTime DateOfBirth => _animal.DateOfBirth;
        public DateTime? DateAcquired => _animal.DateAcquired;
        public string AcquiredFrom => _animal.AcquiredFrom;
        public Enclosure Enclosure => _animal.Enclosure;
    }
}
