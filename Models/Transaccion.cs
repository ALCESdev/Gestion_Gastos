using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Gastos.Models
{
    public class Transaccion
    {
        [Key]
        public int IdTransaccion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Por favor, selecciona una categoría.")]
        public int IdCategoria { get; set; }

        [ForeignKey("IdCategoria")]
        public Categoria? Categoria { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0.")]
        public int Cantidad { get; set; }

        [Column(TypeName = "nvarchar(75)")]
        public string? Notas { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        [NotMapped]
        public string? IconoConCategoria
        {
            get
            {
                return Categoria == null ? "" : Categoria.Icono + " " + Categoria.Titulo;
            }
        }

        [NotMapped]
        public string? FormatoCantidad
        {
            get
            {
                return ((Categoria == null || Categoria.Tipo == "Gasto") ? "- " : "+ ") + Cantidad.ToString("C0");
            }
        }

    }
}
