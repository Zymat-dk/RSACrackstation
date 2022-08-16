using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RSACrackstation.Models;
using System.Net;
using System.Numerics;
using System.Text.Json.Nodes;
using RSACrackstation.Backend;

namespace RSACrackstation.Controllers;

public class ApiController : Controller {
    public string[] GetFactors(string inputNum) {
        var cracker = new RSACracker(inputNum);
        return cracker.GetFactors();
    }

    public Dictionary<string, string> Decipher(string p, string q, string e, string ct) {
        var cracker = new RSACracker(p, q);
        cracker.E = BigInteger.Parse(e);

        var output = new Dictionary<string, string>();
        output["N"] = cracker.N.ToString();
        output["d"] = cracker.Decipher().ToString();
        output["pt"] = "61 73 63 69 69 74 69 6E 67";
        output["ptAscii"] = "asciiting";

        return output;
    }
}