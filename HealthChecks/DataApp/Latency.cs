







internal static class Latency
{
    static int counter = 1;

    // увеличиваем счетчик
    public static int GetLatency() => counter++ * 500;
    
    // сбрасываем счетчик
    public static void ResetLatency() => counter = 1;
}