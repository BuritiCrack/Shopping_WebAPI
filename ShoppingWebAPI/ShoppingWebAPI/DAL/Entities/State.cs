using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ShoppingWebAPI.DAL.Entities
{
    public class State : Entity
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")] // Not null
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener como maximo {1} caracteres")]
        [Display(Name = "País")]
        public string Name { get; set; }

        [Display(Name = "País")]
        public Country Country { get; set; }

        [Display(Name = "Id País")]
        public Guid CountryId { get; set; }


        [Display(Name = "Ciudades")]
        public ICollection<City> Cities { get; set; }

        public int CitiesNumber => Cities == null ? 0 : Cities.Count;
    }

}
