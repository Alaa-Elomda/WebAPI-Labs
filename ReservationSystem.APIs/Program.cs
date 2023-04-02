using ReservationSystem.DAL;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.BL;

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
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

#endregion

#region Managers

builder.Services.AddScoped<ITicketsManager, TicketsManager>();
builder.Services.AddScoped<IDepartmentsManager, DepartmentsManager>();

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

