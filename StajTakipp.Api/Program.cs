using Microsoft.EntityFrameworkCore;
using StajTakip.Data.Context;
using StajTakip.Data.Repository;
using StajTakip.Service;
using StajTakip.Service.Services;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<StajTakipDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IStajyerRepository, StajyerRepository>();
builder.Services.AddScoped<IStajyerService, StajyerService>();

builder.Services.AddScoped<IKurumRepository, KurumRepository>();
builder.Services.AddScoped<IKurumService, KurumService>();

builder.Services.AddScoped<IMentorRepository, MentorRepository>();
builder.Services.AddScoped<IMentorService, MentorService>();

builder.Services.AddScoped<IKullaniciRepository, KullaniciRepository>();
builder.Services.AddScoped<IKullaniciService, KullaniciService>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapControllers();

app.Run();
// NEW: Tarih formatý dönüþüm hatalarýný önlemek için yeni eklenen converter sýnýfý.
// JSON'dan gelen "11-06-2026", "11.06.2026" veya "2026-06-11" gibi farklý formatlarý otomatik parse eder.
public class CustomDateTimeConverter : JsonConverter<DateTime>
{
    private readonly string[] _formats = new[]
    {
        "dd-MM-yyyy",
        "dd.MM.yyyy",
        "dd/MM/yyyy",
        "yyyy-MM-dd",
        "yyyy-MM-ddTHH:mm:ss"
    };

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? dateString = reader.GetString();

        if (string.IsNullOrWhiteSpace(dateString))
            return DateTime.MinValue;

        if (DateTime.TryParseExact(dateString, _formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
        {
            return date;
        }

        if (DateTime.TryParse(dateString, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
        {
            return parsedDate;
        }

        throw new JsonException($"'{dateString}' geçerli bir tarih formatý deðil. Örnek format: 10-10-2026");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("dd-MM-yyyy"));
    }
}
//Þu ana kadar StajyerController, constructor'ýnda IStajyerService istiyor. Ama sistem þu an "IStajyerService istenirse ne vereceðimi bilmiyorum" durumunda — çünkü hiçbir yerde "IStajyerService istenirse StajyerService ver" diye bir kayýt yapmadýk. Ayný þekilde StajyerService de IStajyerRepository istiyor, onun için de bir kayýt gerekiyor.