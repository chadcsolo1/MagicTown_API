using MagicTown_TownAPI.Data;
using MagicTown_TownAPI.Logging;
using MagicTown_TownAPI.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicTown_TownAPI.Controllers
{
    [Route("api/TownAPI")]
    [ApiController]
    public class TownAPIController : ControllerBase
    {
        private readonly ILogging _logger;
        public TownAPIController(ILogging logger)
        {
            _logger = logger;
        }

        [HttpGet("towns")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TownDTO>> GetTowns()
        {
            _logger.Log("Getting all Towns...", "info");
            return Ok(TownStore.townList);
        }

        [HttpGet("{id:int}", Name = "GetTowns")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type = typeof(TownDTO))]
        public ActionResult<TownDTO> GetTown(int id)
        {
            _logger.Log($"Retrieving Town with an id of : {id}", "info");
            if (id == 0) {return BadRequest("The id you provided was invalid."); _logger.Log("Id provided was invalid.", "error");}

            var town = TownStore.townList.FirstOrDefault(x => x.Id == id);

            if (town == null) {return NotFound(); _logger.Log($"Town with Id of : {id} was not found.", "error");}

            return Ok(town);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TownDTO> CreateTown([FromBody] TownDTO TownDTO) 
        {
            if (TownStore.townList.Exists(x => x.Name.ToLower() == TownDTO.Name.ToLower()))
            {
                ModelState.AddModelError("TownCreationError", "Town already exists!");
                return BadRequest(ModelState);
            }
            if (TownDTO == null) {return BadRequest(TownDTO);}
            if (TownDTO.Id > 0) {return BadRequest("Please leave the Id value as 0. This value is automatically generated");}

            TownDTO.Id = TownStore.townList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;

            TownStore.townList.Add(TownDTO);

            //If you want to return the location the resource was created you can use the implementation below
            // CreatedAtRoute returns a 201 created response code.
            return CreatedAtRoute("GetTowns", new { id = TownDTO.Id }, TownDTO);

            //You can also return a simple 200 Http Response
            //return Ok(TownDTO);
        }

        [HttpDelete("{id}", Name = "DeleteTown")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteTown(int id)
        {
            if (id == 0) {return BadRequest();}

            var town = TownStore.townList.FirstOrDefault(x => x.Id == id);

            if (town == null) { return NotFound("The Town you entered was not. No Town was deleted."); }

            TownStore.townList.Remove(town);
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
            var town = TownStore.townList.FirstOrDefault(x => x.Id == id);
            if (town == null) {return NotFound();}

            town.Name = townDTO.Name;
            town.Population = townDTO.Population;
            town.AverageIncome = townDTO.AverageIncome;

            return NoContent();
        }

        [HttpPatch("{id}", Name = "UpdatePartialTown")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialTown(int id, JsonPatchDocument<TownDTO> patchDTO)
        {
            if(patchDTO == null || id == 0) { return BadRequest(); }

            var town = TownStore.townList.FirstOrDefault(x => x.Id == id);

            if (town == null) {return NotFound("No Town matching the provided Id was found");}

            patchDTO.ApplyTo(town, ModelState);

            if (!ModelState.IsValid) { return BadRequest(); }
            return NoContent();
        }

    }
}
