using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zoo_Management.Models.Database;

namespace Zoo_Management.Data
{
    public static class SampleZooKeepers
    {
        private const int NumberOfZooKeepers = 20;
        public static IEnumerable<ZooKeeper> GetZooKeepers(IEnumerable<Animal> animals)
        {
            
            var firstNames = GetNames();
            var lastNames = GetLastNames();
            var animalsList = animals.ToList();
            for (var i = 0; i < Math.Min(NumberOfZooKeepers, animalsList.Count); i++)
            {
                yield return new ZooKeeper
                {
                    FirstName = NameHelper.GetRandomName(firstNames),
                    LastName = NameHelper.GetRandomName(lastNames),
                    DateOfBirth = DateHelper.GetRandomZooKeeperDate(),
                    AnimalIds = new List<Animal> {animalsList[i]} // TODO This column does not get created in the database
                };
            }
        }

        private static List<string> GetNames()
        {
            return new List<string>
            {
                "Jennifer",
                "Amy",
                "Melissa",
                "Michelle",
                "Kimberly",
                "Lisa",
                "Angela",
                "Heather",
                "Stephanie",
                "Nicole",
                "Jessica",
                "Elizabeth",
                "Rebecca",
                "Kelly",
                "Mary",
                "Christina",
                "Amanda",
                "Julie",
                "Sarah",
                "Laura",
                "Shannon",
                "Christine",
                "Tammy",
                "Tracy",
                "Karen",
                "Dawn",
                "Susan",
                "Andrea",
                "Tina",
                "Patricia",
                "Cynthia",
                "Lori",
                "Rachel",
                "April",
                "Maria",
                "Wendy",
                "Crystal",
                "Stacy",
                "Erin",
                "Jamie",
                "Carrie",
                "Tiffany",
                "Tara",
                "Sandra",
                "Monica",
                "Danielle",
                "Stacey",
                "Pamela",
                "Tonya",
                "Sara",
                "Michael",
                "Christopher",
                "Jason",
                "David",
                "James",
                "John",
                "Robert",
                "Brian",
                "William",
                "Matthew",
                "Joseph",
                "Daniel",
                "Kevin",
                "Eric",
                "Jeffrey",
                "Richard",
                "Scott",
                "Mark",
                "Steven",
                "Thomas",
                "Timothy",
                "Anthony",
                "Charles",
                "Joshua",
                "Ryan",
                "Jeremy",
                "Paul",
                "Andrew",
                "Gregory",
                "Chad",
                "Kenneth",
                "Jonathan",
                "Stephen",
                "Shawn",
                "Aaron",
                "Adam",
                "Patrick",
                "Justin",
                "Sean",
                "Edward",
                "Todd",
                "Donald",
                "Ronald",
                "Benjamin",
                "Keith",
                "Bryan",
                "Gary",
                "Jose",
                "Nathan",
                "Douglas"
            };
        }

        private static List<string> GetLastNames()
        {
            return new List<string>
            {
                "Smith",
                "Johnson",
                "Williams",
                "Brown",
                "Jones",
                "Garcia",
                "Miller",
                "Davis",
                "Rodriguez",
                "Martinez",
                "Hernandez",
                "Lopez",
                "Gonzalez",
                "Wilson",
                "Anderson",
                "Thomas",
                "Taylor",
                "Moore",
                "Jackson",
                "Martin",
                "Lee",
                "Perez",
                "Thompson",
                "White",
                "Harris",
                "Sanchez",
                "Clark",
                "Ramirez",
                "Lewis",
                "Robinson",
                "Walker",
                "Young",
                "Allen",
                "King",
                "Wright",
                "Scott",
                "Torres",
                "Nguyen",
                "Hill",
                "Flores",
                "Green",
                "Adams",
                "Nelson",
                "Baker",
                "Hall",
                "Rivera",
                "Campbell",
                "Mitchell",
                "Carter",
                "Roberts"
            };
        }
    }
}