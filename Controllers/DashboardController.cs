using Gestion_Gastos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Gestion_Gastos.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AplicacionDbContext _context;
        public DashboardController(AplicacionDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            // Últimos 7 días
            DateTime FechaInicio = DateTime.Today.AddDays(-6);
            DateTime FechaFin = DateTime.Today;

            List<Transaccion> TransaccionSeleccionada = await _context.Transacciones
                .Include(x => x.Categoria)
                .Where(y => y.Fecha >= FechaInicio && y.Fecha <= FechaFin)
                .ToListAsync();

            // Ingresos totales
            int IngresosTotales = TransaccionSeleccionada
                .Where(i => i.Categoria.Tipo == "Ingreso")
                .Sum(j => j.Cantidad);

            ViewBag.IngresoTotal = IngresosTotales.ToString("C0");

            // Gastos totales
            int GastosTotales = TransaccionSeleccionada
                .Where(i => i.Categoria.Tipo == "Gasto")
                .Sum(j => j.Cantidad);

            ViewBag.GastoTotal = GastosTotales.ToString("C0");

            // Balance
            int Balance = IngresosTotales - GastosTotales;

            CultureInfo culture = CultureInfo.CreateSpecificCulture("es-ES");
            culture.NumberFormat.CurrencyNegativePattern = 1;

            ViewBag.Balance = String.Format(culture,"{0:C0}", Balance);

            // Gráfico Donut - Gasto por categoría
            ViewBag.DatosGraficoDonut = TransaccionSeleccionada
                .Where(i => i.Categoria.Tipo == "Gasto")
                .GroupBy(j => j.Categoria.IdCategoria)
                .Select(k => new
                {
                    categoriaTituloIcono = k.First().Categoria.Icono + " " + k.First().Categoria.Titulo,
                    cantidad = k.Sum(j => j.Cantidad),
                    cantidadFormato = k.Sum(j => j.Cantidad).ToString("C0"),
                })
                .OrderByDescending(l => l.cantidad)
                .ToList();

            // Gráfico de lineas - Ingreso
            List<DatosGraficoLineas> ResumenIngresos = TransaccionSeleccionada
                .Where(i => i.Categoria.Tipo == "Ingreso")
                .GroupBy(j => j.Fecha)
                .Select(k => new DatosGraficoLineas()
                {
                    dias = k.First().Fecha.ToString("dd-MMM"),
                    ingreso = k.Sum(l => l.Cantidad)
                })
                .ToList();

            // Gráfico de lineas - Gasto
            List<DatosGraficoLineas> ResumenGastos = TransaccionSeleccionada
                .Where(i => i.Categoria.Tipo == "Gasto")
                .GroupBy(j => j.Fecha)
                .Select(k => new DatosGraficoLineas()
                {
                    dias = k.First().Fecha.ToString("dd-MMM"),
                    gasto = k.Sum(l => l.Cantidad)
                })
                .ToList();

            // Gráfico de lineas - Combinación de Ingresos y Gastos
            string[] Ultimos7Dias = Enumerable.Range(0, 7)
                .Select(i => FechaInicio.AddDays(i).ToString("dd-MMM"))
                .ToArray();

            ViewBag.DatosGraficoLineas = from dia in Ultimos7Dias
                                      join ingreso in ResumenIngresos on dia equals ingreso.dias into ingresoDiarioJoin
                                      from ingreso in ingresoDiarioJoin.DefaultIfEmpty()
                                      join gasto in ResumenGastos on dia equals gasto.dias into gastoDiarioJoin
                                      from gasto in gastoDiarioJoin.DefaultIfEmpty()
                                      select new
                                      {
                                          dias = dia,
                                          ingreso = ingreso == null ? 0 : ingreso.ingreso,
                                          gasto = gasto == null ? 0 : gasto.gasto,
                                      };

            // Transacciones recientes
            ViewBag.TransaccionesRecientes = await _context.Transacciones
                .Include(i => i.Categoria)
                .OrderByDescending(j => j.Fecha)
                .Take(5)
                .ToListAsync();


            return View();
        }
    }
}

public class DatosGraficoLineas
{
    public string dias;
    public int ingreso;
    public int gasto;
}
