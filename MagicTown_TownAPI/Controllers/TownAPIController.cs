using MagicTown_TownAPI.Data;
using MagicTown_TownAPI.Infastructure;
using MagicTown_TownAPI.Logging;
using MagicTown_TownAPI.Models;
using MagicTown_TownAPI.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicTown_TownAPI.Controllers
{
    [Route("api/TownAPI")]
    [ApiController]
    public class TownAPIController : ControllerBase
    {
        private readonly ILogging _logger;
        //private readonly ApplicationDbContext _context;
        //private readonly IRepository<Town> _repo;
        //private readonly IRepository<House> _houseRepo;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ITownRepo _townRepo;
        public TownAPIController(ILogging logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            //_repo = repo;
            //_houseRepo = houseRepo;
            //_townRepo = townRepo;
            _unitOfWork = unitOfWork;
            //_context = context;
            //_repo = repo;
            //_houseRepo = houseRepo;
        }

        [HttpGet("towns")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TownDTO>> GetTowns()
        {
            _logger.Log("Getting all Towns...", "info");
            //return Ok(_context.Towns.ToList());
            //return Ok(_repo.GetAll());
            return Ok(_unitOfWork.TownRepo.GetAll());
        }

        [HttpGet("{id:int}", Name = "GetTowns")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type = typeof(TownDTO))]
        public ActionResult<TownDTO> GetTown(int id)
        {
            _logger.Log($"Retrieving Town with an id of : {id}", "info");
            if (id == 0) {return BadRequest("The id you provided was invalid.");}

            //var town = _context.Towns.FirstOrDefault(x => x.Id == id);


            //if (town == null) {return NotFound(); _logger.Log($"Town with Id of : {id} was not found.", "error");}

            //return Ok(_repo.Get(id));
            return Ok(_unitOfWork.TownRepo.Get(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TownDTO> CreateTown([FromBody] TownDTO TownDTO) 
        {
            //if (_context.Towns.FirstOrDefault(x => x.Name.ToLower() == TownDTO.Name.ToLower()) != null)
            //{
            //    ModelState.AddModelError("TownCreationError", "Town already exists!");
            //    return BadRequest(ModelState);
            //}
            //if (TownDTO == null) {return BadRequest(TownDTO);}
            if (TownDTO.Id > 0) {return BadRequest("Town - Please leave the Id value as 0. This value is automatically generated");}

            //TownDTO.Id = _context.Towns.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;

            Town town = new Town() 
            {
                Name = TownDTO.Name,
                Description = TownDTO.Description,
                BiggestAttraction = TownDTO.BiggestAttraction,
                ImageUrl = TownDTO.ImageUrl,
                Population = TownDTO.Population,
                AverageIncome = TownDTO.AverageIncome,
            };

            //House house = new House()
            //{
            //    Name = "Test House",
            //    Description = "Testing Transactions House"
            //};
            //House house = null;

            //_context.Towns.Add(town);
            //_context.SaveChanges();

            //If you want to return the location the resource was created you can use the implementation below
            // CreatedAtRoute returns a 201 created response code.
            //return CreatedAtRoute("GetTowns", new { id = TownDTO.Id }, TownDTO);

            //You can also return a simple 200 Http Response
            //return Ok(TownDTO);

            _unitOfWork.TownRepo.Create(town);
            _unitOfWork.Save();
            //return CreatedAtRoute("createTown", new { id = TownDTO.Id }, TownDTO);
            return Ok($"{town.Name} was created.");
            //return Ok($"{town.Name} was created Successfully");
        }


        [HttpDelete("{id}", Name = "DeleteTown")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteTown(int id)
        {
            if (id == 0) {return BadRequest();}

            //var town = _context.Towns.FirstOrDefault(x => x.Id == id);
            var town = _unitOfWork.TownRepo.Get(id);

            if (town == null) { return NotFound("The Town you entered was not. No Town was deleted."); }

            _unitOfWork.TownRepo.Delete(town);
            _unitOfWork.Save();
            //_context.Remove(town);
            //_context.SaveChanges();
            //You can return Ok200 or NoContent204 either works
            return NoContent();
        }

        [HttpPut("{id}", Name = "UpdateTown")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateTown(int id, [FromBody] TownDTO townDTO)
        {
            if (townDTO == null || id != townDTO.Id) { return BadRequest(); }
            //var town = _context.Towns.FirstOrDefault(x => x.Id == id);

            var town = _unitOfWork.TownRepo.Get(id);
            if (town == null) {return NotFound();}

            town.Name = townDTO.Name;
            town.Description = townDTO.Description;
            town.BiggestAttraction = townDTO.BiggestAttraction;
            town.ImageUrl = townDTO.ImageUrl;
            town.Population = townDTO.Population;
            town.AverageIncome = townDTO.AverageIncome;

            _unitOfWork.TownRepo.Update(town);
            _unitOfWork.Save();
            //_context.Update(town);
            //_context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}", Name = "UpdatePartialTown")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialTown(int id, JsonPatchDocument<TownDTO> patchDTO)
        {
            if(patchDTO == null || id == 0) { return BadRequest(); }

            //var town = _context.Towns.AsNoTracking().FirstOrDefault(x => x.Id == id);

            var town = _unitOfWork.TownRepo.Get(id);

            if (town == null) {return NotFound("No Town matching the provided Id was found");}

            TownDTO townDTO = new TownDTO() 
            {
                Name = town.Name,
                Description = town.Description,
                BiggestAttraction = town.BiggestAttraction,
                ImageUrl = town.ImageUrl,
                Population = town.Population,
                AverageIncome = town.AverageIncome,
            };

            patchDTO.ApplyTo(townDTO, ModelState);

            Town townModel = new Town() 
            {
                Name = townDTO.Name,
                Description = townDTO.Description,
                BiggestAttraction = townDTO.BiggestAttraction,
                ImageUrl = townDTO.ImageUrl,
                Population = townDTO.Population,
                AverageIncome = townDTO.AverageIncome,
            };

            _unitOfWork.TownRepo.Update(townModel);

            //_context.Update(townModel);
            //_context.SaveChanges();

            if (!ModelState.IsValid) { return BadRequest(); }
            return NoContent();
        }


    }
}
