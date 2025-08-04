using Backend.Services;
using ContextoDB;
using ContextoDB.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CreacionEjemplos :ControllerBase
    {

        private readonly BancaContext _context;
        public CreacionEjemplos(BancaContext context)
        {
            _context = context;
        }

        [HttpPost("CreateUsers")]
        public async Task<ActionResult<User>> CreateUserExamples()
        {
            User user = new User()
            {
                email = "AlexanderMendez@gmail.com",
                fullName = "Alexander Mendez Morales",
                username = "AMM_35",
                password = "AlexM_9999"
            };

            var hashedPassword = Encrypt.HashPassword(user.password); ;
            user.password = hashedPassword;
            User user2 = new User()
            {
                email = "OlivioOrantes@gmail.com",
                fullName = "Olivio Orantes Casablanca",
                username = "Olivio_Orantes",
                password = "Casablanca999_"
            };
            var hashedPassword1 = Encrypt.HashPassword(user2.password);

            user2.password = hashedPassword1;
            _context.Users.Add(user);
            _context.Users.Add(user2);

            await _context.SaveChangesAsync();



            return Ok("Usuarios de ejemplo creados,"+ user.username + user2.username);
        }

        [HttpPost("CreateCards")]
        public async Task<ActionResult<DebitCard>> CreateDebitCardExamples()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.username == "AMM_35");
            var user1 = await _context.Users.FirstOrDefaultAsync(u => u.username == "Olivio_Orantes");
            if (user == null || user1 == null)
                return BadRequest("Usuarios no encontrados.");
            var cuenta1 = await _context.Accounts.FirstOrDefaultAsync(a => a.id_user == user.Id);
            var cuenta2 = await _context.Accounts.Where(a => a.number == "678-1203-97").FirstOrDefaultAsync();
            var cuenta3 = await _context.Accounts.Where(a => a.number == "491-6532-08").FirstOrDefaultAsync(); 
            var cuenta4 = await _context.Accounts.Where(a => a.number == "847-2901-34").FirstOrDefaultAsync();

            if (cuenta1 == null || cuenta2 == null)
                return BadRequest("Cuentas no encontradas.");
            var tarjeta1 = new DebitCard
            {
                number = "1234567812345678",
                active = true,
                id_account = cuenta1.Id,
                type = "Visa",
                expDate = "12/25",
                name_card = "Alexander Mendez Morales"
            };
            var tarjeta2 = new DebitCard
            {
                number = "9977541234965170",
                active = true,
                id_account = cuenta2.Id,
                type = "Master Card",
                expDate = "03/26",
                name_card = "Morales"
            };
            var tarjeta3 = new DebitCard
            {
                number = "6547889741203214",
                active = true,
                id_account = cuenta3.Id,
                type = "American Express",
                expDate = "12/24",
                name_card = "Alex"
            };
            var tarjeta4 = new DebitCard
            {
                number = "6543210987654321",
                active = true,
                id_account = cuenta4.Id,
                type = "MasterCard",
                expDate = "08/26",
                name_card = "Jojo"
            };
            _context.DebitCard.Add(tarjeta1);
            _context.DebitCard.Add(tarjeta2);
            _context.DebitCard.Add(tarjeta3);
            _context.DebitCard.Add(tarjeta4);
            await _context.SaveChangesAsync();
            return Ok("Tarjetas de débito de ejemplo creadas.");
        }

        [HttpPost("CreateAccount")]
        public async Task<ActionResult<Account>> CreateAccountExamples()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.username == "AMM_35");
            var user1 = await _context.Users.FirstOrDefaultAsync(u => u.username == "Olivio_Orantes");


            var acc1User1 = new Account
            {
                number = "847-2901-34",
                balance = 25000.00,
                account_name = "Cuenta Nómina",
                id_user = user1.Id
            };

            var acc2User1 = new Account
            {
                number = "560-8745-12",
                balance = 1000,
                account_name = "Cuenta Ahorro",
                id_user = user1.Id
            };

            var acc3User = new Account
            {
                number = "739-0012-67",
                balance = 25000.00,
                account_name = "Cuenta Para el banco",
                id_user = user.Id
            };

            var acc4User = new Account
            {
                number = "678-1203-97",
                balance = 4000,
                account_name = "Cuenta Plazo Fijo",
                id_user = user.Id
            };

            var acc1User = new Account
            {
                number = "491-6532-08",
                balance = 5000.50,
                account_name = "Cuenta Movimiento Lento",
                id_user = user.Id
            };

            var acc2User = new Account
            {
                number = "330-7777-71",
                balance = 20000,
                account_name = "Mi cuenta",
                id_user = user.Id
            };

            _context.Accounts.Add(acc1User1);
            _context.Accounts.Add(acc2User1);

            _context.Accounts.Add(acc3User);
            _context.Accounts.Add(acc4User);

            _context.Accounts.Add(acc1User);
            _context.Accounts.Add(acc2User);

            await _context.SaveChangesAsync();



            return Ok();
        }

        [HttpPost("CreateTransfersMovements")]
        public async Task<ActionResult<DebitCard>> CreateTransfersAndMovements()
        {
            // Obtener cuentas de ejemplo
            var cuentaOrigen = await _context.Accounts.FirstOrDefaultAsync(a => a.number == "847-2901-34");
            var cuentaDestino = await _context.Accounts.FirstOrDefaultAsync(a => a.number == "560-8745-12");

            if (cuentaOrigen == null || cuentaDestino == null)
                return BadRequest("No se encontraron las cuentas de ejemplo.");

            // Crear una transferencia de ejemplo
            var transferencia = new Transfer
            {
                origin = cuentaOrigen.Id,
                destiny = cuentaDestino.Id,
                description = "Transferencia de ejemplo de Pyme a Nómina",
                amount = 1500.00
            };
            _context.Transfers.Add(transferencia);
            cuentaOrigen.balance -= transferencia.amount;
            cuentaDestino.balance += transferencia.amount;

            _context.Entry(cuentaOrigen).State = EntityState.Modified;
            _context.Entry(cuentaDestino).State = EntityState.Modified;


            // Crear movimientos asociados a la transferencia
            var movimientoOrigen = new Movement
            {
                origin = cuentaOrigen.Id,
                description = "Pago de servicios",
                amount = 1500.00,
                fecha = DateTime.UtcNow,
                type = 2,
                kind = 1,
                id_user = cuentaOrigen.id_user
            };

            var movimientoDestino = new Movement
            {
                origin = cuentaDestino.Id,
                description = "Ingreso por transferencia desde Pyme",
                amount = 1500.00,
                fecha = DateTime.UtcNow,
                type = 1,
                kind = 1,
                id_user = cuentaDestino.id_user
            };



            var movimientoOrigen1 = new Movement
            {
                origin = cuentaOrigen.Id,
                description = "DEPOSITO BANCA",
                amount = 200.00,
                fecha = DateTime.UtcNow,
                type = 1,
                kind = 3,
                id_user = cuentaOrigen.id_user
            };

            var movimientoOrigen2 = new Movement
            {
                origin = cuentaOrigen.Id,
                description = "Pago de servicios",
                amount = 50.00,
                fecha = DateTime.UtcNow,
                type = 2,
                kind = 4,
                id_user = cuentaOrigen.id_user
            };


            var movimientoDestino1 = new Movement
            {
                origin = cuentaDestino.Id,
                description = "Compra en línea",
                amount = 78.50,
                fecha = DateTime.UtcNow,
                type = 2,
                kind = 5,
                id_user = cuentaDestino.id_user
            };

            var movimientoDestino2 = new Movement
            {
                origin = cuentaDestino.Id,
                description = "Retiro",
                amount = 100,
                fecha = DateTime.UtcNow,
                type = 2,
                kind = 2,
                id_user = cuentaDestino.id_user
            };

            var movimientoDestino3 = new Movement
            {
                origin = cuentaOrigen.Id,
                description = "Retiro",
                amount = 500,
                fecha = DateTime.UtcNow,
                type = 2,
                kind = 2,
                id_user = cuentaOrigen.id_user
            };




            _context.Movements.Add(movimientoOrigen);
            _context.Movements.Add(movimientoOrigen1);
            _context.Movements.Add(movimientoOrigen2);
            _context.Movements.Add(movimientoDestino);
            _context.Movements.Add(movimientoDestino1);
            _context.Movements.Add(movimientoDestino2);
            _context.Movements.Add(movimientoDestino3);

            await _context.SaveChangesAsync();

            return Ok(new { transferencia, movimientoOrigen, movimientoDestino });

        }

        [HttpPost("CreateTransfersMovements2")]
        public async Task<ActionResult<DebitCard>> CreateTransfersAndMovements2()
        {
            // Obtener cuentas de ejemplo
            var cuentaOrigen = await _context.Accounts.FirstOrDefaultAsync(a => a.number == "678-1203-97");
            var cuentaDestino = await _context.Accounts.FirstOrDefaultAsync(a => a.number == "491-6532-08");

            if (cuentaOrigen == null || cuentaDestino == null)
                return BadRequest("No se encontraron las cuentas de ejemplo.");

            // Crear una transferencia de ejemplo
            var transferencia = new Transfer
            {
                origin = cuentaOrigen.Id,
                destiny = cuentaDestino.Id,
                description = "Transferencia de ejemplo de Pyme a Nómina",
                amount = 700.00
            };
            _context.Transfers.Add(transferencia);
            cuentaOrigen.balance -= transferencia.amount;
            cuentaDestino.balance += transferencia.amount;

            _context.Entry(cuentaOrigen).State = EntityState.Modified;
            _context.Entry(cuentaDestino).State = EntityState.Modified;


            // Crear movimientos asociados a la transferencia
            var movimientoOrigen = new Movement
            {
                origin = cuentaOrigen.Id,
                description = "Movimiento normal",
                amount = 700.00,
                fecha = DateTime.UtcNow,
                type = 2,
                kind = 1,
                id_user = cuentaOrigen.id_user
            };

            var movimientoDestino = new Movement
            {
                origin = cuentaDestino.Id,
                description = "Ahorro",
                amount = 700.00,
                fecha = DateTime.UtcNow,
                type = 1,
                kind = 1,
                id_user = cuentaDestino.id_user
            };



            var movimientoOrigen1 = new Movement
            {
                origin = cuentaOrigen.Id,
                description = "DEPOSITO",
                amount = 200.00,
                fecha = DateTime.UtcNow,
                type = 1,
                kind = 3,
                id_user = cuentaOrigen.id_user
            };

            var movimientoOrigen2 = new Movement
            {
                origin = cuentaOrigen.Id,
                description = "Pago de tarjeta",
                amount = 50.00,
                fecha = DateTime.UtcNow,
                type = 2,
                kind = 4,
                id_user = cuentaOrigen.id_user
            };


            var movimientoDestino1 = new Movement
            {
                origin = cuentaDestino.Id,
                description = "Compra en supermercado",
                amount = 78.50,
                fecha = DateTime.UtcNow,
                type = 2,
                kind = 5,
                id_user = cuentaDestino.id_user
            };

            var movimientoDestino2 = new Movement
            {
                origin = cuentaDestino.Id,
                description = "Retiro",
                amount = 100,
                fecha = DateTime.UtcNow,
                type = 2,
                kind = 2,
                id_user = cuentaDestino.id_user
            };

            var movimientoDestino3 = new Movement
            {
                origin = cuentaOrigen.Id,
                description = "Movimiento",
                amount = 500,
                fecha = DateTime.UtcNow,
                type = 2,
                kind = 2,
                id_user = cuentaOrigen.id_user
            };




            _context.Movements.Add(movimientoOrigen);
            _context.Movements.Add(movimientoOrigen1);
            _context.Movements.Add(movimientoOrigen2);
            _context.Movements.Add(movimientoDestino);
            _context.Movements.Add(movimientoDestino1);
            _context.Movements.Add(movimientoDestino2);
            _context.Movements.Add(movimientoDestino3);

            await _context.SaveChangesAsync();

            return Ok(new { transferencia, movimientoOrigen, movimientoDestino });

        }


    }
}
