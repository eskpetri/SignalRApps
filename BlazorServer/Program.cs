using BlazorServer.Data;
using Microsoft.AspNetCore.ResponseCompression;   //SignalR
using BlazorServer.Hubs;                          //SignalR

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});                                                    //SignalR

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

app.MapBlazorHub();                 //SignalR
app.MapHub<ChatHub>("/chathub");    //SignalR   Client side address
app.MapHub<CounterHub>("/counterhub");  //SignarR listening address
app.MapFallbackToPage("/_Host");    

app.Run();
