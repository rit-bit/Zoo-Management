using Microsoft.AspNetCore.Mvc;
using Zoo_Management.Models;
using Zoo_Management.Repositories;

namespace Zoo_Management.Controllers
{
    [Route("species")]
    public class SpeciesController : Controller
    {
        private readonly ISpeciesRepo _speciesRepo;

        public SpeciesController(ISpeciesRepo speciesRepo)
        {
            _speciesRepo = speciesRepo;
        }

        [HttpGet]
        public ActionResult<SpeciesListResponse> GetAll()
        {
            var species = _speciesRepo.GetAll();
            return new SpeciesListResponse(species);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateSpeciesRequest newSpecies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var species = _speciesRepo.Create(newSpecies);

            var url = Url.Action("GetAll");
            var responseModel = new SpeciesResponse(species);
            return Created(url, responseModel);   
        }
    }
}
