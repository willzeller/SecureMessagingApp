using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessagingApp
{

public struct PrivateKey
{
    public ulong privateExponent;
    public ulong n;
}
public struct PublicKey
{
    public ulong publicExponent;
    public ulong n;
}
public struct KeyPair
{
    public PrivateKey privateKey;
    public PublicKey publicKey;
}

class Encryption
{
    //TODO: make the random function be actually cryptographically random
    //TODO: find a way to make the random function spit out much larger integers
    Random random = new Random();

       /**********************
        *                    *
        *       R S A        *
        *                    *
        **********************/

    // String encrypt( string message, public key)
    public byte[] RSAEncrpyt(string message, PublicKey publicKey)
    {

        // pads it so it's length is multiples of four
        int len = message.Length;
        int paddingNeeded = len % 4;
        for (int i = 0; i < paddingNeeded; i++)
        {
            message += " ";
        }

        // holds the ciphertext
        byte[] ciphertext = new byte[message.Length];


        // loops through message, encrypts blocks at a time
        for (int i = 0; i < message.Length; i += 4)
        {
            byte[] miniCipherText = encryptBlock(message.Substring(i, 4), publicKey);
            ciphertext[i] = miniCipherText[0];
            ciphertext[i + 1] = miniCipherText[1];
            ciphertext[i + 2] = miniCipherText[2];
            ciphertext[i + 3] = miniCipherText[3];
        }

        return ciphertext;
    }

    // Encrypts the block of four characters at a time
    public byte[] encryptBlock(string fourChars, PublicKey publicKey)
    {
        ulong digit1 = (ulong)fourChars[0];
        ulong digit2 = (ulong)fourChars[1];
        ulong digit3 = (ulong)fourChars[2];
        ulong digit4 = (ulong)fourChars[3];

        // shifts to the left
        ulong plainText = (digit1 << 24) + (digit2 << 16) + (digit3 << 8) + digit4;

        ulong cipherBlock = powerModN(plainText, publicKey.publicExponent, publicKey.n);

        byte[] byteArray = new byte[4];

        // puts everything into the byte array
        byteArray[0] = (byte)((cipherBlock & 0xFF000000) >> 24);
        byteArray[1] = (byte)((cipherBlock & 0x00FF0000) >> 16);
        byteArray[2] = (byte)((cipherBlock & 0x0000FF00) >> 8);
        byteArray[3] = (byte)((cipherBlock & 0x000000FF));

        return byteArray;
    }

    // decrypts a block of four bytes
    public string decryptBlock(byte[] decryptMe, PrivateKey privateKey)
    {
        ulong cipher = ((ulong)decryptMe[0] << 24) + ((ulong)decryptMe[1] << 16) + ((ulong)decryptMe[2] << 8) + ((ulong)decryptMe[3]);

        ulong wordBlock = powerModN(cipher, privateKey.privateExponent, privateKey.n);

        string message = "";

        message += (char)((wordBlock & 0xFF000000) >> 24);
        message += (char)((wordBlock & 0x00FF0000) >> 16);
        message += (char)((wordBlock & 0x0000FF00) >> 8);
        message += (char)((wordBlock & 0x000000FF));

        return message;
    }

    // RSA Decrypt
    public string RSADecrypt(byte[] byteArray, PrivateKey privateKey)
    {
        string message = "";
            
        for (int i = 0; i < byteArray.Length; i += 4)
        {
            string miniMessage = decryptBlock(byteArray.Skip(i).Take(4).ToArray(), privateKey);
            message += miniMessage;
                        }

        return message;
    }

    public KeyPair generateKeyPair()
    {
        KeyPair keys = new KeyPair();

        // I use letters because these are the standard letters for RSA Encryption in
        // My number theory textbook 
        ulong n = 0, p = 0, q = 0;

        while (n < 0xFFFFFFFF)
        {
            //Generate two distinct primes
            p = (ulong)generatePrime();
            q = (ulong)generatePrime();

            while (p == q) { q = (ulong)generatePrime(); }

            //calculate n
            n = p * q;

        }
        keys.publicKey.n = n;
        keys.privateKey.n = n;

        ulong k = (ulong)random.Next(2, int.MaxValue);

        // This is Euler's (pronounced Oiler) phi function
        // Euler's totient function combined with Fermat's Little Theorem
        // give us our lovely RSA algorithm 

        ulong phiOfN = (p - 1) * (q - 1);
        while (GCD(phiOfN, k) != 1) { k = (ulong)random.Next(2, int.MaxValue); }

        ulong j = phiOfN / k;

        keys.publicKey.publicExponent = k;
        keys.privateKey.privateExponent = j;

        return keys;

    }

  
    /*So, it turns out that the distribution of primes is such that it's easier and more efficient to just randomly select a number and check to see if it's
        prime, and keep going until you find one, than it is to randomly generate primes in any programatic way. That's what this algorithm does.*/
    private int generatePrime()
    {
        //Limit the space to sufficiently big numbers
        int temp = random.Next(int.MaxValue / 2, int.MaxValue);

        while (!isPrime(temp)) { temp = random.Next(int.MaxValue / 2, int.MaxValue); }

        return temp;

    }

    //Primality test jacked from Wikipedia (thanks Wikipedia!)
    private bool isPrime(int n)
    {
        if(n <= 3)
        {
            return n > 1;
        }   
            
        if(n % 2 == 0 || n % 3 == 0)
        {
            return false;
        }

        //6k +/- 1, look at us go!
        for(int i = 5; i * i < n; n += 6)
        {
            if(n % i == 0 || n % i + 2 == 0)
            {
                return false;
            }
        }

        return true;
    }


    // From Stack overflow
    // This finds the greatest common divisor of two ulongs
    private static ulong GCD(ulong a, ulong b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a == 0 ? b : a;
    }

    // This figures out what the value raised to a large exponent is mod n
    public ulong powerModN(ulong baseVal, ulong exponent, ulong n)
    {
        ulong result = 1;

        for (ulong i = 1; i <= exponent; i++)
        {
            result *= baseVal;
            result %= n;
        }

        return result;
    }

}
}
