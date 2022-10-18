using System.Globalization;
using System.Numerics;
using System.Text;

namespace RSACrackstation.Backend;

public class RSAEncrypter{
    private BigInteger _n;

    private BigInteger _m;
    private BigInteger _c;

    public BigInteger E{ get; set; } = 65537;

    public RSAEncrypter(string n, string e){
        _n = BigInteger.Parse(n);
        E = BigInteger.Parse(e);
    }

    public string Encrypt(string m){
        _m = BigInteger.Parse(m);
        _c = BigInteger.ModPow(_m, E, _n);
        return Convert.ToString(_c);
    }
}