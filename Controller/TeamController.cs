using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceApi.Data;
using EcommerceApi.Models;
using EcommerceApi.DTOs;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeamController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Team
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetTeamMembers()
        {
            return await _context.TeamMembers
                .Select(t => new TeamDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Role = t.Role,
                    Photo = t.Photo,
                    Description = t.Description,
                    CreatedAt = t.CreatedAt
                })
                .ToListAsync();
        }

        // GET: api/Team/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDto>> GetTeamMember(int id)
        {
            var teamMember = await _context.TeamMembers.FindAsync(id);

            if (teamMember == null)
            {
                return NotFound();
            }

            return new TeamDto
            {
                Id = teamMember.Id,
                Name = teamMember.Name,
                Role = teamMember.Role,
                Photo = teamMember.Photo,
                Description = teamMember.Description,
                CreatedAt = teamMember.CreatedAt
            };
        }

        // POST: api/Team
        [HttpPost]
        public async Task<ActionResult<TeamDto>> CreateTeamMember(CreateTeamDto createTeamDto)
        {
            var teamMember = new Team
            {
                Name = createTeamDto.Name,
                Role = createTeamDto.Role,
                Photo = createTeamDto.Photo,
                Description = createTeamDto.Description,
                CreatedAt = DateTime.UtcNow
            };

            _context.TeamMembers.Add(teamMember);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTeamMember), new { id = teamMember.Id }, new TeamDto
            {
                Id = teamMember.Id,
                Name = teamMember.Name,
                Role = teamMember.Role,
                Photo = teamMember.Photo,
                Description = teamMember.Description,
                CreatedAt = teamMember.CreatedAt
            });
        }

        // PUT: api/Team/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeamMember(int id, UpdateTeamDto updateTeamDto)
        {
            var teamMember = await _context.TeamMembers.FindAsync(id);
            if (teamMember == null)
            {
                return NotFound();
            }

            teamMember.Name = updateTeamDto.Name ?? teamMember.Name;
            teamMember.Role = updateTeamDto.Role ?? teamMember.Role;
            teamMember.Photo = updateTeamDto.Photo ?? teamMember.Photo;
            teamMember.Description = updateTeamDto.Description ?? teamMember.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamMemberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Team/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeamMember(int id)
        {
            var teamMember = await _context.TeamMembers.FindAsync(id);
            if (teamMember == null)
            {
                return NotFound();
            }

            _context.TeamMembers.Remove(teamMember);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeamMemberExists(int id)
        {
            return _context.TeamMembers.Any(e => e.Id == id);
        }
    }
}