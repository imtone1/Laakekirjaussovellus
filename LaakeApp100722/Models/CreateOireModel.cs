using System.ComponentModel.DataAnnotations;

namespace LaakeApp100722.Models;

public class CreateOireModel
{
   [Required]
   [MinLength(1)]
   [Display(Name = "Oire")]

   public string OireNimi { get; set; }


   [Display(Name = "Oireen kuvaus")]
   public string OireKuvaus { get; set; }

   [Display(Name = "Pvm, aika")]
   public DateTime OirePvm { get; set; } = DateTime.Now;
}
