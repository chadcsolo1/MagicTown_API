using MagicTown_TownAPI.Data;
using MagicTown_TownAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MagicTown_TownAPI.Controllers
{
    [Route("api/TownAPI")]
    [ApiController]
    public class TownAPIController : ControllerBase
    {
        [HttpGet("towns")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TownDTO>> GetTowns()
        {
            return Ok(TownStore.townList);
        }

        [HttpGet("{id:int}", Name = "GetTowns")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type = typeof(TownDTO))]
        public ActionResult<TownDTO> GetTown(int id)
        {
            if (id == 0) {return BadRequest("The id you provided was invalid.");}

            var town = TownStore.townList.FirstOrDefault(x => x.Id == id);

            if (town == null) {return NotFound();}

            return Ok(town);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TownDTO> CreateTown([FromBody] TownDTO TownDTO) 
        {
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
    }
}
