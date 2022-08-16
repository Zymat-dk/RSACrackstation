using System.Net;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http.Features;

namespace RSACrackstation.Backend;

public class RSACracker {
    public int E { get; set; } = 6557;

    private string _n = "";
    private string _p = "";
    private string _q = "";
    private string _d = "";


    public RSACracker(string n) {
        _n = n;
    }

    public RSACracker(string p, string q) {
        _p = p;
        _q = q;
    }

    public string[] GetFactors() {
        string[] factors = { "-1", "-1" }; // Default values if the number, or the factors are not prime

        var url = $"http://factordb.com/api?query={this._n}"; // factordb api url
        var request = WebRequest.Create(url);
        request.Method = "GET"; // Use GET request

        using var webResponse = request.GetResponse();
        using var webStream = webResponse.GetResponseStream();

        using var reader = new StreamReader(webStream);
        var data = reader.ReadToEnd();

        dynamic jsonData = JsonObject.Parse(data); // parse json data

        if (jsonData["status"].ToString() != "FF" || jsonData["factors"].Count != 2) {
            // If the number is prime, or has more than two factors, return -1 -1
            return factors;
        }

        if (jsonData["factors"][0][1].ToString() != "1" || jsonData["factors"][1][1].ToString() != "1") {
            // Check for multiple of the same factor, and return -1 -1 if found
            return factors;
        }

        for (var i = 0; i < 2; i++) {
            // Get the two factors from the json data
            factors[i] = jsonData["factors"][i][0].ToString();
        }

        _p = factors[0];
        _q = factors[1];

        return factors;
    }
}