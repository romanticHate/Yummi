using Yummi.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Add Controllers to the container
builder.Services.AddControllers();
// Add Db service(s) to the container.
builder.Services.AddDbContext(builder.Configuration);
// Add service(s) to the container.
builder.Services.AddApplication();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
