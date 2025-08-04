using ContextoDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContextoDB.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementsController : ControllerBase
    {
        private readonly BancaContext _context;

        public MovementsController(BancaContext context)
        {
            _context = context;
        }
        [HttpGet("User/{id_user}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Movement>>> GetMovementsbyUser(int id_user)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id_user);
            
            if (user == null)
            {
                return NotFound("No existe el usuario.");
            }

            var movements = await _context.Movements
                .Where(m => m.id_user == id_user)
                .ToListAsync();

            if(movements == null || !movements.Any())
            {
                return NotFound("No hay movimientos aún.");
            }



            return Ok(movements);
        }

        [HttpGet("Account/{id_account}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Movement>>> GetMovementsbyAccount(int id_account)
        {

            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id_account);

            if (account == null)
            {
                return NotFound("No existe la cuenta");
            }

            var movements = await _context.Movements
                .Where(m => m.origin == id_account)
                .ToListAsync();

            if (movements == null || !movements.Any())
            {
                return NotFound("No hay movimientos aún.");
            }

            return Ok(movements);
        }
    }
}
