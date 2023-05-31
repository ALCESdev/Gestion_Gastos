using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Gastos.Models
{
    public class Transaccion
    {
        [Key]
        public int IdTransaccion { get; set; }


        public int IdCategoria { get; set; }
        public Categoria? Categoria { get; set; }

        public int Cantidad { get; set; }

        [Column(TypeName = "nvarchar(75)")]
        public String? Nota { get; set; }


        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
