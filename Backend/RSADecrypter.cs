using System.Numerics;

namespace RSACrackstation.Backend;

public class RSADecrypter{
    private BigInteger _n;
    private BigInteger _d;
    private BigInteger _c;
    private BigInteger _m;
    
    public RSADecrypter(string n, string d){
        _n = BigInteger.Parse(n);
        _d = BigInteger.Parse(d);
    }
    
    public string Decrypt(string c){
        _c = BigInteger.Parse(c);
        _m = BigInteger.ModPow(_c, _d, _n);
        return Convert.ToString(_m);
    }
}