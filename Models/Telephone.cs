using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelephoneCRUD.Models
{
    public class Telephone
    {
        [Key]
        [StringLength(15)]
        public string Imei { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Marque { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Modele { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Prix { get; set; }

        public DateTime DateAjout { get; set; } = DateTime.Now;
    }
}