namespace LaakeAppLibrary.DataAccess;

public interface IOireetData
{
   Task CreateOire(OireetModel oire);
   Task<OireetModel> GetOireet(string id);
   Task<List<OireetModel>> GetUsersOireet(string userId);
   Task UpdateOire(OireetModel oire);
}