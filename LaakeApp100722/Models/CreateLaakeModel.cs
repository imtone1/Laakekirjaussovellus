using System.ComponentModel.DataAnnotations;

namespace LaakeApp100722.Models;

public class CreateLaakeModel
{
   [Required]
   [MinLength(1)]
   [Display(Name = "Lääkenimi ja vahvuus")]
   public string Nimi { get; set; }

   [Display(Name = "Lääkemuoto")]
   public string LaakeMuotoId { get; set; }

   [Display(Name = "Annostelumäärä")]
   public int AnnosteleluMaara { get; set; }

   [Display(Name = "Annosteluväli")]
   public string AnnosteleluValiId { get; set; }

   [Display(Name = "Otetaan yöllä")]
   public bool YoMukaan { get; set; }

   public DateTime Lisatty { get; set; }

   public DateTime Muokattu { get; set; }

}
