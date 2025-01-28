using System.ComponentModel.DataAnnotations;
namespace RegistroTecnicos.Models
{
    public class Ciudades
    {
        [Key]
        public int CiudadId{ get; set; }
        public string Nombre { get; set; }

    }
}
