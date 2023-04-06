using Microsoft.EntityFrameworkCore;
using TodoList.Application;
using TodoList.Application.Services.Todos;
using TodoList.Core;
using TodoList.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var appsettings = new AppSettings();
const string _appPolicy = "AppPolicy";
builder.Configuration.Bind(appsettings);
builder.Services.AddCors(
    options => options.AddPolicy(
        _appPolicy,
        builder => builder
                .WithOrigins(
                    appsettings.CORS!
                 )
                .SetIsOriginAllowed(isOriginAllowed: _ => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
    )
);

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(c => 
        c.UseSqlServer(appsettings.ConnectionStrings.AppConnection,
        b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
builder.Services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());

builder.Services.AddTransient<ITodoService, TodoService>();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}
app.UseCors(_appPolicy);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();

app.Run();
