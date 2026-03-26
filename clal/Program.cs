using clal.Operations;
using clal.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IInterestRateCalculator, InterestRateCalculator>();
builder.Services.AddScoped<ICreditStatusCalculator, CreditStatusCalculator>();
builder.Services.AddScoped<IAccountAgeCalculator, AccountAgeCalculator>();

builder.Services.AddScoped<IWorkFlowOperation, HolidayDiscountOperation>();
builder.Services.AddScoped<IWorkFlowOperation, InterestRateOperation>();
builder.Services.AddScoped<IWorkFlowOperation, CreditStatusOperation>();
builder.Services.AddScoped<IWorkFlowOperation, AccountAgeOperation>();

builder.Services.AddScoped<IWorkFlowEngine, WorkFlowEngine>();
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
