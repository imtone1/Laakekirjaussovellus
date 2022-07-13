using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaakeAppLibrary.Models;
public class KipumittariModel
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
   public string KipumittariId { get; set; }
   public int KipuAste { get; set; }
   public string KipuAsteKuvaus { get; set; }
}
