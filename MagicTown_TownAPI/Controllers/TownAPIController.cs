using MagicTown_TownAPI.Data;
using MagicTown_TownAPI.Infastructure;
using MagicTown_TownAPI.Logging;
using MagicTown_TownAPI.Models;
using MagicTown_TownAPI.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicTown_TownAPI.Controllers
{
    [Route("api/TownAPI")]
    [ApiController]
    public class TownAPIController : ControllerBase
    {
        private readonly ILogging _logger;
        private readonly IUnitOfWork _unitOfWork;

        public TownAPIController(ILogging logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("towns")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TownDTO>> GetTowns()
        {
            _logger.Log("Getting all Towns...", "info");
            return Ok(_unitOfWork.TownRepo.GetAll(filter: f => f.AverageIncome > 1500.00 && f.AverageIncome < 5000.00, orderBy: o => o.OrderBy(p => p.Population), pageNumber: 1, pageSize: 3));
            //return Ok(_unitOfWork.TownRepo.GetAll(query));
            _logger.Log("All Towns were retrieved.", "info");
        }

        [HttpGet("{id:int}", Name = "GetTowns")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TownDTO> GetTown(int id)
        {
            _logger.Log($"Retrieving Town with an id of : {id}", "info");
            if (id == 0) 
            {
                _logger.Log("An invalid Id was provided.", "error");
                return BadRequest("The id you provided was invalid.");
            }
            _logger.Log($"Town with an id of : {id} was found.", "info");
            return Ok(_unitOfWork.TownRepo.Get(id));
        }

        [HttpPost("createtown")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TownDTO> CreateTown([FromBody] TownDTO TownDTO) 
        {
            if (TownDTO == null) 
            {
                _logger.Log("TownDTO provided was null.", "error");
                return BadRequest(TownDTO);
            }
            if (TownDTO.Id > 0) 
            {
                _logger.Log("An id was manually provided when trying to create a resource.", "error");
                return BadRequest("Town - Please leave the Id value as 0. This value is automatically generated");
            }

            Town town = new Town() 
            {
                Name = TownDTO.Name,
                Description = TownDTO.Description,
                BiggestAttraction = TownDTO.BiggestAttraction,
                ImageUrl = TownDTO.ImageUrl,
                Population = TownDTO.Population,
                AverageIncome = TownDTO.AverageIncome,
            };


            _unitOfWork.TownRepo.Create(town);
            _unitOfWork.Save();
            _logger.Log($"A town with a name of : {town.Name} was successfully created", "info");
            return Ok($"{town.Name} was created.");
        }


        [HttpDelete("{id}", Name = "DeleteTown")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteTown(int id)
        {
            if (id == 0) 
            {
                _logger.Log("No id was provided so that a resource could be identified for deletion.", "error");
                return BadRequest();
            }

            var town = _unitOfWork.TownRepo.Get(id);
            _logger.Log($"Town with a name of : {town.Name} was identified for deletion.", "info");

            if (town == null) 
            {
                _logger.Log($"town was null and therfore can not be deleted.", "error");
                return NotFound("The Town you entered was not. No Town was deleted."); 
            }

            _unitOfWork.TownRepo.Delete(town);
            _unitOfWork.Save();
            _logger.Log($"Town with a name of : {town.Name} was successfully deleted.", "info");

            return NoContent();
        }

        [HttpPut("{id}", Name = "UpdateTown")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateTown(int id, [FromBody] TownDTO townDTO)
        {
            if (townDTO == null || id != townDTO.Id) 
            {
                _logger.Log("townDTO provided was either null or not correctly identified.", "error");
                return BadRequest(); 
            }

            var town = _unitOfWork.TownRepo.Get(id);
     
            if (town == null) 
            {
                _logger.Log("Town was null when attempting to update.", "error");
                return NotFound();
            }

            town.Name = townDTO.Name;
            town.Description = townDTO.Description;
            town.BiggestAttraction = townDTO.BiggestAttraction;
            town.ImageUrl = townDTO.ImageUrl;
            town.Population = townDTO.Population;
            town.AverageIncome = townDTO.AverageIncome;

            _unitOfWork.TownRepo.Update(town);
            _unitOfWork.Save();
            _logger.Log($"{town.Name} was successfully updated.", "info");

            return NoContent();
        }

        [HttpPatch("{id}", Name = "UpdatePartialTown")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialTown(int id, JsonPatchDocument<TownDTO> patchDTO)
        {
            if(patchDTO == null || id == 0) 
            {
                _logger.Log("Either the id provided was zero or the patchDTO was null.", "error");
                return BadRequest(); 
            }

            var town = _unitOfWork.TownRepo.Get(id);

            if (town == null) 
            {
                _logger.Log("Town was null when attempting to partial update.", "error");
                return NotFound("No Town matching the provided Id was found");
            }

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
            _unitOfWork.Save();
            _logger.Log($"{townModel.Name} was successfully updated.", "info");

            if (!ModelState.IsValid) { return BadRequest(); }
            return NoContent();
        }


    }
}
