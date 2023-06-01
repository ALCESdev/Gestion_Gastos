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
    public class CategoriaController : Controller
    {
        private readonly AplicacionDbContext _context;

        public CategoriaController(AplicacionDbContext context)
        {
            _context = context;
        }

        // GET: Categoria
        public async Task<IActionResult> Index()
        {
              return _context.Categorias != null ? 
                          View(await _context.Categorias.ToListAsync()) :
                          Problem("Entity set 'AplicacionDbContext.Categorias'  is null.");
        }

        // GET: Categoria/CrearEditar
        public IActionResult CrearEditar(int id = 0)
        {
            if (id == 0)
            {
                return View(new Categoria());
            }
            else
                return View(_context.Categorias.Find(id));
        }

        // POST: Categoria/CrearEditar
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearEditar([Bind("IdCategoria,Titulo,Icono,Tipo")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                if (categoria.IdCategoria == 0)
                {
                    _context.Add(categoria);
                }
                else
                    _context.Update(categoria);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categorias == null)
            {
                return Problem("Entity set 'AplicacionDbContext.Categorias'  is null.");
            }
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
          return (_context.Categorias?.Any(e => e.IdCategoria == id)).GetValueOrDefault();
        }
    }
}
