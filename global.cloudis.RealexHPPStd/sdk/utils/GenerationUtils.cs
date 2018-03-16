using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace global.cloudis.RealexHPP.sdk.utils
{
    public static class GenerationUtils
    {
        /// <summary>
        /// Each message sent to Realex should have a hash, attached. For a message using the remote 
        /// interface this is generated using the This is generated from the TIMESTAMP, MERCHANT_ID, 
        /// ORDER_ID, AMOUNT, and CURRENCY fields concatenated together with "." in between each field.  
        /// This confirms the message comes 
        /// from the client and 
        /// Generate a hash, required for all messages sent to IPS to prove it was not tampered with. 
        /// 
        /// Hashing takes a string as input, and produce a fixed size number (160 bits for SHA-1 which 
        /// this implementation uses). This number is a hash of the input, and a small change in the 
        /// input results in a substantial change in the output. The functions are thought to be secure 
        /// in the sense that it requires an enormous amount of computing power and time to find a string 
        /// that hashes to the same value. In others words, there's no way to decrypt a secure hash. 
        /// Given the larger key size, this implementation uses SHA-1 which we prefer that you, but Realex 
        /// has retained compatibilty with MD5 hashing for compatibility with older systems.
        /// 
        /// 
        /// To construct the hash for the remote interface follow this procedure: 
        /// 
        /// To construct the hash for the remote interface follow this procedure: 
        /// Form a string by concatenating the above fields with a period ('.') in the following order
        /// 
        /// (TIMESTAMP.MERCHANT_ID.ORDER_ID.AMOUNT.CURRENCY)
        /// 
        /// Like so (where a field is empty an empty string "" is used):
        /// 
        /// (20120926112654.thestore.ORD453-11.29900.EUR)
        /// 
        /// Get the hash of this string (SHA-1 shown below).
        /// 
        /// (b3d51ca21db725f9c7f13f8aca9e0e2ec2f32502)
        /// 
        /// Create a new string by concatenating this string and your shared secret using a period.
        /// 
        /// (b3d51ca21db725f9c7f13f8aca9e0e2ec2f32502.mysecret )
        /// 
        /// Get the hash of this value. This is the value that you send to Realex Payments.
        /// 
        /// (3c3cac74f2b783598b99af6e43246529346d95d1)
        ///
        /// This method takes the pre-built string of concatenated fields and the secret and returns the 
        /// SHA-1 hash to be placed in the request sent to Realex.
        /// 
        /// </summary>
        /// <param name="toHash"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static string GenerateHash(string toHash, string secret)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hashFirstPass = sha1.ComputeHash(Encoding.UTF8.GetBytes(toHash));
                var sbHashFirstPass = new StringBuilder((hashFirstPass.Length * 2));

                foreach (byte b in hashFirstPass)
                {
                    // can be "x2" if you want lowercase
                    sbHashFirstPass.Append(b.ToString("x2"));
                }

                var toHashSecondPass = sbHashFirstPass.Append(".").Append(secret).ToString();

                var hashSecondPass = sha1.ComputeHash(Encoding.UTF8.GetBytes(toHashSecondPass));
                var sbToHashSecondPass = new StringBuilder((hashSecondPass.Length * 2));

                foreach (byte b in hashSecondPass)
                {
                    // can be "x2" if you want lowercase
                    sbToHashSecondPass.Append(b.ToString("x2"));
                }

                return sbToHashSecondPass.ToString();
            }
        }
        

        public static string GenerateTimestamp()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public static string GenerateOrderId()
        {
            return new Guid().ToString();
        }
    }
}
