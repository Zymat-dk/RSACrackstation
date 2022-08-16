using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RSACrackstation.Models;
using System.Net;
using System.Text.Json.Nodes;

namespace RSACrackstation.Controllers;

public class ApiController : Controller {
    public int[] GetFactors(string inputNum) {
        var url = $"http://factordb.com/api?query={inputNum}";
        var request = WebRequest.Create(url);
        request.Method = "GET";

        using var webResponse = request.GetResponse();
        using var webStream = webResponse.GetResponseStream();

        using var reader = new StreamReader(webStream);
        var data = reader.ReadToEnd();

        var jsonData = JsonObject.Parse(data);

        Console.WriteLine(jsonData);

        int[] arr = { -1, -1 };

        return arr;
    }
}