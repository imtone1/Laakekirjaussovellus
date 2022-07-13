using MongoDB.Driver;

namespace LaakeAppLibrary.DataAccess;
public interface IDbConnection
{
   IMongoCollection<AnnosteluValiModel> AnnosteluValiCollection { get; }
   string AnnosteluValiCollectionName { get; }
   IMongoCollection<CategoryModel> CategoryCollection { get; }
   string CategoryCollectionName { get; }
   MongoClient Client { get; }
   string DbName { get; }
   IMongoCollection<KipumittariModel> KipumittariCollection { get; }
   string KipumittariCollectionName { get; }
   IMongoCollection<LaakeModel> LaakeCollection { get; }
   string LaakeCollectionName { get; }
   IMongoCollection<LaakeKayttoModel> LaakeKayttoCollection { get; }
   string LaakeKayttoCollectionName { get; }
   IMongoCollection<LaakeMuotoModel> LaakeMuotoCollection { get; }
   string LaakeMuotoCollectionName { get; }
   IMongoCollection<StatusModel> StatusCollection { get; }
   string StatusCollectionName { get; }
   IMongoCollection<SuggestionModel> SuggestionCollection { get; }
   string SuggestionCollectionName { get; }
   IMongoCollection<UserModel> UserCollection { get; }
   string UserCollectionName { get; }
   IMongoCollection<YoMaarittelyModel> YoMaarittelyCollection { get; }
   string YoMaatittelyCollectionName { get; }
}