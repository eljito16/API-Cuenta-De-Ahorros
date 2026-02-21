using System.ComponentModel.DataAnnotations;

namespace CuentaApi.Models
{
    public class CuentaUpdateDto
    {
        [Required]
        [MinLength(3)]
        public string Titular { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Saldo { get; set; }
    }
}
