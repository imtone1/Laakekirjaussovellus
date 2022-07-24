namespace LaakeAppLibrary.DataAccess;

public interface IAnnosteluValiData
{
   Task CreateAnnosteluVali(AnnosteluValiModel annosteluvali);
   Task<AnnosteluValiModel> GetAnnosteluvali(string id);
   Task<List<AnnosteluValiModel>> GetUsersAnnosteluVali(string userId);
   Task<List<AnnosteluValiModel>> GetLaakkeenOttopvm(string userId, string laakeId);
   Task UpdateLaakeKaytto(AnnosteluValiModel annosteluvali);
}
