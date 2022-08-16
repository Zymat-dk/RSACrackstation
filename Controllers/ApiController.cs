using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RSACrackstation.Models;
using System.Net;
using System.Text.Json.Nodes;

namespace RSACrackstation.Controllers;

public class ApiController : Controller {
    public string[] GetFactors(string inputNum) {
        string[] factors = { "-1", "-1" };  // Default values if the number, or the factors are not prime
        
        var url = $"http://factordb.com/api?query={inputNum}";  // factordb api url
        var request = WebRequest.Create(url);
        request.Method = "GET";  // Use GET request

        using var webResponse = request.GetResponse();
        using var webStream = webResponse.GetResponseStream();

        using var reader = new StreamReader(webStream);
        var data = reader.ReadToEnd();

        dynamic jsonData = JsonObject.Parse(data);  // parse json data

        if (jsonData["status"].ToString() != "FF" || jsonData["factors"].Count != 2) {
            // If the number is prime, or has more than two factors, return -1 -1
            return factors;
        }

        if (jsonData["factors"][0][1].ToString() != "1" || jsonData["factors"][1][1].ToString() != "1") {
            // Check for multiple of the same factor, and return -1 -1 if found
            return factors;
        }
        
        for(var i=0; i<2; i++){
            // Get the two factors from the json data
            factors[i] = jsonData["factors"][i][0].ToString();
        }

        return factors;
    }
}