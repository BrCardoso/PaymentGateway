using Application.Shared.DependencyInjection;
using Application.Features.PaymentProcessor.DependencyInjection;
using Application.Shared.AcquiringBank;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration configuration = builder.Configuration;

builder.Services.AddMemoryCache();
builder.Services.AddStorageExtension();
builder.Services.AddScoped<IAcquiringBankAuth, AcquiringBankAuth>();
builder.Services.AddPaymentProcessor();
builder.Services.AddHttpFactoryExtension(configuration);
builder.Services.AddMediatRExtension();

builder.Services.AddControllers();
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
