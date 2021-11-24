using System;
using System.Collections.Generic;
using System.Linq;
using Zoo_Management.Models.Database;

namespace Zoo_Management.Models.Response
{
    public class ZooKeeperResponse
    {
        private readonly ZooKeeper _zooKeeper;

        public ZooKeeperResponse(ZooKeeper zooKeeper)
        {
            _zooKeeper = zooKeeper;
        }

        public int Id => _zooKeeper.Id;
        public string FirstName => _zooKeeper.FirstName;
        public string LastName => _zooKeeper.LastName;
        public DateTime DateOfBirth => _zooKeeper.DateOfBirth;
        public IEnumerable<AnimalResponse> Animals => _zooKeeper.AnimalIds.Select(a => new AnimalResponse(a));
    }
}