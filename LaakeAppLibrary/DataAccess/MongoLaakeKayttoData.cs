using Microsoft.Extensions.Caching.Memory;

namespace LaakeAppLibrary.DataAccess;
public class MongoLaakeKayttoData : ILaakeKayttoData
{
   private readonly IDbConnection _db;
   private readonly IMongoCollection<LaakeKayttoModel> _laakekaytto;
   private readonly IMemoryCache _cache;
   private readonly IUserData _userData;
   private const string CacheName = "LaakeKayttoData";

   public MongoLaakeKayttoData(IDbConnection db, IUserData userData, IMemoryCache cache)
   {
      _db = db;
      _cache = cache;
      _userData = userData;

      _laakekaytto = db.LaakeKayttoCollection;
   }

   public async Task<List<LaakeKayttoModel>> GetUsersLaakeKaytto(string userId)
   {
      var output = _cache.Get<List<LaakeKayttoModel>>(userId);
      if (output is null)
      {
         var results = await _laakekaytto.FindAsync(s => s.Author.Id == userId);
         output = results.ToList();

         _cache.Set(userId, output, TimeSpan.FromMinutes(1));
      }

      return output;
   }

   public async Task CreateLaakeKaytto(LaakeKayttoModel laakekaytto)
   {
      var client = _db.Client;
      using var session = await client.StartSessionAsync();
      session.StartTransaction();
      try
      {
         var db = client.GetDatabase(_db.DbName);
         var laakekayttoInTransaction = db.GetCollection<LaakeKayttoModel>(_db.LaakeKayttoCollectionName);
         //lisää suggestionin suggestion tableen
         await laakekayttoInTransaction.InsertOneAsync(laakekaytto);

         var userInTransaction = db.GetCollection<UserModel>(_db.UserCollectionName);
         var user = await _userData.GetUser(laakekaytto.Author.Id);
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

   public async Task<LaakeKayttoModel> GetLaakekaytto(string id)
   {
      var results = await _laakekaytto.FindAsync(s => s.Id == id);
      return results.FirstOrDefault();
   }
   public async Task UpdateLaakeKaytto(LaakeKayttoModel laakekaytto)
   {
      await _laakekaytto.ReplaceOneAsync(s => s.Id == laakekaytto.Id, laakekaytto);
      //destroys cache data cache. ei niin hyvä jos on liian paljon käyttäjiä.
      _cache.Remove(CacheName);
   }

}
