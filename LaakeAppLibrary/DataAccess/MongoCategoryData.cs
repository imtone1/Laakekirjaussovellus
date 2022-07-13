using Microsoft.Extensions.Caching.Memory;

namespace LaakeAppLibrary.DataAccess;
public class MongoCategoryData : ICategoryData
{
   private readonly IMongoCollection<CategoryModel> _categories;
   private readonly IMemoryCache _cache;
   private const string CacheName = "CategoryData";
   public MongoCategoryData(IDbConnection db, IMemoryCache cache)
   {
      _cache = cache;
      _categories = db.CategoryCollection;
   }

   public async Task<List<CategoryModel>> GetAllCategories()
   {
      var output = _cache.Get<List<CategoryModel>>(CacheName);
      //ekalla kerralla output on null koska sitä ei ole vielä cachissa. 
      if (output == null)
      {
         var results = await _categories.FindAsync(_ => true);
         output = results.ToList();

         //laitetaan kategoriat cacheen ja pidetään se siinä yhden päivän. Huom! Tämän takia cachissa saa olla vain sellainen data, joka ei muutu joka kerta
         _cache.Set(CacheName, output, TimeSpan.FromDays(1));
      }
      return output;
   }

   public Task CreateCategory(CategoryModel category)
   {
      return _categories.InsertOneAsync(category);
   }
}
