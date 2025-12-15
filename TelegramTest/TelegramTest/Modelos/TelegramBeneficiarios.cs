using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegramTest
{
    [Table("TelegramBeneficiarios")]
    public class TelegramBeneficiarios
    {
        [Key]
        [Column("filaid")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FilaId { get; set; }

        [Column("usuario", TypeName = "varchar(50)")]
        [MaxLength(50)]
        public string Usuario { get; set; }

        [Column("nombre", TypeName = "varchar(500)")]
        [MaxLength(500)]
        public string Nombre { get; set; }

        [Column("telegramid", TypeName = "varchar(128)")]
        [MaxLength(128)]
        public string TelegramId { get; set; }

        [Column("situacion")]
        public int? Situacion { get; set; }

        [Column("usuarioinserta", TypeName = "varchar(50)")]
        [MaxLength(50)]
        public string UsuarioInserta { get; set; }

        // Tiene default GETDATE() en SQL; puede venir nulo si se crea fuera de DB
        [Column("fechainserta")]
        public DateTime? FechaInserta { get; set; }
    }

}
