using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Sender;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ServiceBusClient>(_ =>
{
    var serviceBusNamespace = _.GetService<IConfiguration>()["ServiceBus:Namespace"];
    return new ServiceBusClient(serviceBusNamespace, new DefaultAzureCredential());
});
builder.Services.AddSingleton<ServiceBusSenderProvider>();

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