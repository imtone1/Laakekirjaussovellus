using Microsoft.Extensions.Caching.Memory;

namespace LaakeAppLibrary.DataAccess;
public class MongoLaakeMuotoData : ILaakeMuotoData
{
   private readonly IMongoCollection<LaakeMuotoModel> _laakemuoto;
   private readonly IMemoryCache _cache;
   private const string CacheName = "LaakeMuotoData";

   public MongoLaakeMuotoData(IDbConnection db, IMemoryCache cache)
   {
      _cache = cache;
      _laakemuoto = db.LaakeMuotoCollection;
   }
   public async Task<List<LaakeMuotoModel>> GetAllLaakemuodot()
   {
      var output = _cache.Get<List<LaakeMuotoModel>>(CacheName);
      //ekalla kerralla output on null koska sitä ei ole vielä cachissa. 
      if (output == null)
      {
         var results = await _laakemuoto.FindAsync(_ => true);
         output = results.ToList();

         //laitetaan kategoriat cacheen ja pidetään se siinä yhden päivän. Huom! Tämän takia cachissa saa olla vain sellainen data, joka ei muutu joka kerta
         _cache.Set(CacheName, output, TimeSpan.FromDays(1));
      }
      return output;
   }

   public Task CreateLaakemuoto(LaakeMuotoModel laakemuoto)
   {
      return _laakemuoto.InsertOneAsync(laakemuoto);
   }
}
