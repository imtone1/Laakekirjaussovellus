using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

namespace LaakeApp100722;

public static class RegisterServices
{
   public static void ConfigureServices(this WebApplicationBuilder builder)
   {
      //tämä otettu Program.cs .sta
        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor().AddMicrosoftIdentityConsentHandler();
        builder.Services.AddMemoryCache();

      builder.Services.AddControllersWithViews().AddMicrosoftIdentityUI();
      //b2c lisätty, ensin lisätty appsettings.jsoniin, sitten program.cs ja sitten tähän
      builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));


      //extra lisätään admin tarkistuksen
      builder.Services.AddAuthorization(options =>
      {
         options.AddPolicy("Admin", policy =>
         {
            policy.RequireClaim("jobTitle", "Admin");
         });
      });

      //kaikki käyttää saman db connectionin "AddSingleton"
      builder.Services.AddSingleton<IDbConnection, DbConnection>();
      builder.Services.AddSingleton<ICategoryData, MongoCategoryData>();
      builder.Services.AddSingleton<IStatusData, MongoStatusData>();
      builder.Services.AddSingleton<ISuggestionData, MongoSuggestionData>();
      builder.Services.AddSingleton<IUserData, MongoUserData>();
      //omat
      builder.Services.AddSingleton<ILaakeData, MongoLaakeData>();
      builder.Services.AddSingleton<IKipumittariData, MongoKipumittariData>();
      builder.Services.AddSingleton<ILaakeKayttoData, MongoLaakeKayttoData>();
      builder.Services.AddSingleton<ILaakeMuotoData, MongoLaakeMuotoData>();
      builder.Services.AddSingleton<IOireetData, MongoOireetData>();
      builder.Services.AddSingleton<IOireKuvausData, MongoOireKuvausData>();
      builder.Services.AddSingleton<IYoMukaanData, MongoYoMukaanData>();
      builder.Services.AddSingleton<IAnnosteluValiData, MongoAnnosteluValiData>();


    }
}
