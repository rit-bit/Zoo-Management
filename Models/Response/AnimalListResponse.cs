using System.Collections.Generic;
using System.Linq;
using Zoo_Management.Models.Database;

namespace Zoo_Management.Models.Response
{
    public class AnimalListResponse
    {
        public IEnumerable<AnimalResponse> AnimalList { get; set; }

        public AnimalListResponse(IEnumerable<Animal> animalList)
        {
            AnimalList = animalList.Select(a => new AnimalResponse(a));
        }
    }
}
