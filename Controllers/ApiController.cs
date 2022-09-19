using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using RSACrackstation.Backend;

namespace RSACrackstation.Controllers;

public class ApiController : Controller{
    public string[] GetFactors(string inputNum){
        var cracker = new RSACracker(inputNum);
        return cracker.GetFactors();
    }

    public Dictionary<string, string> Decipher(string p, string q, string e, string ct){
        var cracker = new RSACracker(p, q);
        cracker.E = BigInteger.Parse(e);

        var output = new Dictionary<string, string>();
        output["N"] = cracker.N.ToString();
        output["d"] = cracker.GetD().ToString();

        if (ct is null){
            output["pt"] = "";
            output["ptAscii"] = "";
        }
        else{
            var pt = cracker.GetPlainText(ct).ToString();
            output["pt"] = pt;
            try{
                output["ptAscii"] = cracker.ConvertToAscii(pt);
            }
            catch{
                output["ptAscii"] = "Could not convert to ASCII";
            }
        }
        return output;
    }

    public string Encrypt(string N, string e, string pt){
        var encryptor = new RSAEncrypter(N, e);
        encryptor.E = BigInteger.Parse(e);
        return encryptor.Encrypt(pt);
    }
}