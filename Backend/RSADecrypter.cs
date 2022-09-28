using System.Numerics;

namespace RSACrackstation.Backend;

public class RSADecrypter{
    private BigInteger _n;
    private BigInteger _d;
    private BigInteger _c;

    private BigInteger _m;
    
    public RSADecrypter(string n, string d, string c){
        _n = BigInteger.Parse(n);
        _d = BigInteger.Parse(d);
        _c = BigInteger.Parse(c);
    }
    
    public string Decrypt(){
        _m = BigInteger.ModPow(_c, _d, _n);
        return Convert.ToString(_m);
    }
}