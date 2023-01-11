using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RSACrackstation.Backend;

namespace RSACrackstation.Controllers;

public class ApiController : Controller{
    public string[] GetFactors(string inputNum){
        var cracker = new RSACracker(inputNum);
        return cracker.GetFactors();
    }

    public Dictionary<string, string> Decrypt(string p, string q, string e, string ct){
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
                output["ptAscii"] = RSACracker.ToAscii(pt);
            }
            catch{
                output["ptAscii"] = "Could not convert to ASCII";
            }
        }
        return output;
    }

    public string Encrypt(string N, string e, string pt){
        var encrypter = new RSAEncrypter(N, e);
        return encrypter.Encrypt(pt);
    }
    
    public string Decipher(string N, string d, string ct){
        var decrypter = new RSADecrypter(N, d);
        return decrypter.Decrypt(ct);
    }
    
    public Dictionary<string, string> GenerateKeys(int keySize, int e = 65537){
        KeyGenerator kg = (e < 1) ? new KeyGenerator() : new KeyGenerator(e);
        
        var output = kg.GenerateKeys(keySize);
        return output;
    }
    
    public Dictionary<string, string> SmallE(string N, string e, string ct){
        Console.WriteLine("WHOOO");
        Console.WriteLine(e);
        var cracker = new RSACracker(N);
        cracker.E = BigInteger.Parse(e);
        return cracker.SmallE(ct);
    }
}
