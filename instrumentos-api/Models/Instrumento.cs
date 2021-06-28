using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace instrumentos_api.Models
{
    [Table("instrumento")]
    public class InstrumentoEntity
    {
        public int Id { get; set; }
        public string Instrumento { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Imagen { get; set; }

        [Column("costo_envio")]
        public string CostoEnvio { get; set; }
        public decimal Precio { get; set; }

        [Column("cantidad_vendida")]
        public int CantidadVendida { get; set; }
    }
}
