var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<SmtpSettings>(
    builder.Configuration.GetSection("SmtpSettings")
);
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// 1. Load secrets into the SmtpSettings class
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
// 2. Register the service with a Transient lifetime
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();
