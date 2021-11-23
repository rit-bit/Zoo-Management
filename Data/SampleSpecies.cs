using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NLog;
using Zoo_Management.Models.Database;

namespace Zoo_Management.Data
{
    public static class SampleSpecies
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public static IEnumerable<Species> GetSpecies()
        {
            return GetAmphibians()
                .Concat(GetReptiles())
                .Concat(GetBirds())
                .Concat(GetFish())
                .Concat(GetMammals())
                .Concat(GetInvertebrates());
        }

        private static IEnumerable<Species> GetReptiles()
        {
            return GetReptilesData().Select(reptileSpecies => new Species()
            {
                SpeciesName = reptileSpecies,
                Classification = Classification.Reptile
            });
        }
        
        private static IEnumerable<string> GetReptilesData()
        {
            return new List<string>()
            {
                "Alligator",
                "Iguana",
                "Snake",
                "Turtle",
            };
        }
        
        private static IEnumerable<Species> GetFish()
        {
            return GetFishData().Select(fishSpecies => new Species()
            {
                SpeciesName = fishSpecies,
                Classification = Classification.Fish
            });
        }
        
        private static IEnumerable<string> GetFishData()
        {
            return new List<string>()
            {
                
            };
        }

        private static IEnumerable<Species> GetAmphibians()
        {
            return GetAmphibiansData().Select(amphibianSpecies => new Species()
            {
                SpeciesName = amphibianSpecies,
                Classification = Classification.Amphibian
            });
        }
        
        private static IEnumerable<string> GetAmphibiansData()
        {
            return new List<string>()
            {
                "Salamander",
                "Toad",
                "Frog",
            };
        }
        
        private static IEnumerable<Species> GetBirds()
        {
            return GetBirdsData().Select(birdSpecies => new Species()
            {
                SpeciesName = birdSpecies,
                Classification = Classification.Bird
            });
        }
        
        private static IEnumerable<string> GetBirdsData()
        {
            return new List<string>()
            {
                "Flamingo",
                "Parrot",
                "Penguin",
            };
        }
        
        private static IEnumerable<Species> GetMammals()
        {
            return GetMammalsData().Select(mammalSpecies => new Species()
            {
                SpeciesName = mammalSpecies,
                Classification = Classification.Mammal
            });
        }
        
        private static IEnumerable<string> GetMammalsData()
        {
            return new List<string>()
            {
                "Bear",
                "Camel",
                "Cheetah",
                "Elephant",
                "Giraffe",
                "Gorilla",
                "Hippo",
                "Hyena",
                "Kangaroo",
                "Koala",
                "Lion",
                "Monkey",
                "Panda",
                "Sloth",
                "Tiger",
                "Walrus",
                "Zebra"
            };
        }

        private static IEnumerable<Species> GetInvertebrates()
        {
            return GetInvertebratesData().Select(invertebrateSpecies => new Species()
            {
                SpeciesName = invertebrateSpecies,
                Classification = Classification.Invertebrate
            });
        }

        private static IEnumerable<string> GetInvertebratesData()
        {
            return new List<string>()
            {
                "Jellyfish",
                "Octopus",
                "Spider",
                "Squid",
                "Earthworm",
                "Sea anemone"
            };
        }
    }
}
