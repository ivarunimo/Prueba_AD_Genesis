using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContextoDB;
using ContextoDB.Tables;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly BancaContext _context;

        public AccountsController(BancaContext context)
        {
            _context = context;
        }

        // Solo para revisar todas las cuentas
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            return await _context.Accounts.ToListAsync();
        }

        // Obtiene cuentas por el ID del usuario
        [Authorize]
        [HttpGet("{id_user}")]
        public async Task<ActionResult<Account>> GetAccount(int id_user)
        {

            var account = await _context.Accounts.Where(a => a.id_user == id_user).ToListAsync();

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }


        // Obtiene cuentas por el ID del usuario
        [HttpGet("GetAccount/{id_account}")]
        public async Task<ActionResult<Account>> GetAccountById(int id_account)
        {

            var account = await _context.Accounts.FindAsync(id_account);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }


        // POST: api/Accounts
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = account.Id }, account);
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
