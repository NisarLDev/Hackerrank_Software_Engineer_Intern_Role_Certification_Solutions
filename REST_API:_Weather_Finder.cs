using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

class Result
{

    /*
     * Complete the 'getTemperature' function below.
     *
     * URL for cut and paste
     * https://jsonmock.hackerrank.com/api/weather?name=<name>
     *
     * The function is expected to return an Integer.
     * The function accepts a singe parameter name.
     */
    private static readonly HttpClient client = new HttpClient();
    
    public static int getTemperature(string name)
    {
        // Create the URL with the provided city name
        string url = $"https://jsonmock.hackerrank.com/api/weather?name={name}";

        try
        {
            // Perform an HTTP GET request to fetch weather information
            HttpResponseMessage response = client.GetAsync(url).Result;

            // Check if the response is successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as string
                string responseBody = response.Content.ReadAsStringAsync().Result;

                // Deserialize the JSON response
                dynamic weatherData = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);

                // Extract the temperature from the first weather record
                string weather = weatherData.data[0].weather;
                int temperature = int.Parse(weather.Split(' ')[0]); // Extract integer part

                return temperature;
            }
            else
            {
                throw new Exception($"Failed to fetch weather data for {name}. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions and return -1 indicating failure
            Console.WriteLine($"An error occurred: {ex.Message}");
            return -1;
        }
    }

}
class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string name = Console.ReadLine();

        int result = Result.getTemperature(name);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
