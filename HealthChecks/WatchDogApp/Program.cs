
HttpClient client = new HttpClient();

while (true)
{
    using var response = await client.GetAsync("https://localhost:44444/health");
    var status = await response.Content.ReadAsStringAsync();

    if (status == "Unhealthy")
    {
        Console.WriteLine($"{DateTime.Now.ToLongTimeString()} : сервер в нерабочем состоянии, осуществляется перезапуск.");
        await client.GetAsync("https://localhost:33333/reset");
    }
    else
    {
        Console.WriteLine($"{DateTime.Now.ToLongTimeString()} : все норм");
    }

    await Task.Delay(10000);   // задержка на 10 секунд
}