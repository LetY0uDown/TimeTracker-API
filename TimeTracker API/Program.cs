using TimeTracker.API.Services;
using TimeTracker.API.Tools;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureAPI();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabase();
builder.Services.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapHub<MainHub>("MainHub");

app.Run();
