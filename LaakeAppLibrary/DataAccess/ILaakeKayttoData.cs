namespace LaakeAppLibrary.DataAccess;

public interface ILaakeKayttoData
{
   Task CreateLaakeKaytto(LaakeKayttoModel laakekaytto);
   Task<LaakeKayttoModel> GetLaakekaytto(string id);
   Task<List<LaakeKayttoModel>> GetUsersLaakeKaytto(string userId);
   Task UpdateLaakeKaytto(LaakeKayttoModel laakekaytto);
}