using Microsoft.Extensions.Caching.Memory;

namespace LaakeAppLibrary.DataAccess;
public class MongoOireetData : IOireetData
{
   private readonly IDbConnection _db;
   private readonly IMemoryCache _cache;
   private readonly IUserData _userData;
   private readonly IMongoCollection<OireetModel> _oireet;
   private const string CacheName = "OireetData";

   public MongoOireetData(IDbConnection db, IUserData userData, IMemoryCache cache)
   {
      _db = db;
      _cache = cache;
      _userData = userData;
      _oireet = db.OireetCollection;
   }

   public async Task<OireetModel> GetOireet(string id)
   {
      var results = await _oireet.FindAsync(s => s.Id == id);
      return results.FirstOrDefault();
   }
   public async Task<List<OireetModel>> GetUsersOireet(string userId)
   {
      var output = _cache.Get<List<OireetModel>>(userId);
      if (output is null)
      {
         var results = await _oireet.FindAsync(s => s.Author.Id == userId);
         output = results.ToList();

         _cache.Set(userId, output, TimeSpan.FromMinutes(1));
      }

      return output;
   }

   public async Task UpdateOire(OireetModel oire)
   {
      await _oireet.ReplaceOneAsync(s => s.Id == oire.Id, oire);
      //destroys cache data cache. ei niin hyvä jos on liian paljon käyttäjiä.
      _cache.Remove(CacheName);
   }

   public async Task CreateOire(OireetModel oire)
   {
      var client = _db.Client;
      using var session = await client.StartSessionAsync();
      session.StartTransaction();
      try
      {
         var db = client.GetDatabase(_db.DbName);
         var oireetInTransaction = db.GetCollection<OireetModel>(_db.OireetCollectionName);
         //lisää suggestionin suggestion tableen
         await oireetInTransaction.InsertOneAsync(oire);

         var userInTransaction = db.GetCollection<UserModel>(_db.UserCollectionName);
         var user = await _userData.GetUser(oire.Author.Id);
         //user.AuthoredSuggestions.Add(new BasicSuggestionModel(oire));
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
}
