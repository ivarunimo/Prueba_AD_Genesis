using ContextoDB;
using ContextoDB.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransfersController : ControllerBase
    {
        private readonly BancaContext _context;

        public TransfersController(BancaContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Transfer>> CreateTransfer(Transfer transferencia)
        {
            var cuentaOrigen = await _context.Accounts.FindAsync(transferencia.origin);
            var cuentaDestino = await _context.Accounts.FindAsync(transferencia.destiny);

            if (cuentaOrigen == null || cuentaDestino == null)
            {
                return NotFound("Cuenta de origen o destino no encontrada.");
            }

            if (cuentaOrigen.balance < transferencia.amount)
            {
                return BadRequest("Saldo insuficiente en la cuenta de origen para realizar la transferencia.");
            }

            cuentaDestino.balance += transferencia.amount;
            cuentaOrigen.balance -= transferencia.amount;
            

            _context.Entry(cuentaOrigen).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.Entry(cuentaDestino).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.Transfers.Add(transferencia);
            


            // Registrar el movimiento de la transferencia

            var movimientoOrigen = new Movement
            {
                id_user = cuentaOrigen.id_user,
                origin = cuentaOrigen.Id,
                description = $"Transferencia enviada a cuenta {cuentaDestino.Id}: "+ transferencia.description,
                amount = -transferencia.amount,
                type = 2,
                kind = 1,
                fecha = DateTime.UtcNow
            };
            var movimientoDestino = new Movement
            {
                id_user = cuentaDestino.id_user,
                origin = cuentaDestino.Id,
                description = $"Transferencia recibida de cuenta {cuentaOrigen.Id}: " + transferencia.description,
                amount = transferencia.amount,
                type = 1,
                kind = 1,
                fecha = DateTime.UtcNow
            };
            _context.Movements.Add(movimientoOrigen);

            _context.Movements.Add(movimientoDestino);


            await _context.SaveChangesAsync();



            return CreatedAtAction(nameof(CreateTransfer),transferencia);
        }

    }
}
