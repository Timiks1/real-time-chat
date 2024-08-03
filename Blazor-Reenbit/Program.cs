using Blazor_Reenbit.database;
using Blazor_Reenbit.Hubs;
using Blazor_Reenbit.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSignalR().AddAzureSignalR("");
builder.Services.AddDbContext<ChatContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
    builder => builder.AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .WithOrigins("http://localhost:4200"));
});

builder.Services.AddSingleton(new SentimentAnalysisService(
    builder.Configuration["TextAnalytics:ApiKey"],
    builder.Configuration["TextAnalytics:Endpoint"]));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapRazorPages();
app.MapHub<ChatHub>("/chatHub");

app.MapGet("/", context =>
{
    context.Response.Redirect("/Chat");
    return Task.CompletedTask;
});

app.Run();
