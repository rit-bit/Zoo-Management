using Microsoft.AspNetCore.Mvc;
using Zoo_Management.Models;
using Zoo_Management.Models.Request;
using Zoo_Management.Models.Response;
using Zoo_Management.Repositories;

namespace Zoo_Management.Controllers
{
    public class ZooKeepersController : Controller
    {
        private readonly IZooKeepersRepo _zooKeepersRepo;

        public ZooKeepersController(IZooKeepersRepo zooKeepersRepo)
        {
            _zooKeepersRepo = zooKeepersRepo;
        }
        
        [HttpGet("{id:int}")]
        public ActionResult<ZooKeeperResponse> GetById([FromRoute] int id)
        {
            var zooKeeper = _zooKeepersRepo.GetById(id);
            return new ZooKeeperResponse(zooKeeper);
        }
        
        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateZooKeeperRequest newZooKeeper)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var zooKeeper = _zooKeepersRepo.Create(newZooKeeper);
                var url = Url.Action("GetById", new {id = zooKeeper.Id});
                var responseModel = new ZooKeeperResponse(zooKeeper);
                return Created(url, responseModel);
            }
            catch (EnclosureFullException e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}