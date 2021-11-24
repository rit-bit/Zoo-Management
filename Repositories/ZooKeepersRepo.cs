using System.Collections.Generic;
using System.Linq;
using Zoo_Management.Models;
using Zoo_Management.Models.Database;
using Zoo_Management.Models.Request;

namespace Zoo_Management.Repositories
{
    public interface IZooKeepersRepo
    {
        ZooKeeper GetById(int id);
        ZooKeeper Create(CreateZooKeeperRequest zooKeeperRequest);
    }
    
    public class ZooKeepersRepo : IZooKeepersRepo
    {
        private readonly ZooDbContext _context;

        public ZooKeepersRepo(ZooDbContext context)
        {
            _context = context;
        }

        public ZooKeeper GetById(int id)
        {
            return _context.ZooKeepers.Single(z => z.Id == id);
        }

        public ZooKeeper Create(CreateZooKeeperRequest zooKeeperRequest)
        {
            var animals = _context.Animals.Where(a => zooKeeperRequest.AnimalIds.Contains(a.AnimalId));
            var insertResponse = _context.ZooKeepers.Add(new ZooKeeper
            {
                FirstName = zooKeeperRequest.FirstName,
                LastName = zooKeeperRequest.LastName,
                DateOfBirth = zooKeeperRequest.DateOfBirth,
                AnimalIds = animals.ToList()
            });
            _context.SaveChanges();

            return insertResponse.Entity;
        }
    }
}