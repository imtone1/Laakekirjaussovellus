using Microsoft.Extensions.Caching.Memory;

namespace LaakeAppLibrary.DataAccess;
public class MongoYoMukaanData : IYoMukaanData
{
   private readonly IMongoCollection<YoMaarittelyModel> _yomukaan;
   private readonly IDbConnection _db;
   private readonly IMemoryCache _cache;
   private readonly IUserData _userData;
   private const string CacheName = "YoMukaanData";
   public MongoYoMukaanData(IDbConnection db, IUserData userData, IMemoryCache cache)
   {
      _db = db;
      _userData = userData;
      _cache = cache;
      _yomukaan = db.YoMaarittelyCollection;
   }

   public async Task<List<YoMaarittelyModel>> GetYoMukaanAsync()
   {
      //find all
      var results = await _yomukaan.FindAsync(_ => true);
      return results.ToList();
   }

   public async Task CreateYoMukaan(YoMaarittelyModel yo)
   {
      //return _yomukaan.InsertOneAsync(yo);
      
         var client = _db.Client;
         using var session = await client.StartSessionAsync();
         session.StartTransaction();
         try
         {
            var db = client.GetDatabase(_db.DbName);
            var yomaarittelyInTransaction = db.GetCollection<YoMaarittelyModel>(_db.YoMaatittelyCollectionName);
            //lisää suggestionin suggestion tableen
            await yomaarittelyInTransaction.InsertOneAsync(yo);

            var userInTransaction = db.GetCollection<UserModel>(_db.UserCollectionName);
            var user = await _userData.GetUser(yo.Author.Id);
            //user.AuthoredSuggestions.Add(new BasicSuggestionModel(suggestion));
            await userInTransaction.ReplaceOneAsync(u => u.Id == user.Id, user);

            await session.CommitTransactionAsync();

            //ei enää lisätä cacheen eikä poisteta cache. Tämä tarkoittaa, että admin ei näe ehdotukset minuutin verran eli niin kauan kuin cache refreshaa 
         }
         catch (Exception ex)
         {
            await session.AbortTransactionAsync();
            throw;
         }
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
