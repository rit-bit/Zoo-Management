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
}