using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace LaakeAppLibrary.DataAccess;
public class DbConnection : IDbConnection
{
   private readonly IConfiguration _config;
   private readonly IMongoDatabase _db;
   //referenses appsettings.json filissa connectionstringin
   private string _connectionId = "MongoDB";
   public string DbName { get; private set; }
   public string CategoryCollectionName { get; private set; } = "categories";
   public string StatusCollectionName { get; private set; } = "statuses";
   public string UserCollectionName { get; private set; } = "users";
   public string SuggestionCollectionName { get; private set; } = "suggestions";
   //Omat
   public string AnnosteluValiCollectionName { get; private set; } = "annostelu";
   public string YoMaatittelyCollectionName { get; private set; } = "yomaarittely";
   public string KipumittariCollectionName { get; private set; } = "kipumittari";
   public string LaakeMuotoCollectionName { get; private set; } = "laakemuoto";
   public string LaakeKayttoCollectionName { get; private set; } = "laakekaytto";
   public string LaakeCollectionName { get; private set; } = "laake";
   public string OireKuvausCollectionName { get; private set; } = "oirekuvaus";
   public string OireetCollectionName { get; private set; } = "oireet";


   //Omat loppuu
   public MongoClient Client { get; private set; }
   public IMongoCollection<CategoryModel> CategoryCollection { get; private set; }
   public IMongoCollection<StatusModel> StatusCollection { get; private set; }
   public IMongoCollection<UserModel> UserCollection { get; private set; }
   public IMongoCollection<SuggestionModel> SuggestionCollection { get; private set; }

   //Omat
   public IMongoCollection<AnnosteluValiModel> AnnosteluValiCollection { get; private set; }
   public IMongoCollection<YoMaarittelyModel> YoMaarittelyCollection { get; private set; }
   public IMongoCollection<KipumittariModel> KipumittariCollection { get; private set; }
   public IMongoCollection<LaakeMuotoModel> LaakeMuotoCollection { get; private set; }
   public IMongoCollection<LaakeKayttoModel> LaakeKayttoCollection { get; private set; }
   public IMongoCollection<LaakeModel> LaakeCollection { get; private set; }
   public IMongoCollection<OireKuvausModel> OireKuvausCollection { get; private set; }
   public IMongoCollection<OireetModel> OireetCollection { get; private set; }


   //Omat loppuu
   public DbConnection(IConfiguration config)
   {
      _config = config;
      //connect to db
      Client = new MongoClient(_config.GetConnectionString(_connectionId));
      DbName = _config["DatabaseName"];
      _db = Client.GetDatabase(DbName);

      //connection to collections
      CategoryCollection = _db.GetCollection<CategoryModel>(CategoryCollectionName);
      StatusCollection = _db.GetCollection<StatusModel>(StatusCollectionName);
      UserCollection = _db.GetCollection<UserModel>(UserCollectionName);
      SuggestionCollection = _db.GetCollection<SuggestionModel>(SuggestionCollectionName);

      //Omat
      AnnosteluValiCollection = _db.GetCollection<AnnosteluValiModel>(AnnosteluValiCollectionName);
      YoMaarittelyCollection = _db.GetCollection<YoMaarittelyModel>(YoMaatittelyCollectionName);
      KipumittariCollection = _db.GetCollection<KipumittariModel>(KipumittariCollectionName);
      LaakeKayttoCollection = _db.GetCollection<LaakeKayttoModel>(LaakeKayttoCollectionName);
      LaakeMuotoCollection = _db.GetCollection<LaakeMuotoModel>(LaakeMuotoCollectionName);
      LaakeCollection = _db.GetCollection<LaakeModel>(LaakeCollectionName);
      OireKuvausCollection = _db.GetCollection<OireKuvausModel>(OireKuvausCollectionName);
      OireetCollection = _db.GetCollection<OireetModel>(OireetCollectionName);

      //Omat loppuu
   }
}
