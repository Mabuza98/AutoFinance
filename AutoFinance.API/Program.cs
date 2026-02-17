using AutoFinance.API.Services;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Validation
builder.Services.AddValidatorsFromAssemblyContaining<FinanceRequestValidator>();

// Your services
builder.Services.AddScoped<AiAdviceService>();
builder.Services.AddScoped<IFinancialCalculator, FinancialCalculator>();

//  CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // React dev server
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// app.UseHttpsRedirection();

//  Apply CORS before authorization
app.UseCors("AllowReactApp");

app.UseAuthorization();

// Exception middleware
app.UseMiddleware<AutoFinance.API.Middleware.ExceptionMiddleware>();

app.MapControllers();
app.Run();

