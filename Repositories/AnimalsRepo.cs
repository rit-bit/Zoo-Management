using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Zoo_Management.Data;
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
            IQueryable<Animal> query = _context.Animals
                .Include(a => a.Species)
                .Include(a => a.Enclosure);

            if (searchRequest.Species != null)
                query = query.Where(a => a.Species.SpeciesName.ToLower().Contains(searchRequest.Species));

            if (searchRequest.Classification != null)
                query = query.Where(a => a.Species.Classification == searchRequest.Classification);

            if (searchRequest.Age != null)
                query = query.Where(a => (DateTime.Today.Year - a.DateOfBirth.Year).Equals(searchRequest.Age));

            if (searchRequest.Name != null)
                query = query.Where(a => a.AnimalName.ToLower().Contains(searchRequest.Name));

            if (searchRequest.DateAcquired != null)
                query = query.Where(a => a.DateAcquired.Equals(searchRequest.DateAcquired));

            switch (searchRequest.OrderBy)
            {
                case OrderBy.Age:
                    query = query.OrderBy(a => DateTime.Today.Year - a.DateOfBirth.Year);
                    break;
                case OrderBy.Classification:
                    query = query.OrderBy(a => a.Species.Classification);
                    break;
                case OrderBy.Name:
                    query = query.OrderBy(a => a.AnimalName);
                    break;
                case OrderBy.DateAcquired:
                    query = query.OrderBy(a => a.DateAcquired);
                    break;
                default:
                    query = query.OrderBy(a => a.Species.SpeciesName);
                    break;
            }

            return query.Skip(toSkip).Take(searchRequest.PageSize);
        }

        public Animal Create(CreateAnimalRequest request) // TODO This throws a nasty exception
        {
            var species = _context.Species.Single(s => s.SpeciesId == request.SpeciesId);
            var enclosure = _context.Enclosures.Single(e => e.Id == request.EnclosureId);
            var animalsInEnclosure = _context.Animals.Count(a => a.Enclosure.Id == enclosure.Id);
            var enclosureHasRoom = enclosure.Capacity > animalsInEnclosure;
            if (!enclosureHasRoom)
            {
                throw new EnclosureFullException($"{enclosure.Name} does not have room for {request.AnimalName} the {species.SpeciesName}.");
            }
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