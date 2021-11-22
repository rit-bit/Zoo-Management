using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zoo_Management.Models;
using Zoo_Management.Models.Database;

namespace Zoo_Management.Repositories
{
    public interface IAnimalsRepo
    {
        Animal GetAnimalById(int id);
        IEnumerable<Animal> Search(AnimalSearchRequest searchRequest);
        Animal Create(CreateAnimalRequest request);
    }

    public class AnimalsRepo : IAnimalsRepo
    {
        private readonly ZooDbContext _context;

        public AnimalsRepo(ZooDbContext context)
        {
            _context = context;
        }

        public Animal GetAnimalById(int id)
        {
            var animal = _context.Animals.Single(a => a.AnimalId == id);
            _context.Entry(animal).Reference(a => a.Species).Load();
            return animal;
        }

        public IEnumerable<Animal> Search(AnimalSearchRequest searchRequest)
        {
            var toSkip = searchRequest.PageSize * (searchRequest.PageNumber - 1);
            return _context.Animals.Where(a => searchRequest.Search == null ||
                                               (
                                                   a.AnimalName.ToLower().Contains(searchRequest.Search) ||
                                                   a.Species.SpeciesName.ToLower().Contains(searchRequest.Search) ||
                                                   a.Species.Classification.ToString().ToLower()
                                                       .Contains(searchRequest.Search)
                                                   // TODO Fix _"int.ToString()"_ not working - should be _"enum.ToString()"_
                                                   // TODO Add age (as a number not a date) and DateAcquired
                                               ))
                .Skip(toSkip).Take(searchRequest.PageSize);
        }

        public Animal Create(CreateAnimalRequest request)
        {
            var species = _context.Species.Single(s => s.SpeciesId == request.SpeciesId);
            var insertResponse = _context.Animals.Add(new Animal
            {
                AnimalName = request.AnimalName,
                Species = species,
                Sex = request.Sex,
                DateOfBirth = request.DateOfBirth,
                DateAcquired = request.DateAcquired,
                AcquiredFrom = request.AcquiredFrom
            });
            _context.SaveChanges();

            return insertResponse.Entity;
        }
    }
}