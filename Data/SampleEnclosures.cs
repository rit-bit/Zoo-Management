using System.Collections.Generic;
using Zoo_Management.Models.Database;

namespace Zoo_Management.Data
{
    public static class SampleEnclosures
    {
        public static IEnumerable<Enclosure> GetEnclosures()
        {
            return new List<Enclosure>
            {
                new Enclosure
                {
                    Name = "Lions Enclosure",
                    Capacity = 10
                },
                new Enclosure
                {
                    Name = "Aviary",
                    Capacity = 50,
                },
                new Enclosure
                {
                    Name = "Reptile House",
                    Capacity = 40,
                },
                new Enclosure
                {
                    Name = "Giraffe Enclosure",
                    Capacity = 6,
                },
                new Enclosure
                {
                    Name = "Hippo Enclosure", Capacity = 10,
                }
            };
        }
    }
}