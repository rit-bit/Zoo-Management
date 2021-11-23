using Zoo_Management.Models.Database;

namespace Zoo_Management.Models
{
    public class CreateSpeciesRequest
    {
        public string SpeciesName { get; set; }
        public Classification Classification { get; set; }
    }
}
