using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessagingApp
{
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

         public struct KeyPair
        {
            public ulong publicExponent;
            public ulong privateExponent;
            public ulong n;
        }

        //Generates a public and private key to be used in RSA
        public KeyPair generateKeyPair()
        {
            KeyPair keys = new KeyPair();
            
            //Generate two distinct primes
            ulong p = (ulong)generatePrime();
            ulong q = (ulong)generatePrime();
            while(p == q) { q = (ulong)generatePrime(); }

            //calculate n
            ulong n = p * q;

            keys.n = n;

            //Get the lambda (Carmichael's totient function?)
            ulong lambda = lcm(p - 1, q - 1);

            //Now we have to compute e, such that 1 < e < lambda and gcd(e, lambda) = 1
            ulong e = (ulong)random.Next(2, int.MaxValue);
            while(gcd(lambda, e) != 1) { e = (ulong)random.Next(2, int.MaxValue); }

            keys.publicExponent = e;

            //Calculate d
            ulong d = (n + 1) / e; //I think this works? Not sure gonna make Kris figure it out for sure
            //The other thing is this may be insecure, I'll look in to that.

            keys.privateExponent = d;
            
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
        
        //Get the greatest common divisor (I wrote this, there's probably a faster way to do it, I'll probably have Kris figure this out)
        private ulong gcd(ulong a, ulong b)
        {
            ulong m, n;
            if (a > b)
            {
                m = a; n = b;
            }
            else
            {
                m = b; n = a;
            }

            for (ulong i = n; i > 1; i--)
            {
                if(m % i == 0 && n % i == 0)
                {
                    return i;
                }
            }

            return 1;
        }
        
        //Get the lowest common multiple (shamelessly taken from Kory Gill on Stackoverflow (thanks Kory!))
        private ulong lcm(ulong a, ulong b)
        {
            ulong m, n;
            if( a > b)
            {
                m = a; n = b;
            }
            else
            {
                m = b; n = a;
            }

            //We can start at 2 instead of 1, since we know the primes will never be equivalent
            for(ulong i = 2; i < n; i++)
            {
                if((m * i) % n == 0)
                {
                    return i * m;
                }
            }

            return m * n;
        }
    }
}
