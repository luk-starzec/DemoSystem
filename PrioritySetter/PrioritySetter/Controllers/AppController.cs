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
    public class AppController : ControllerBase
    {
        private readonly PrioritySetterContext _context;
        private readonly ILogger<AppController> _logger;

        public AppController(PrioritySetterContext context, ILogger<AppController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppPriorityModel>>> GetAppPriority()
        {
            var entities = await _context.AppPriority.ToListAsync();
            return entities.Select(r => r.ToModel()).ToList();
        }

        [HttpGet("{app}")]
        public async Task<ActionResult<AppPriorityModel>> GetAppPriority(string app)
        {
            var entity = await _context.AppPriority.FindAsync(app);

            if (entity == null)
                return NotFound();

            return entity.ToModel();
        }

        [HttpPut("{app}")]
        public async Task<IActionResult> PutAppPriority(string app, [FromBody] int priorityId)
        {
            var entity = await _context.AppPriority.FindAsync(app);

            if (entity is null)
                return NotFound();

            if (!CheckPriority(priorityId))
                return BadRequest();

            entity.PriorityLevel = (EnumPriorityLevel)priorityId;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<AppPriorityModel>> PostAppPriority(AppPriorityModel appPriority)
        {
            if (string.IsNullOrWhiteSpace(appPriority.App) || !CheckPriority(appPriority.PriorityLevelId))
                return BadRequest();

            var entity = appPriority.ToEntity();
            _context.AppPriority.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AppPriorityExists(appPriority.App))
                    return Conflict();

                throw;
            }

            return CreatedAtAction(nameof(GetAppPriority), new { app = appPriority.App }, appPriority);
        }

        [HttpDelete("{app}")]
        public async Task<IActionResult> DeleteAppPriority(string app)
        {
            var appPriority = await _context.AppPriority.FindAsync(app);
            if (appPriority == null)
                return NotFound();

            _context.AppPriority.Remove(appPriority);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppPriorityExists(string app)
        {
            return _context.AppPriority.Any(e => e.App == app);
        }

        private bool CheckPriority(int priorityId)
        {
            return Enum.GetValues(typeof(EnumPriorityLevel)).Cast<EnumPriorityLevel>()
                .Any(r => priorityId == (int)r);
        }

    }
}
