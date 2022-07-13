
namespace LaakeAppLibrary.Models;
public class AnnosteluValiModel
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
   public string AnnosteluValiId { get; set; }
   public string AnnosteluValiNimi { get; set; }
}
