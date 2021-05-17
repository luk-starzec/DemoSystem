using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PrioritySetter.Data;
using PrioritySetter.Helpers;
using PrioritySetter.Models;

namespace PrioritySetter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        private readonly PrioritySetterContext _context;
        private readonly ILogger<ErrorsController> _logger;

        public ErrorsController(PrioritySetterContext context, ILogger<ErrorsController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ErrorPriorityModel>>> GetErrorPriority()
        {
            var entities = await _context.ErrorPriority.ToListAsync();
            return entities.Select(r => r.ToModel()).ToList();
        }

        [HttpGet("{error}")]
        public async Task<ActionResult<ErrorPriorityModel>> GetErrorPriority(string error)
        {
            var entity = await _context.ErrorPriority.FindAsync(error);

            if (entity == null)
                return NotFound();

            return entity.ToModel();
        }

        [HttpPut("{error}")]
        public async Task<IActionResult> PutErrorPriority(string error, int priorityId)
        {
            var entity = await _context.ErrorPriority.FindAsync(error);

            if (entity is null)
                return NotFound();

            if (!CheckPriority(priorityId))
                return BadRequest();

            entity.PriorityLevel = (EnumPriorityLevel)priorityId;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ErrorPriorityModel>> PostErrorPriority(ErrorPriorityModel errorPriority)
        {
            if (string.IsNullOrWhiteSpace(errorPriority.Error) || !CheckPriority(errorPriority.PriorityLevelId))
                return BadRequest();

            var entity = errorPriority.ToEntity();
            _context.ErrorPriority.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ErrorPriorityExists(errorPriority.Error))
                    return Conflict();

                throw;
            }

            return CreatedAtAction("GetErrorPriority", new { id = errorPriority.Error }, errorPriority);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteErrorPriority(string error)
        {
            var errorPriority = await _context.ErrorPriority.FindAsync(error);
            if (errorPriority == null)
                return NotFound();

            _context.ErrorPriority.Remove(errorPriority);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ErrorPriorityExists(string error)
        {
            return _context.ErrorPriority.Any(e => e.Error == error);
        }

        private bool CheckPriority(int priorityId)
        {
            return Enum.GetValues(typeof(EnumPriorityLevel)).Cast<EnumPriorityLevel>()
                .Any(r => priorityId == (int)r);
        }
    }
}
