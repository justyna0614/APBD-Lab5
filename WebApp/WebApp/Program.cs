using System.Text.Json.Serialization;
using WebApp.Context;
using WebApp.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddDbContext<MasterContext>();
        builder.Services.AddScoped<TripService, TripService>();
        builder.Services.AddScoped<ClientService, ClientService>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            var enumConverter = new JsonStringEnumConverter();
            options.JsonSerializerOptions.Converters.Add(enumConverter);
        });


        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}