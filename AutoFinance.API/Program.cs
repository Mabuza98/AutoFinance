using AutoFinance.API.Services;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// ------------------------
// Add services to container
// ------------------------
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Validation
builder.Services.AddValidatorsFromAssemblyContaining<FinanceRequestValidator>();

// Your services
builder.Services.AddScoped<AiAdviceService>();
builder.Services.AddScoped<IFinancialCalculator, FinancialCalculator>();

// CORS Policy - allow local dev and deployed frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins(
            "http://localhost:3000",                  // React dev server
            "https://autofinanceai.netlify.app"      // replace with your Netlify URL
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// ------------------------
// Set port for Render
// ------------------------
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

// ------------------------
// Middleware
// ------------------------

// Enable Swagger for both dev and production
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoFinance API v1");
});

// HTTPS redirection
app.UseHttpsRedirection();

// Apply CORS before authorization
app.UseCors("AllowReactApp");

app.UseAuthorization();

// Exception middleware
app.UseMiddleware<AutoFinance.API.Middleware.ExceptionMiddleware>();

// ------------------------
// Root endpoint (fixes 404 at /)
// ------------------------
app.MapGet("/", () => "AutoFinance API is running");

// Map controllers
app.MapControllers();

// Run the app
app.Run();

