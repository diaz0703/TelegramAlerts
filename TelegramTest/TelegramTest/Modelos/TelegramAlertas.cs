using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegramTest.Modelos
{
    [Table("TelegramAlertas")]
    public class TelegramAlertas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AlertaFilaId { get; set; }
        public string AzureAlertaId { get; set; }
        public int AlertaViva { get; set; }
        public DateTime FechaAlertaFire { get; set; }
        public DateTime FechaAlertaSolve { get; set; }

        // varchar(3000) NULL
        [Column(TypeName = "varchar(3000)")]
        public string TipoAlerta { get; set; }

        // varchar(8000) NULL
        [Column(TypeName = "varchar(8000)")]
        public string Alerta { get; set; }

        // datetime NULL
        [Column(TypeName = "datetime")]
        public DateTime? fechainserta { get; set; }
    }
}
