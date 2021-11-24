using System.Collections.Generic;
using System.Linq;
using Zoo_Management.Models.Database;

namespace Zoo_Management.Models.Response
{
    public class SpeciesListResponse
    {
        public IEnumerable<SpeciesResponse> SpeciesList { get; set; }

        public SpeciesListResponse(IEnumerable<Species> speciesList)
        {
            SpeciesList = speciesList.Select(s => new SpeciesResponse(s));
        }
    }
}
