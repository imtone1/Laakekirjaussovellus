using Microsoft.Extensions.Caching.Memory;

namespace LaakeAppLibrary.DataAccess;
public class MongoKipumittariData
{
   private readonly IMongoCollection<KipumittariModel> _kipumittari;
   private readonly IMemoryCache _cache;
}
