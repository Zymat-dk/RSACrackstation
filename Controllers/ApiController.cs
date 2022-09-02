using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RSACrackstation.Models;
using System.Net;
using System.Numerics;
using System.Text.Json.Nodes;
using RSACrackstation.Backend;

namespace RSACrackstation.Controllers;

public class ApiController : Controller {
    public string[] GetFactors(string inputNum, bool isHex) {
        var cracker = new RSACracker(inputNum, isHex);
        return cracker.GetFactors();
    }

    public Dictionary<string, string> Decipher(string p, string q, string e, string ct, bool isHex) {
        var cracker = new RSACracker(p, q);
        cracker.E = BigInteger.Parse(e);

        var output = new Dictionary<string, string>();
        output["N"] = cracker.N.ToString();
        output["d"] = cracker.GetD().ToString();

        if (ct is null) {
            output["pt"] = "";
            output["ptAscii"] = "";
        }
        else {
            var pt = cracker.GetPlainText(ct, isHex).ToString();
            output["pt"] = pt;
            output["ptAscii"] = cracker.ConvertToAscii(pt);
        }
        

        return output;
    }
    
    public string Encrypt(string n, string e, string pt, bool isHex) {
        var encryptor = new RSAEncryptor(n, e);
        encryptor.E = BigInteger.Parse(e);
        return encryptor.Encrypt(pt, isHex);
    }
}