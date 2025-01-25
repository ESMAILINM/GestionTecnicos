using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class Tecnicos
    {
        [Key]
        public int TecnicoId { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "No se permiten Numero")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string? Nombres { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        public decimal SueldoHora { get; set; }
   
    }
}
