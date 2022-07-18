namespace LaakeAppLibrary.DataAccess;

public interface ILaakeData
{
   Task CreateLaake(LaakeModel laake);
   Task<LaakeModel> GetLaake(string id);
   Task<List<LaakeModel>> GetUsersLaakkeet(string userId);
   Task UpdateLaake(LaakeModel laake);
}