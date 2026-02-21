

using Microsoft.AspNetCore.Mvc;
using CuentaApi.Models;

namespace CuentaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        private static List<Cuenta> cuentas = new List<Cuenta>();

        private static int siguienteId = 1;  

        [HttpGet]
        public ActionResult<List<Cuenta>> ObtenerTodas()
        {
            return Ok(cuentas);
        }   

        [HttpPost]
        public ActionResult<Cuenta> Crear(Cuenta nuevaCuenta)
        {
            nuevaCuenta.Id = siguienteId++;   
            cuentas.Add(nuevaCuenta);

            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaCuenta.Id }, nuevaCuenta);
        }

        [HttpGet("{id}")]
        public ActionResult<Cuenta> ObtenerPorId(int id)
        {
            var cuenta = cuentas.FirstOrDefault(c => c.Id == id);

            if (cuenta == null)
                return NotFound();

            return Ok(cuenta);
        }
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
        var cuenta = cuentas.FirstOrDefault(c => c.Id == id);

        if (cuenta == null)
        return NotFound();

        cuentas.Remove(cuenta);

        return NoContent();
        }
       [HttpPut("{id}")]
        public ActionResult<Cuenta> Actualizar(int id, CuentaUpdateDto datos)
        {
            var cuenta = cuentas.FirstOrDefault(c => c.Id == id);

            if (cuenta == null)
                return NotFound();

            cuenta.Titular = datos.Titular;
            cuenta.Saldo = datos.Saldo;

            return Ok(cuenta);
        }
        [HttpPost("{id}/depositar")]
        public IActionResult Depositar(int id, decimal monto)
        {
            var cuenta = cuentas.FirstOrDefault(c => c.Id == id);

            if (cuenta == null)
                return NotFound();

            if (monto <= 0)
                return BadRequest("El monto debe ser mayor que cero.");

            cuenta.Saldo += monto;

            return Ok(cuenta);
        }
        [HttpPost("{id}/retirar")]
        public IActionResult Retirar(int id, decimal monto)
        {
            var cuenta = cuentas.FirstOrDefault(c => c.Id == id);

            if (cuenta == null)
                return NotFound();

            if (monto <= 0)
                return BadRequest("El monto debe ser mayor que cero.");

            if (cuenta.Saldo < monto)
                return BadRequest("Saldo insuficiente.");

            cuenta.Saldo -= monto;

            return Ok(cuenta);
        }
    }
}
