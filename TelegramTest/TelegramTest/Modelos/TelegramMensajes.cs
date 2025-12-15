using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegramTest.Modelos
{
    [Table("TelegramMensajes")]
    public class TelegramMensajes
    {
        [Key]
        [Column("MensajeFilaId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MensajeFilaId { get; set; }

        [Column("filaid")]
        public int? FilaId { get; set; }

        [Column("AlertaFilaId")]
        public int? AlertaFilaId { get; set; }

        [Column("telegramid", TypeName = "varchar(128)")]
        [MaxLength(128)]
        public string TelegramId { get; set; }

        [Column("MensaTexto", TypeName = "varchar(8000)")]
        [MaxLength(8000)]
        public string MensaTexto { get; set; }

        [Column("fechainserta")]
        public DateTime? FechaInserta { get; set; } // Se respeta el DEFAULT        public DateTime? FechaInserta { get; set; } // Se respeta el DEFAULT GETDATE() en SQL
    }
}
