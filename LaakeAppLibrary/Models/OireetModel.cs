using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaakeAppLibrary.Models;
public class OireetModel
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
   public string OireetId { get; set; }
   public string OireNimi { get; set; }
   public string OireKuvaus { get; set; }

   public OireKuvausModel OireKuvausLista { get; set; }
   public DateTime OirePvm { get; set; }=DateTime.Now;
   public KipumittariModel Kipumittari { get; set; }
}
