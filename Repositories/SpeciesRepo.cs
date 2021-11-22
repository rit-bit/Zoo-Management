using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zoo_Management.Models;
using Zoo_Management.Models.Database;

namespace Zoo_Management.Repositories
{
    public interface ISpeciesRepo
    {
        Species Create(CreateSpeciesRequest newSpecies);
        IEnumerable<Species> GetAll();
    }
    
    public class SpeciesRepo : ISpeciesRepo
    {
        private readonly ZooDbContext _context;

        public SpeciesRepo(ZooDbContext context)
        {
            _context = context;
        }
        
        public Species Create(CreateSpeciesRequest newSpecies)
        {
            var insertResponse = _context.Species.Add(new Species
            {
                Classification = newSpecies.Classification,
                SpeciesName = newSpecies.SpeciesName
            });
            _context.SaveChanges();

            return insertResponse.Entity;
        }

        public IEnumerable<Species> GetAll()
        {
            return _context.Species.ToList();
            
        }
    }
}