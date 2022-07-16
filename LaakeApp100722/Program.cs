
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Rewrite;
using LaakeApp100722;

var builder = WebApplication.CreateBuilder(args);

//korvattu, löytyy mitä on korvattu RegisterServices.cs:sta
builder.ConfigureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseRewriter(
   new RewriteOptions().Add(
      context =>
      {
         //se ei ohjaa automaattisesti enää microsoftIdentity perussivulle vaan ohjaa kotisivulle >täytyy muuttaa vielä app.razor page
         if (context.HttpContext.Request.Path == "/MicrosoftIdentity/Account/SignedOut")
         {
            context.HttpContext.Response.Redirect("/");
         }
      }
      ));

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
