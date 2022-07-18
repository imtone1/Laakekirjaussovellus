namespace LaakeAppLibrary.DataAccess;

public interface IOireKuvausData
{
   Task CreateOireKuvaus(OireKuvausModel oirekuvaus);
   Task<List<OireKuvausModel>> GetAllOireKuvaukset();
}