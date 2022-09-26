using System;
using System.Net;
using System.Numerics;
using System.Security.Cryptography;
using System.Text.Json.Nodes;

namespace RSACrackstation.Backend;

public class KeyGenerator{
    private BigInteger _p;
    private BigInteger _q;
    
    private BigInteger _n;
    private BigInteger _d;

    public BigInteger E { get; set; } = 65537;

    public BigInteger N {
        get { return _n; }
    }

    public KeyGenerator(){}

    public KeyGenerator(BigInteger e){
        E = e;
    }
    
    public BigInteger[] GenerateKeys(int keySize){
        BigInteger[] numbers = { -1, -1 }; // Default values if the key generation fails

        var url = $"http://127.0.0.1:8080/?size={keySize}"; // local python server for number generation
        var request = WebRequest.Create(url);
        request.Method = "GET"; // Use GET request

        var data = "";
        try{
            using var webResponse = request.GetResponse();
            using var webStream = webResponse.GetResponseStream();
            using var reader = new StreamReader(webStream);
            data = reader.ReadToEnd();
        }
        catch (System.Net.WebException){
            (numbers[0], numbers[1]) = (-2, -2);
            return numbers;
        }
        
        dynamic jsonData = JsonObject.Parse(data); // parse json data
        
        if (jsonData["status"].ToString() != "success") {
            // If the number is prime, or has more than two factors, return -1 -1
            return numbers;
        }
        for (int i = 0; i<2; i++)
        {
            numbers[i] = BigInteger.Parse(jsonData["numbers"][i].ToString());
        }

        return numbers;
    }
}