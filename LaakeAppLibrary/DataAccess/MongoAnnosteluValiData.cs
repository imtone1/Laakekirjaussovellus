using Microsoft.Extensions.Caching.Memory;

namespace LaakeAppLibrary.DataAccess;
public class MongoAnnosteluValiData : IAnnosteluValiData
{
   private readonly IDbConnection _db;
   private readonly IMongoCollection<AnnosteluValiModel> _annosteluvali;
   private readonly IMemoryCache _cache;
   private readonly IUserData _userData;
   private const string CacheName = "AnnosteluValiData";

   public MongoAnnosteluValiData(IDbConnection db, IUserData userData, IMemoryCache cache)
   {
      _db = db;
      _cache = cache;
      _userData = userData;

      _annosteluvali = db.AnnosteluValiCollection;
   }

   public async Task<List<AnnosteluValiModel>> GetUsersAnnosteluVali(string userId)
   {
      var output = _cache.Get<List<AnnosteluValiModel>>(userId);
      if (output is null)
      {
         var results = await _annosteluvali.FindAsync(s => s.Author.Id == userId);
         output = results.ToList();

         _cache.Set(userId, output, TimeSpan.FromMinutes(1));
      }

      return output;
   }

   public async Task<List<AnnosteluValiModel>> GetLaakkeenOttopvm(string userId, string laakeId)
   {
     
         var results = await _annosteluvali.FindAsync(s => s.Author.Id == userId && s.Laake.Id==laakeId);
         var output = results.ToList();

      
      return output;
   }

   public async Task CreateAnnosteluVali(AnnosteluValiModel annosteluvali)
   {
      var client = _db.Client;
      using var session = await client.StartSessionAsync();
      session.StartTransaction();
      try
      {
         var db = client.GetDatabase(_db.DbName);
         var annosteluvaliInTransaction = db.GetCollection<AnnosteluValiModel>(_db.AnnosteluValiCollectionName);
         //lisää suggestionin suggestion tableen
         await annosteluvaliInTransaction.InsertOneAsync(annosteluvali);

         var userInTransaction = db.GetCollection<UserModel>(_db.UserCollectionName);
         var user = await _userData.GetUser(annosteluvali.Author.Id);
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

   public async Task<AnnosteluValiModel> GetAnnosteluvali(string id)
   {
      var results = await _annosteluvali.FindAsync(s => s.Id == id);
      return results.FirstOrDefault();
   }
   public async Task UpdateLaakeKaytto(AnnosteluValiModel annosteluvali)
   {
      await _annosteluvali.ReplaceOneAsync(s => s.Id == annosteluvali.Id, annosteluvali);
      //destroys cache data cache. ei niin hyvä jos on liian paljon käyttäjiä.
      _cache.Remove(CacheName);
   }
}
