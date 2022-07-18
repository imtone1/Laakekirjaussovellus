namespace LaakeAppLibrary.DataAccess;

public interface ILaakeMuotoData
{
   Task CreateLaakemuoto(LaakeMuotoModel laakemuoto);
   Task<List<LaakeMuotoModel>> GetAllLaakemuodot();
}