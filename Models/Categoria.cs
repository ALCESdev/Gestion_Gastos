using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;

namespace Gestion_Gastos.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public String? Titulo { get; set; }

        [Column(TypeName = "nvarchar(5)")]
        public String? Icono { get; set; } = "";

        [Column(TypeName = "nvarchar(10)")]
        public String? Tipo { get; set; } = "Gasto";

    }
}
