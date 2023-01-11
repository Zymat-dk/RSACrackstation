using System.Globalization;
using System.Net;
using System.Numerics;
using System.Text;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http.Features;

namespace RSACrackstation.Backend;

public class RSACracker{
    private BigInteger _n;
    private BigInteger _p;
    private BigInteger _q;
    private BigInteger _d;

    public BigInteger E{ get; set; } = 65537;


    public BigInteger N{
        get { return _n; }
    }

    public RSACracker(string n){
        _n = BigInteger.Parse(n);
    }

    public RSACracker(string p, string q){
        _p = BigInteger.Parse(p);
        _q = BigInteger.Parse(q);

        _n = _p * _q;
    }

    public RSACracker(BigInteger p, BigInteger q, BigInteger e){
        _p = p;
        _q = q;
        E = e;

        _n = _p * _q;
    }

    public string[] GetFactors(){
        // Get factors of N, by getting data from factordb
        if ((_p != 0 && _q != 0) || _n == 0){
            // If we already have the factors, or N is 0, return them
            return new string[] { _p.ToString(), _q.ToString() };
        }

        string[] factors = { "-1", "-1" }; // Default return value

        var url = $"http://factordb.com/api?query={_n}"; // factordb api url, with N as query
        var request = WebRequest.Create(url);
        request.Method = "GET"; // Use GET request

        var data = "";
        // Read response using StreamReader, and store it in data
        try{
            using var webResponse = request.GetResponse();
            using var webStream = webResponse.GetResponseStream();
            using var reader = new StreamReader(webStream);
            data = reader.ReadToEnd();
        }
        catch (System.Net.WebException){
            // Return -2, -2 if factordb is offline
            (factors[0], factors[1]) = ("-2", "-2");
            return factors;
        }

        dynamic jsonData = JsonObject.Parse(data); // parse json data

        if (jsonData["status"].ToString() != "FF" || jsonData["factors"].Count != 2){
            // If the number is prime, or has more than two factors, return -1 -1
            if (jsonData["factors"].Count == 1 && jsonData["factors"][0][1].ToString() == "2"){
                // If the number is a perfect square, it is allowed to have only one factor
                var factor = jsonData["factors"][0][0].ToString();
                return new string[] { factor, factor };
            }

            return factors;
        }

        if (jsonData["factors"][0][1].ToString() != "1" || jsonData["factors"][1][1].ToString() != "1"){
            // Check for multiple of the same factor, and return -1 -1 if found
            Console.WriteLine("Multiple of the same factor found");
            return factors;
        }

        for (var i = 0; i < 2; i++){
            // Get the two factors from the json data
            factors[i] = jsonData["factors"][i][0].ToString();
        }

        // Set the p and q fields to the factors
        _p = BigInteger.Parse(factors[0]);
        _q = BigInteger.Parse(factors[1]);

        return factors;
    }

    public BigInteger GetD(){
        if (_p == 0 || _q == 0){
            // If the factors are not set, return -1
            return -1;
        }

        if (_d != 0){
            // If the d value already exists, return it
            return _d;
        }

        var phi_n = (_p - 1) * (_q - 1); // Calculate phi(n)
        _d = EGCD(E, phi_n); // Run the extended euclidean algorithm to get d
        _d = _d % phi_n;
        if (_d < 0){
            // If d is negative, add phi(n) to it
            _d += phi_n;
        }

        return _d;
    }

    public BigInteger GetPlainText(string cipherText, bool isHex = false){
        var decimalCipher = BigInteger.Zero;

        if (isHex){
            if (cipherText.Substring(0, 2) == "0x"){
                cipherText = cipherText.Substring(2);
            }

            decimalCipher = BigInteger.Parse(cipherText, NumberStyles.AllowHexSpecifier);
        }
        else{
            decimalCipher = BigInteger.Parse(cipherText);
        }

        // Calculate the plaintext using the formula: m = c^d mod n
        var pt = BigInteger.ModPow(decimalCipher, _d, _n);
        return pt;
    }

    public static string ToAscii(string pt){
        var plain = BigInteger.Parse(pt);
        string hex = plain.ToString("X"); // Convert to hex
        string ascii = "";
        for (var i = 0; i < hex.Length; i += 2){
            // Iterate over the hex string, and convert each 2 characters to an ascii character
            var hexPair = hex.Substring(i, 2);
            var asciiChar = (char)Convert.ToByte(hexPair, 16);
            ascii += asciiChar; // Append the ascii character to the string
        }

        return ascii;
    }

    private BigInteger EGCD(BigInteger a, BigInteger b){
        // Runs the extended euclidean algorithm to find the modular inverse of a mod b
        BigInteger x = 0;
        BigInteger y = 1;
        BigInteger u = 1;
        BigInteger v = 0;

        while (a != 0){
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