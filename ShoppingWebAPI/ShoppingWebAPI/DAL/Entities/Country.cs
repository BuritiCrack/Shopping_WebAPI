using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ShoppingWebAPI.DAL.Entities
{
    public class Country : Entity
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")] // Not null
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener como maximo {1} caracteres")]
        [Display(Name = "País")]
        public string Name { get; set; }
    }
}
