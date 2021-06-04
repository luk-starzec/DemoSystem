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
    public class TitleController : ControllerBase
    {
        private readonly PrioritySetterContext _context;
        private readonly ILogger<TitleController> _logger;

        public TitleController(PrioritySetterContext context, ILogger<TitleController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TitlePriorityModel>>> GetTitlePriority()
        {
            var entities = await _context.TitlePriority.ToListAsync();
            return entities.Select(r => r.ToModel()).ToList();
        }

        [HttpGet("{title}")]
        public async Task<ActionResult<TitlePriorityModel>> GetTitlePriority(string title)
        {
            var entity = await _context.TitlePriority.FindAsync(title);

            if (entity == null)
                return NotFound();

            return entity.ToModel();
        }

        [HttpPut("{title}")]
        public async Task<IActionResult> PutTitlePriority(string title, [FromBody] int priorityId)
        {
            var entity = await _context.TitlePriority.FindAsync(title);

            if (entity is null)
                return NotFound();

            if (!CheckPriority(priorityId))
                return BadRequest();

            entity.PriorityLevel = (EnumPriorityLevel)priorityId;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TitlePriorityModel>> PostTitlePriority(TitlePriorityModel errorPriority)
        {
            if (string.IsNullOrWhiteSpace(errorPriority.Title) || !CheckPriority(errorPriority.PriorityLevelId))
                return BadRequest();

            var entity = errorPriority.ToEntity();
            _context.TitlePriority.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TitlePriorityExists(errorPriority.Title))
                    return Conflict();

                throw;
            }

            return CreatedAtAction(nameof(GetTitlePriority), new { error = errorPriority.Title }, errorPriority);
        }

        [HttpDelete("{title}")]
        public async Task<IActionResult> DeleteTitlePriority(string title)
        {
            var errorPriority = await _context.TitlePriority.FindAsync(title);
            if (errorPriority == null)
                return NotFound();

            _context.TitlePriority.Remove(errorPriority);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TitlePriorityExists(string title)
        {
            return _context.TitlePriority.Any(e => e.Title == title);
        }

        private bool CheckPriority(int priorityId)
        {
            return Enum.GetValues(typeof(EnumPriorityLevel)).Cast<EnumPriorityLevel>()
                .Any(r => priorityId == (int)r);
        }
    }
}
