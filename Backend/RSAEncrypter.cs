using System.Numerics;

namespace RSACrackstation.Backend;

public class RSAEncrypter{
    private BigInteger _n;
    private BigInteger _e;
    
    private BigInteger _m;
    private BigInteger _c;
    
    public BigInteger E{ get; set; } = 65537;
    
    public RSAEncrypter(string n, string e){
        _n = BigInteger.Parse(n);
        _e = BigInteger.Parse(e);
    }
    
    public string Encrypt(string m, bool isHex = false){
        _m = BigInteger.Parse(m);
        _c = BigInteger.ModPow(_m, _e, _n);
        return Convert.ToString(_c);
    }
    
}