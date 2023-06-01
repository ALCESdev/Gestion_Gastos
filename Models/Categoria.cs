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
        [Required(ErrorMessage = "Se requiere un título.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Se requiere un icono para la categoría.")]
        [Column(TypeName = "nvarchar(5)")]
        public string Icono { get; set; } = "";

        [Column(TypeName = "nvarchar(10)")]
        public string Tipo { get; set; } = "Gasto";

        [NotMapped]
        public string? IconoTitulo
        {
            get
            {
                return this.Icono + " " + this.Titulo;
            }
        }
    }
}
