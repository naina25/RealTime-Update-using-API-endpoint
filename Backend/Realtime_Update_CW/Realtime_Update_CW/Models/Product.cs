using System.ComponentModel.DataAnnotations;

namespace Realtime_Update_CW.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int Product_id { get; set; }
        public string? Product_name { get; set; }
        public int Product_price { get; set; }
    }
}
