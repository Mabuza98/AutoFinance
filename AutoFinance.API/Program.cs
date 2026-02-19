using AutoFinance.API.Services;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Validation & Services
builder.Services.AddValidatorsFromAssemblyContaining<FinanceRequestValidator>();
builder.Services.AddScoped<AiAdviceService>();
builder.Services.AddScoped<IFinancialCalculator, FinancialCalculator>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // React dev server
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

// ✅ Enable Swagger for all environments
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoFinance API V1");
});

app.UseHttpsRedirection();

// Apply CORS before authorization
app.UseCors("AllowReactApp");

app.UseAuthorization();

// Exception middleware
app.UseMiddleware<AutoFinance.API.Middleware.ExceptionMiddleware>();

app.MapControllers();

app.Run();

