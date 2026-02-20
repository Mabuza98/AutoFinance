using AutoFinance.API.Services;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<FinanceRequestValidator>();
builder.Services.AddScoped<AiAdviceService>();
builder.Services.AddScoped<IFinancialCalculator, FinancialCalculator>();

// ✅ ADD THIS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
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

// ✅ ADD THIS (before Authorization)
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.UseMiddleware<AutoFinance.API.Middleware.ExceptionMiddleware>();

app.MapControllers();
app.Run();