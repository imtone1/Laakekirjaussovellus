
namespace LaakeAppLibrary.Models;
public class LaakeMuotoModel
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
   public string LaakeMuotoId { get; set; }
   public string LaakeMuotoNimi{ get; set; }
   public string LaakeMuotoKuvaus { get; set; }
}
