namespace LaakeAppLibrary.DataAccess;

public interface IKipumittariData
{
   Task CreateKipumittari(KipumittariModel kipumittari);
   Task<List<KipumittariModel>> GetAllKipumittarit();
}