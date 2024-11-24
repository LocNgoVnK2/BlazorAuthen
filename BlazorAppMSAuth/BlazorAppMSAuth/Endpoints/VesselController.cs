using BazorAppMSAuth.Share.Interfaces.ServicesInterfaces;
using BazorAppMSAuth.Share.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppMSAuth.Endpoints
{
        [Route("api/[controller]")]
        [ApiController]
        public class VesselController : ControllerBase
        {
            private readonly IVesselService _vesselService;

            public VesselController(IVesselService vesselService)
            {
                _vesselService = vesselService;
            }

            // GET api/vessel 
            [HttpGet]
            [AllowAnonymous] 
            public async Task<IActionResult> GetAllVessels()
            {
                var vessels = await _vesselService.GetAllVesselsAsync();
                return Ok(vessels);
            }

            // GET api/vessel/{id} 
            [HttpGet("{id}")]
            [AllowAnonymous]
            public async Task<IActionResult> GetVesselById(int id)
            {
                var vessel = await _vesselService.GetVesselByIdAsync(id);
                if (vessel == null)
                {
                    return NotFound();
                }
                return Ok(vessel);
            }

            // POST api/vessel 
            [HttpPost]
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> AddVessel([FromBody] Vessel vessel)
            {
                await _vesselService.AddVesselAsync(vessel);
                return CreatedAtAction(nameof(GetVesselById), new { id = vessel.Id }, vessel);
            }

            // PUT api/vessel/{id}
            [HttpPut("{id}")]
            [Authorize(Roles = "Admin")] 
            public async Task<IActionResult> UpdateVessel(int id, [FromBody] Vessel vessel)
            {
                if (id != vessel.Id)
                {
                    return BadRequest("ID mismatch.");
                }

                var existingVessel = await _vesselService.GetVesselByIdAsync(id);
                if (existingVessel == null)
                {
                    return NotFound();
                }

                await _vesselService.UpdateVesselAsync(vessel);
                return NoContent();
            }

            // DELETE api/vessel/{id} 
            [HttpDelete("{id}")]
            [Authorize(Roles = "Admin")] 
            public async Task<IActionResult> DeleteVessel(int id)
            {
                var vessel = await _vesselService.GetVesselByIdAsync(id);
                if (vessel == null)
                {
                    return NotFound();
                }

                await _vesselService.DeleteVesselAsync(id);
                return NoContent();
            }
        }
    }


