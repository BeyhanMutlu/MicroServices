using Microsoft.EntityFrameworkCore;
using ProductionService.AsyncDataServices;
using ProductionService.Data;
using ProductionService.Interfaces;
using ProductionService.SyncDataServices.Grpc;
using ProductionService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductRepo, ProductRepo>();

builder.Services.AddHttpClient<IOrderDataClient, HttpOrderDataClient>();

builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

builder.Services.AddGrpc();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Console.WriteLine($"---> Order service endpoint {builder.Configuration["OrderService"]}");

if (builder.Environment.IsProduction())
{
    Console.WriteLine("---> Using SqlServer Db");
    builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ProductionConn")));
    
}
else{
    Console.WriteLine("---> Using InMemory Db");
    builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseInMemoryDatabase("InMemoryDb"));
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

DbInit.InitDb(app, builder.Environment.IsProduction());

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints=>
{
    endpoints.MapControllers();

    endpoints.MapGrpcService<GrpcProductService>();

    endpoints.MapGet("/Protos/production.proto",async context =>
    {
        await context.Response.WriteAsync(File.ReadAllText("Protos/production.proto"));
    });
});

app.UseHttpsRedirection();
app.Run();

