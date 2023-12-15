using System.ComponentModel.DataAnnotations;

namespace ApiTestDemo.Dto
{
    public class PembagianDto
    {
        [Required]
        public int? x { get; set; }
        [Required]
        public int? y { get; set; }
    }
}
