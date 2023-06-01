using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gestion_Gastos.Models;

namespace Gestion_Gastos.Controllers
{
    public class TransaccionController : Controller
    {
        private readonly AplicacionDbContext _context;

        public TransaccionController(AplicacionDbContext context)
        {
            _context = context;
        }

        // GET: Transaccion
        public async Task<IActionResult> Index()
        {
            var aplicacionDbContext = _context.Transacciones.Include(t => t.Categoria);

            return View(await aplicacionDbContext.ToListAsync()); 
        }

        // GET: Transaccion/CrearEditar
        public IActionResult CrearEditar(int id = 0)
        {
            AgruparCategorias();

            if (id == 0)
                return View(new Transaccion());
            
            else
                return View(_context.Transacciones.Find(id));
        }

        // POST: Transaccion/CrearEditar
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearEditar([Bind("IdTransaccion, IdCategoria, Cantidad, Notas, Fecha")] Transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                if (transaccion.IdTransaccion == 0)
                {
                    _context.Add(transaccion);
                }
                else
                    _context.Update(transaccion);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            AgruparCategorias();
            return View(transaccion);
        }

        // POST: Transaccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transacciones == null)
            {
                return Problem("Entity set 'AplicacionDbContext.Transacciones'  is null.");
            }
            var transaccion = await _context.Transacciones.FindAsync(id);
            if (transaccion != null)
            {
                _context.Transacciones.Remove(transaccion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public void AgruparCategorias()
        {
            var ColeccionCategorias = _context.Categorias.ToList();

            Categoria CategoriaDefault = new Categoria() { IdCategoria = 0, Titulo = "Elige una categoría" };

            ColeccionCategorias.Insert(0, CategoriaDefault);

            ViewBag.Categorias = ColeccionCategorias;
        }
    }
}
