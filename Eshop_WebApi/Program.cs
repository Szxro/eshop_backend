using Eshop_Application;
using Eshop_Infrastructure;
using Eshop_Infrastructure.Persistence;
using Eshop_WebApi.Extensions;
using Eshop_WebApi.Filters;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddApplication(builder.Configuration);
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddInfrastructureAuthentication(builder.Configuration);
    builder.Services.AddControllers(options =>
    {
        //Adding the filter globaly for all the controller
        options.Filters.Add<ExceptionHandlerFilter>();
    });
    //Adding the cors and policy
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("allowAll", policy => 
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });
    });
    builder.Services.AddEndpointsApiExplorer();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.ConfigureSwagger(); //Adding the Swagger and Configuring it for Auth
    builder.Configuration.AddUserSecrets<Program>(optional: false,reloadOnChange: true); // Adding the user secrets
}

var app = builder.Build();
{ 
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        await app.InitializeDatabaseAsync(); // Seeding and Making the migration
    }

    app.UseHttpsRedirection();

    app.UseCors("allowAll"); //Adding the cors

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
