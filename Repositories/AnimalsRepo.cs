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
        IEnumerable<Animal> GetAll(int pageNumber, int pageSize);
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

        public IEnumerable<Animal> GetAll(int pageNumber, int pageSize)
        {
            var toSkip = pageSize * (pageNumber - 1);
            return _context.Animals.Skip(toSkip).Take(pageSize);
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