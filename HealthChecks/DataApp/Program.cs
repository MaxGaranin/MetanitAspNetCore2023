
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// При запуске из студии не работает
// builder.WebHost.UseUrls("https://[::]:33333");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/reset", async () =>
{
    Latency.ResetLatency();
    return "Application reset";
});

app.MapGet("/data", async () =>
{
    int latency = Latency.GetLatency();
    await Task.Delay(latency);
    return $"Application latency: {latency}";
});

//app.MapGet("/get-json", async () =>
//{
//    //return new { A = 1 };
//    return (Result) null;
//});

app.Run();

class Result
{
    public int A { get; set; }
}