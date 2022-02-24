using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Vocabulary1;
using Document;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

          var docs = Directory.GetFiles("../Content","*.txt");//cargo los documentos
         
          Documents[] documents =  new Documents[docs.Length];//creo el array de los documentos
          for(int i = 0 ; i < docs.Length ; i++)
          {
              documents[i] = new Documents(docs[i]);//los guardo 
          }
          Vocabulary vocabulary =  new Vocabulary(documents);//creo mi vocabulario

          MoogleEngine.Moogle.seeker = vocabulary;

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
