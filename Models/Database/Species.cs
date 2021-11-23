using System;

namespace Zoo_Management.Models.Database
{
    public class Species
    {
        public int SpeciesId { get; set; }
        public string SpeciesName { get; set; }
        public Classification Classification { get; set; }
    }

    public enum Classification
    {
        Fish,
        Amphibian,
        Reptile,
        Bird,
        Mammal,
        Invertebrate
    }
    
    public static class ClassificationEnumHelper
    {
        public static Classification IntToEnum(Classification number)
        {
            return IntToEnum((int) number);
        }
        public static Classification IntToEnum(int number)
        {
            if (Enum.IsDefined(typeof(Classification), number))
            {
                return (Classification) number;
            }

            throw new ArgumentOutOfRangeException($"{number} is not a valid Sex Enum");
        }
    }
}
