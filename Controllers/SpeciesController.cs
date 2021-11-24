using Microsoft.AspNetCore.Mvc;
using Zoo_Management.Models;
using Zoo_Management.Models.Request;
using Zoo_Management.Models.Response;
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
        public IActionResult Create([FromBody] CreateSpeciesRequest speciesRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var species in _speciesRepo.GetAll())
            {
                if (species.SpeciesName.ToLower().Equals(speciesRequest.SpeciesName.ToLower()))
                {
                    return BadRequest(
                        $"A species with the name {speciesRequest.SpeciesName} already exists (id {species.SpeciesId})");
                }
            }
            var newSpecies = _speciesRepo.Create(speciesRequest);

            var url = Url.Action("GetAll");
            var responseModel = new SpeciesResponse(newSpecies);
            return Created(url, responseModel);   
        }
    }
}
