using Newtonsoft.Json.Linq;
using System.Collections.Generic;



public class Program
{
    private static readonly HttpClient client = new HttpClient();
    static async Task Main()
    {
        List<double> list = new List<double>();
        string[] arrRates = { "EUR", "RUB", "USD" };
         await getExchange(arrRates);

    }
    private static async Task getExchange(string[] arrRates)
    {
        string url = "https://v6.exchangerate-api.com/v6/02c65cea68d5d61862413f1b/latest/USD";
        using HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        JObject data = JObject.Parse(responseBody);
        List<double> list = new List<double>();
        for (int i = 0; i < arrRates.Length; i++)
        {
            double value = data["conversion_rates"][$"{arrRates[i]}"].Value<double>();
            list.Add(value);
        }
        int count = 0;
        foreach (double val in list)
        {

            Console.WriteLine($"{arrRates[count]}:{val}");
            count++;
        }
    }

}
