using System;
using System.Numerics;
using System.Security.Cryptography;

namespace RSACrackstation.Backend;

public class KeyGenerator{
    private BigInteger _p;
    private BigInteger _q;
    
    private BigInteger _n;
    private BigInteger _d;

    public BigInteger E { get; set; } = 65537;

    public BigInteger N {
        get { return _n; }
    }

    public KeyGenerator(){}

    public KeyGenerator(BigInteger e){
        E = e;
    }
    
    public BigInteger[] GenerateKeys(int keySize){
        using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider()) {
            byte[] val = new byte[keySize];
            crypto.GetBytes(val);
            int randomvalue = BitConverter.ToInt32(val, 1);
            Console.WriteLine(randomvalue);
        }

        return new BigInteger[] { 1, 2 };
    }
}