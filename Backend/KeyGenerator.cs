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

    public BigInteger E{ get; set; } = 65537;

    public BigInteger N{
        get { return _n; }
    }

    public KeyGenerator(){}

    public KeyGenerator(BigInteger e){
        E = e;
    }

    public Dictionary<string, string> GenerateKeys(int keySize){
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
            return new Dictionary<string, string>()
            {
                { "status", "error" }, { "message", "Could not connect to the number generation server" }
            };
        }

        dynamic jsonData = JsonObject.Parse(data); // parse json data

        if (jsonData["status"].ToString() != "success"){
            return new Dictionary<string, string>()
            {
                { "status", "error" }, { "message", "Unexpected error on the number generation server" }
            };
        }
        
        _p = BigInteger.Parse(jsonData["numbers"][0].ToString());
        _q = BigInteger.Parse(jsonData["numbers"][1].ToString());
        _n = _p * _q;
        
        return new Dictionary<string, string>()
        {
            {"status", "success"}, {"message", "Keys generated successfully"},
            {"p", _p.ToString()}, {"q", _q.ToString()}, {"N", _n.ToString()}
        };
    }
}