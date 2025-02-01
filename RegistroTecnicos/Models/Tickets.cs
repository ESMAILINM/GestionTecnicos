using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RegistroTecnicos.Models
{
    public class Tickets
    {
        [Key]
        public int TicketId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Prioridad { get; set; }
        public int? ClienteId { get; set; }
        public int? TecnicoId { get; set; }
        public string Asunto { get; set; }
        public string Descripcion { get; set; }
        public string TiempoInvertido {get; set;}


        [ForeignKey("ClienteId")]
        public  Clientes clientes { get; set; }

        [ForeignKey("TecnicoId")]
        public  Tecnicos tecnicos { get; set; }
   
    }
}
