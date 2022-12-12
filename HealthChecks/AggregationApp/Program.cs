var builder = WebApplication.CreateBuilder(args);

// ��� ������� �� ������ �� ��������
// builder.WebHost.UseUrls("https://[::]:44444");

builder.Services.AddHealthChecks()
    .AddCheck<RequestTimeHealthCheck>("RequestTimeCheck");

builder.Services.AddHttpClient();   // ���������� HttpClient

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
    // ���������� ������ � ������� ������� � ���������� ��� �����
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