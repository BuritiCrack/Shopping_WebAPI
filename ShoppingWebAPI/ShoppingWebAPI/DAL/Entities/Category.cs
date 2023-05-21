using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ShoppingWebAPI.DAL.Entities
{
    public class Category : Entity
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")] // Not null
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener como maximo {1} caracteres")]
        [Display(Name = "País")]
        public string Name { get; set; }


        [Display(Name = "Descripcion")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener como maximo {1} caracteres")]
        public string Description { get; set; }
    }
}
