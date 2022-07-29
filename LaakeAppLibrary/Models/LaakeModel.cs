

namespace LaakeAppLibrary.Models
{
    public class LaakeModel
    {
      [BsonId]
      [BsonRepresentation(BsonType.ObjectId)]
      public string Id { get; set; }
      public string Nimi { get; set; }
      public BasicUserModel Author { get; set; }
      public LaakeMuotoModel LaakeMuoto { get; set; }
      public int AnnosteluMaara{ get; set; }

      public int AnnosteluVali1 { get; set; }
      public AnnosteluValiModel AnnosteluVali { get; set; }
      public bool YoMukaan { get; set; } = false;

      public string SeuraavatOttoAjat { get; set; }
      public YoMaarittelyModel YoMaarittely { get; set; }
      public bool Kaytetaan { get; set; } = true;
      public bool Arkistoitu { get; set; } = false;
      public DateTime Lisatty { get; set; } = DateTime.UtcNow;
      public DateTime Muokattu { get; set; } = DateTime.UtcNow;
   }
}
