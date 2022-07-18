namespace LaakeAppLibrary.DataAccess;
public class MongoYoMukaanData : IYoMukaanData
{
   private readonly IMongoCollection<YoMaarittelyModel> _yomukaan;
   public MongoYoMukaanData(IDbConnection db)
   {
      _yomukaan = db.YoMaarittelyCollection;
   }

   public async Task<List<YoMaarittelyModel>> GetYoMukaanAsync()
   {
      //find all
      var results = await _yomukaan.FindAsync(_ => true);
      return results.ToList();
   }

   public Task CreateYoMukaan(YoMaarittelyModel yo)
   {
      return _yomukaan.InsertOneAsync(yo);
   }

   public async Task UpdateYoMukaan(YoMaarittelyModel yo)
   {
      await _yomukaan.ReplaceOneAsync(s => s.Id == yo.Id, yo);

   }
   public async Task<YoMaarittelyModel> GetYoMukaanYksi(string id)
   {
      var results = await _yomukaan.FindAsync(s => s.Id == id);
      return results.FirstOrDefault();
   }
}
