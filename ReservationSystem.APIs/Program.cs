using ReservationSystem.DAL;
using ReservationSystem.BL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Default

builder.Services.AddControllers();
#region Swagger

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion


#endregion

#region Database

var connectionString = builder.Configuration.GetConnectionString("ResevationSystemDb");
builder.Services.AddDbContext<ReservationSystemContext>(options =>
    options.UseSqlServer(connectionString));

#endregion

#region Repos

builder.Services.AddScoped<IDepartmentsRepo, DepartmentsRepo>();
builder.Services.AddScoped<ITicketsRepo, TicketsRepo>();

#endregion

#region Managers

builder.Services.AddScoped<ITicketsManager, TicketsManager>();

#endregion

var app = builder.Build();

#region Middlewares

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion

