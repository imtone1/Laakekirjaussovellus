using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaakeAppLibrary.Models;
public class OireKuvausModel
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
   public string OireKuvausId { get; set; }
   public string OireNimi { get; set; }
   public string OireKuvaus { get; set; }
}
