using ContextoDB;
using ContextoDB.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebitCardController : ControllerBase
    {
        private readonly BancaContext _context;
        public DebitCardController(BancaContext context)
        {
            _context = context;
        }

        [HttpGet("{id_user}")]
        [Authorize]
        public async Task<ActionResult<DebitCard>> GetDebitCard(int id_user)
        {
            var accounts = await _context.Accounts.Where(a => a.id_user == id_user).ToListAsync();

            List<DebitCard> debitList = new List<DebitCard>();

            foreach (var account in accounts)
            {
                var debitCards = await _context.DebitCard.Where(d => d.id_account == account.Id).ToListAsync();

                debitList.AddRange(debitCards);
            }

            if (debitList.Count == 0)
            {
                return NotFound();
            }
            return Ok(debitList);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<DebitCard>> CreateDebitCard(DebitCard debitCard)
        {
            var account = await _context.Accounts.FindAsync(debitCard.id_account);
            if (account == null)
            {
                return NotFound("Cuenta no encontrada.");
            }
            // Verificar si ya existe una tarjeta de débito para esta cuenta
            var existingCard = await _context.DebitCard.FirstOrDefaultAsync(d => d.id_account == debitCard.id_account);
            if (existingCard != null)
            {
                return BadRequest("Ya existe una tarjeta de débito para esta cuenta.");
            }
            _context.DebitCard.Add(debitCard);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDebitCard), new { id_user = account.id_user }, debitCard);
        }
    }
}
