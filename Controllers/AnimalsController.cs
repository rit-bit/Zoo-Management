using Microsoft.AspNetCore.Mvc;
using Zoo_Management.Models;
using Zoo_Management.Repositories;

namespace Zoo_Management.Controllers
{
    [Route("/animals")]
    public class AnimalsController : Controller
    {
        private readonly IAnimalsRepo _animalsRepo;

        public AnimalsController(IAnimalsRepo animalsRepo)
        {
            _animalsRepo = animalsRepo;
        }
        
        [HttpGet]
        public ActionResult<AnimalListResponse> Search([FromQuery] AnimalSearchRequest searchRequest)
        {
            var animals = _animalsRepo.Search(searchRequest);
            return new AnimalListResponse(animals);
        }

        [HttpGet("{id:int}")]
        public ActionResult<AnimalResponse> GetById([FromRoute] int id)
        {
            var animal = _animalsRepo.GetAnimalById(id);
            return new AnimalResponse(animal);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateAnimalRequest newAnimal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var animal = _animalsRepo.Create(newAnimal);

            var url = Url.Action("GetById", new {id = animal.AnimalId});
            var responseModel = new AnimalResponse(animal);
            return Created(url, responseModel);
        }
    }
}
