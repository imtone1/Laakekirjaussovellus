using Microsoft.Extensions.Caching.Memory;

namespace LaakeAppLibrary.DataAccess;
public class MongoKipumittariData : IKipumittariData
{
   private readonly IMongoCollection<KipumittariModel> _kipumittari;
   private readonly IMemoryCache _cache;
   private const string CacheName = "KipumittariData";

   public MongoKipumittariData(IDbConnection db, IMemoryCache cache)
   {
      _cache = cache;
      _kipumittari = db.KipumittariCollection;
   }

   public async Task<List<KipumittariModel>> GetAllKipumittarit()
   {
      var output = _cache.Get<List<KipumittariModel>>(CacheName);

      if (output == null)
      {
         var results = await _kipumittari.FindAsync(_ => true);
         output = results.ToList();

         _cache.Set(CacheName, output, TimeSpan.FromDays(1));
      }
      return output;
   }

   public Task CreateKipumittari(KipumittariModel kipumittari)
   {
      return _kipumittari.InsertOneAsync(kipumittari);
   }

}
