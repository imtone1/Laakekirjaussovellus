namespace LaakeApp100722;

public static class RegisterServices
{
   public static void ConfigureServices(this WebApplicationBuilder builder)
   {
      //tämä otettu Program.cs .sta
        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddMemoryCache();

      //kaikki käyttää saman db connectionin "AddSingleton"
      builder.Services.AddSingleton<IDbConnection, DbConnection>();
      builder.Services.AddSingleton<ICategoryData, MongoCategoryData>();
      builder.Services.AddSingleton<IStatusData, MongoStatusData>();
      builder.Services.AddSingleton<ISuggestionData, MongoSuggestionData>();
      builder.Services.AddSingleton<IUserData, MongoUserData>();


    }
}
