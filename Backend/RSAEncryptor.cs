using System.Numerics;

namespace RSACrackstation.Backend;

public class RSAEncryptor{
    private BigInteger _n;
    private BigInteger _e;
    
    private BigInteger _p;
    private BigInteger _c;
    
    public BigInteger E{ get; set; } = 65537;
    
    public RSAEncryptor(BigInteger n, BigInteger e){
        _n = n;
        _e = e;
    }
    
    public BigInteger Encrypt(BigInteger m){
        _p = m;
        _c = BigInteger.ModPow(m, _e, _n);
        return _c;
    }
}