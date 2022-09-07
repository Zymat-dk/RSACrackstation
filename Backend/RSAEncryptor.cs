using System.Numerics;

namespace RSACrackstation.Backend;

public class RSAEncryptor{
    private BigInteger _n;
    private BigInteger _e;
    
    private BigInteger _p;
    private BigInteger _c;
    
    public BigInteger E{ get; set; } = 65537;
    
    public RSAEncryptor(string n, string e){
        _n = BigInteger.Parse(n);
        _e = BigInteger.Parse(e);
    }
    
    public string Encrypt(string m, bool isHex = false){
        _p = BigInteger.Parse(m);
        _c = BigInteger.ModPow(_p, _e, _n);
        return Convert.ToString(_c);
    }
    
}