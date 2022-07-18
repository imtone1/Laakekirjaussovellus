namespace LaakeAppLibrary.DataAccess;

public interface IYoMukaanData
{
   Task CreateYoMukaan(YoMaarittelyModel yo);
   Task<List<YoMaarittelyModel>> GetYoMukaanAsync();
   Task<YoMaarittelyModel> GetYoMukaanYksi(string id);
   Task UpdateYoMukaan(YoMaarittelyModel yo);
}