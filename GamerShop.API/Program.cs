using GamerShop.Core.Contracts;
using GamerShop.Core.Repositories;
using GamerShop.Core.Services;
using MongoDB.Driver;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var log = new LoggerConfiguration()
    .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

Log.Logger = log;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//repositories
builder.Services.AddTransient<IUserRepository, UserRepository>(_ => new UserRepository());
builder.Services.AddTransient<IProductRepository, ProductRepository>(_ => new ProductRepository());
builder.Services.AddTransient<IOrderRepository, OrderRepository>(_ => new OrderRepository());
builder.Services.AddTransient<IMongoDbRepository, MongoDbRepository>();
builder.Services.AddSingleton<IMongoClient, MongoClient>(_ => new MongoClient("mongodb+srv://edgarsokol:lala4444@cluster0.kdpdd.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0"));
//Services
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IBusinessLogicService, BusinessLogicService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("https://localhost:5180")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

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
