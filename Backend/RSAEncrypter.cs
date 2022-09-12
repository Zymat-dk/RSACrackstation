using System.Globalization;
using System.Numerics;
using System.Text;

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

    public string Encrypt(string m){
        _m = BigInteger.Parse(m);
        _c = BigInteger.ModPow(_m, _e, _n);
        return Convert.ToString(_c);
    }

    public string FromHex(string hex){
        BigInteger dec = BigInteger.Parse(hex, NumberStyles.AllowHexSpecifier);
        string pt = dec.ToString();
        return pt;
    }

    public string FromAscii(string m){
        StringBuilder sb = new StringBuilder();
        byte[] inputBytes = Encoding.UTF8.GetBytes(m);

        foreach (byte b in inputBytes){
            sb.Append(string.Format("{0:x2}", b));
        }

        string hex = sb.ToString();
        return FromHex(hex);
    }
}