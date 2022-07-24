
namespace LaakeAppLibrary.Models;
public class AnnosteluValiModel
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
   public string Id { get; set; }
   public string AnnosteluValiNimi { get; set; }
   public int AnnosteluVali { get; set; }

   public DateTime Otettu { get; set; }= DateTime.Now;

   public LaakeModel Laake { get; set; } 
   public BasicUserModel Author { get; set; }
}
