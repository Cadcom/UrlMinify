using Microsoft.EntityFrameworkCore;
using UrlMinify.Business.Abstract;
using UrlMinify.Business.Concrete;
using UrlMinify.Data;
using UrlMinify.Data.Abstract;
using UrlMinify.Data.Concrate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<dbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["myConnectionString"], b => b.MigrationsAssembly("UrlMinify.Data"));
});
builder.Services.AddScoped<DbContext>(provider => provider.GetService<dbContext>());

builder.Services.AddScoped<IDatabaseHelper, DatabaseHelper>();
builder.Services.AddScoped<IUrlService, UrlService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();
app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();

app.Run();
