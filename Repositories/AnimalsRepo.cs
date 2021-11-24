using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Zoo_Management.Data;
using Zoo_Management.Models;
using Zoo_Management.Models.Database;
using Zoo_Management.Models.Request;

namespace Zoo_Management.Repositories
{
    public interface IAnimalsRepo
    {
        Animal GetById(int id);
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

        public Animal GetById(int id)
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

            query = ApplySearchFilters(query, searchRequest);

            query = searchRequest.OrderBy switch
            {
                OrderBy.Age => query.OrderBy(a => DateTime.Today.Year - a.DateOfBirth.Year),
                OrderBy.Classification => query.OrderBy(a => a.Species.Classification),
                OrderBy.Name => query.OrderBy(a => a.AnimalName),
                OrderBy.DateAcquired => query.OrderBy(a => a.DateAcquired),
                OrderBy.Species => query.OrderBy(a => a.Species.SpeciesName),
                _ => query.OrderBy(a => a.Enclosure.Id).ThenBy(a => a.Species.SpeciesName)
            };

            return query.Skip(toSkip).Take(searchRequest.PageSize);
        }

        private static IQueryable<Animal> ApplySearchFilters(IQueryable<Animal> query, AnimalSearchRequest searchRequest)
        {
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

            if (searchRequest.EnclosureId != null)
                query = query.Where(a => a.Enclosure.Id == searchRequest.EnclosureId);

            return query;
        }

        public Animal Create(CreateAnimalRequest request)
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
                AcquiredFrom = request.AcquiredFrom,
                Enclosure = enclosure
            });
            _context.SaveChanges();

            return insertResponse.Entity;
        }
    }
}