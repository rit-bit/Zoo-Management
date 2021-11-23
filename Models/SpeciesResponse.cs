using Zoo_Management.Models.Database;

namespace Zoo_Management.Models
{
    public class SpeciesResponse
    {
        private readonly Species _species;

        public SpeciesResponse(Species species)
        {
            _species = species;
        }

        public int Id => _species.SpeciesId;
        public string Name => _species.SpeciesName;
        public Classification Classification => _species.Classification;
    }
}
