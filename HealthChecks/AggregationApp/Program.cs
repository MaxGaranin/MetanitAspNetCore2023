var builder = WebApplication.CreateBuilder(args);

// ѕри запуске из студии не работает
// builder.WebHost.UseUrls("https://[::]:44444");

builder.Services.AddHealthChecks()
    .AddCheck<RequestTimeHealthCheck>("RequestTimeCheck");

builder.Services.AddHttpClient();   // подключаем HttpClient

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapHealthChecks("/health");

app.MapGet("/", async (HttpClient httpClient) =>
{
    // отправл€ем запрос к другому сервису и возвращаем его ответ
    var response = await httpClient.GetAsync("https://localhost:33333/data");
    return await response.Content.ReadAsStringAsync();
});

app.MapGet("/get-json", async (HttpClient httpClient) =>
{
    var response = await httpClient.GetAsync("https://localhost:33333/get-json");
    return await response.Content.ReadFromJsonAsync<Result>();
});

app.Run();

class Result
{
    public int A { get; set; }
}