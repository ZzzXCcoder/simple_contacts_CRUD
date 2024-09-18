using contacts_CRUD.ContactRepository;
using contacts_CRUD.ContactServices;
using contacts_CRUD.Data.AppDbContext;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSqlite<ContactDbContext>(connString);

builder.Services.AddScoped<ContactDbContext>();

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();



builder.Services.AddControllers();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "ContactApi", Version = "v1" });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{   
    app.UseSwagger();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.MapControllers();

app.Run();
