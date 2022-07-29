
using Microsoft.Extensions.Caching.Memory;

namespace LaakeAppLibrary.DataAccess;
public class MongoLaakeData : ILaakeData
{
   private readonly IDbConnection _db;
   private readonly IMemoryCache _cache;
   private readonly IUserData _userData;
   private readonly IMongoCollection<LaakeModel> _laake;
   private const string CacheName = "LaakeData";

   public MongoLaakeData(IDbConnection db, IUserData userData, IMemoryCache cache)
   {
      _db = db;
      _cache = cache;
      _userData = userData;
      _laake = db.LaakeCollection;
   }

   public async Task<LaakeModel> GetLaake(string id)
   {
      var results = await _laake.FindAsync(s => s.Id == id);
      return results.FirstOrDefault();
   }
   public async Task<List<LaakeModel>> GetUsersLaakkeet(string userId)
   {
      var output = _cache.Get<List<LaakeModel>>(userId);
      if (output is null)
      {
         var results = await _laake.FindAsync(s => s.Author.Id == userId);
         output = results.ToList();

         _cache.Set(userId, output, TimeSpan.FromSeconds(1));
      }

      return output;
   }

   public async Task UpdateLaake(LaakeModel laake)
   {
      await _laake.ReplaceOneAsync(s => s.Id == laake.Id, laake);
      //destroys cache data cache. ei niin hyvä jos on liian paljon käyttäjiä.
      _cache.Remove(CacheName);
   }

   public async Task CreateLaake(LaakeModel laake)
   {
      var client = _db.Client;
      using var session = await client.StartSessionAsync();
      session.StartTransaction();
      try
      {
         var db = client.GetDatabase(_db.DbName);
         var laakeInTransaction = db.GetCollection<LaakeModel>(_db.LaakeCollectionName);
         //lisää suggestionin suggestion tableen
         await laakeInTransaction.InsertOneAsync(laake);

         var userInTransaction = db.GetCollection<UserModel>(_db.UserCollectionName);
         var user = await _userData.GetUser(laake.Author.Id);
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
