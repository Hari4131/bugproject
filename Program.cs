using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ticket_ManagementAPI.Common;
using Ticket_ManagementAPI.Data;
using Ticket_ManagementAPI.IRepository;
using Ticket_ManagementAPI.Model;
using Ticket_ManagementAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// IdentityServer
builder.Services.AddIdentityServer()
    .AddAspNetIdentity<ApplicationUser>()
    .AddInMemoryClients(Config.Clients)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddDeveloperSigningCredential();

builder.Services.AddControllers();
builder.Services.AddScoped<IHome, HomeRepository>();
builder.Services.AddScoped<ITicket, TicketRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

var app = builder.Build();

// Seed roles & data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RoleInitializer.SeedRolesAndUsers(services);
    await RoleInitializer.SeedEntities(services);
    await RoleInitializer.TicketStatus(services);
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAngularApp");

app.UseIdentityServer();

app.UseAuthorization();

app.MapControllers();

app.Run();
