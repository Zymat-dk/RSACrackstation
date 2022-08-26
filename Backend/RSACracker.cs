using System.Globalization;
using System.Net;
using System.Numerics;
using System.Text;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http.Features;

namespace RSACrackstation.Backend;

public class RSACracker {
    private BigInteger _n;
    private BigInteger _p;
    private BigInteger _q;
    private BigInteger _d;

    public BigInteger E { get; set; } = 65537;


    public BigInteger N {
        get { return _n; }
    }

    public BigInteger[] Factors {
        get { return new BigInteger[] { _p, _q }; }
    }

    public RSACracker(string n, bool isHex = false) {
        if (!isHex) {
            _n = BigInteger.Parse(n);
        }
        else {
            if (n.Substring(0, 2) == "0x") {
                n = n.Substring(2);
            }

            _n = BigInteger.Parse(n, NumberStyles.AllowHexSpecifier);
        }
    }

    public RSACracker(string p, string q) {
        _p = BigInteger.Parse(p);
        _q = BigInteger.Parse(q);

        _n = _p * _q;
    }

    public string[] GetFactors() {
        if ((_p != 0 && _q != 0) || _n == 0) {
            return new string[] { _p.ToString(), _q.ToString() };
        }

        string[] factors = { "-1", "-1" }; // Default values if the number, or the factors are not prime

        var url = $"http://factordb.com/api?query={_n}"; // factordb api url
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

        _p = BigInteger.Parse(factors[0]);
        _q = BigInteger.Parse(factors[1]);

        return factors;
    }

    public BigInteger GetD() {
        if (_d != 0 || _p == 0 || _q == 0) {
            // If the d value already exists, return it. If the factors are not found, return 0
            return _d;
        }

        var phi_n = (_p - 1) * (_q - 1);
        _d = EGCD(E, phi_n);

        return _d;
    }

    public BigInteger GetPlainText(string cipherText) {
        var cipher = BigInteger.Parse(cipherText);
        var pt = BigInteger.ModPow(cipher, _d, _n);
        return pt;
    }

    public string ConvertToAscii(string pt) {
        var plain = BigInteger.Parse(pt);
        string hex = plain.ToString("X"); // Convert to hex
        string ascii = "";
        for (var i = 0; i < hex.Length; i += 2) {
            var hexPair = hex.Substring(i, 2);
            var asciiChar = (char)Convert.ToByte(hexPair, 16);
            ascii += asciiChar;
        }

        return ascii;
    }

    private BigInteger EGCD(BigInteger a, BigInteger b) {
        BigInteger x = 0;
        BigInteger y = 1;
        BigInteger u = 1;
        BigInteger v = 0;

        while (a != 0) {
            var q = BigInteger.Divide(b, a);
            var r = b % a;
            var m = x - u * q;
            var n = y - v * q;
            (b, a, x, y, u, v) = (a, r, u, v, m, n);
            var gcd = b;
        }

        return x;
    }
}