using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class Sistemas
    {
        [Key]
        public int SistemaId { get; set; }

        [Required(ErrorMessage ="Este campo no puede estar vacio")]
        [RegularExpression(@"^(10|[0-9](\.[0-9])?)$", ErrorMessage = "Este campo solo acepta numeros entre 0 y 10")]
        public double Complejidad { get; set; }

        [Required(ErrorMessage ="Este campo es requerido")]
        public string Descripcion {  get; set; }
    }
}
