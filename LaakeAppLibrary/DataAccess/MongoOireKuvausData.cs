
using Microsoft.Extensions.Caching.Memory;

namespace LaakeAppLibrary.DataAccess;
public class MongoOireKuvausData : IOireKuvausData
{
   private readonly IMongoCollection<OireKuvausModel> _oirekuvaus;
   private readonly IMemoryCache _cache;
   private const string CacheName = "OireKuvausData";

   public MongoOireKuvausData(IDbConnection db, IMemoryCache cache)
   {
      _cache = cache;
      _oirekuvaus = db.OireKuvausCollection;
   }
   public async Task<List<OireKuvausModel>> GetAllOireKuvaukset()
   {
      var output = _cache.Get<List<OireKuvausModel>>(CacheName);
      if (output is null)
      {
         var result = await _oirekuvaus.FindAsync(_ => true);
         output = result.ToList();

         _cache.Set(CacheName, output, TimeSpan.FromDays(1));
      }
      return output;
   }
   public Task CreateOireKuvaus(OireKuvausModel oirekuvaus)
   {
      return _oirekuvaus.InsertOneAsync(oirekuvaus);
   }
}
