using System.ComponentModel.DataAnnotations;

namespace LaakeApp100722.Models;

public class CreateYoMaarittelyModel
{
   [Required]
   
   [Display(Name ="Yö alkuaika")]
   public DateTime YoAlku { get; set; }

   
   [Display(Name ="Yö loppuaika")]
   public DateTime YoLoppu { get; set; }
}
