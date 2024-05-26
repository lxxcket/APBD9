using APBD9;
using APBD9.Policy;
using APBD9.Repository;
using APBD9.UseCase;
using Microsoft.EntityFrameworkCore;

public class Program
{
    
    public static void Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);
        
        //Registering services
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        var connectionString = builder.Configuration.GetConnectionString("MyConnectionString");
        builder.Services.AddDbContext<MasterContext>(option => option.UseSqlServer(connectionString));
        
        builder.Services.AddScoped<IPagingPolicy, PagingPolicy>();
        builder.Services.AddScoped<IClientTripConnectionExistsPolicy, ClientTripConnectionExistsPolicy>();
        builder.Services.AddScoped<ICreateClientValidPolicy, CreateClientValidPolicy>();
        
        builder.Services.AddScoped<IClientTripsRepository, ClientTripsRepository>();
        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddScoped<ITripRepository,TripRepository>();
        
        builder.Services.AddScoped<ICreateClientAndAssignToTheTripUseCase, CreateClientAndAssignToTheTripUseCase>();
        builder.Services.AddScoped<IGetTripsUseCase, GetTripsUseCase>();
        builder.Services.AddScoped<IDeleteClientUseCase, DeleteClientUseCase>();
        
        var app = builder.Build();

        //Configuring the HTTP request pipeline
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