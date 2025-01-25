using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace RegistroTecnicos.Models
{
    public class Clientes
    {
        [Key]
        
        public int ClienteId { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Inserte un nombre válido")]
        public string? Nombres { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.Date, ErrorMessage = "Fecha Incorrecta")]
        public DateTime FechaIngreso { get; set; } = DateTime.Now;


        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Direccion { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio")]
        [RegularExpression(@"^\d{9}$", ErrorMessage= "Revise que su Rnc digitado contenga exáctamente 9 dígitos numéricos.")]
        public string? Rnc { get;  set; }


        [Required(ErrorMessage = "Este campo es obligatorio")]
        public double LimiteCredito { get; set; }

        //Relacion con la tabla Tecnicos
        public int TecnicoId { get; set; }
        public Tecnicos Tecnico { get; set; }
    }
}