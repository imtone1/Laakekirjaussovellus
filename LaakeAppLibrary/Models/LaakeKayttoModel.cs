using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaakeAppLibrary.Models;
public class LaakeKayttoModel
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
   public string Id { get; set; }
   public BasicUserModel Author { get; set; }
   public DateTime Kaytetty { get; set; } = DateTime.UtcNow;
   public DateTime Siirretty { get; set; }
   public DateTime Hylatty { get; set; }

}
