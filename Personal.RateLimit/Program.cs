using Personal.RateLimit.Middleware;
using Personal.RateLimit.Middleware.B;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RateLimitOptions>(builder.Configuration.GetSection("RateLimitOptions"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.Use((context, next) =>
{
    context.Request.EnableBuffering();
    return next();
});

//Strategy A
//app.UseRateLimiter();

//Strategy B
app.UseMiddleware<RateLimitMiddlware>();

app.MapControllers();

app.Run();