using global.cloudis.RealexHPP.sdk.utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace global.cloudis.RealexHPP.sdk.domain
{
    [JsonConverter(typeof(HPPResponseConverter))]
    public class HPPResponse : IHPP
    {
        #region Members
        /// <summary>
        /// This is the merchant id that Realex Payments assign to you.
        /// </summary>
        [JsonProperty(propertyName: "MERCHANT_ID")]
        public string MerchantID { get; private set; }

        /// <summary>
        /// The sub-account used in the transaction.
        /// </summary>
        [JsonProperty(propertyName: "ACCOUNT")]
        public string Account { get; private set; }

        /// <summary>
        /// The unique order id that you sent to us.
        /// </summary>
        [JsonProperty(propertyName: "ORDER_ID")]
        public string OrderID { get; private set; }

        /// <summary>
        /// The amount that was authorised. Returned in the lowest unit of the currency.
        /// </summary>
        [JsonProperty(propertyName: "AMOUNT")]
        public string Amount { get; private set; }

        /// <summary>
        /// Will contain a valid authcode if the transaction was successful. Will be empty otherwise.
        /// </summary>
        [JsonProperty(propertyName: "AUTHCODE")]
        public string AuthCode { get; private set; }

        /// <summary>
        /// The date and time of the transaction.
        /// </summary>
        [JsonProperty(propertyName: "TIMESTAMP")]
        public string Timestamp { get; private set; }

        /// <summary>
        /// A SHA-1 digital signature created using the HPP response fields and your shared secret.
        /// </summary>
        [JsonProperty(propertyName: "SHA1HASH")]
        public string Hash { get; private set; }

        /// <summary>
        /// The outcome of the transaction. Will contain "00" if the transaction was a success or another value (depending on the error) if not.
        /// </summary>
        [JsonProperty(propertyName: "RESULT")]
        public string Result { get; private set; }

        /// <summary>
        /// Will contain a text message that describes the result code.
        /// </summary>
        [JsonProperty(propertyName: "MESSAGE")]
        public string Message { get; private set; }

        /// <summary>
        /// <p>
        /// The result of the Card Verification check (if enabled):
        /// <ul>
        /// <li>M: CVV Matched.</li>
        /// <li>N: CVV Not Matched.</li>
        /// <li>I: CVV Not checked due to circumstances.</li>
        /// <li>U: CVV Not checked - issuer not certified.</li>
        /// <li>P: CVV Not Processed.</li>
        /// </ul>
        /// </p>
        /// 
        /// </summary>
        [JsonProperty(propertyName: "CVNRESULT")]
        public string CVNResult { get; private set; }

        /// <summary>
        /// A unique reference that Realex Payments assign to your transaction.
        /// </summary>
        [JsonProperty(propertyName: "PASREF")]
        public string PASRef { get; private set; }

        /// <summary>
        /// This is the Realex Payments batch that this transaction will be in. 
        /// (This is equal to "-1" if the transaction was sent in with the autosettle flag off. 
        /// After you settle it (either manually or programmatically) the response to that transaction will contain the batch id.)
        /// </summary>
        [JsonProperty(propertyName: "BATCHID")]
        public string BatchID { get; private set; }

        /// <summary>
        /// This is the ecommerce indicator (this will only be returned for 3DSecure transactions).
        /// </summary>
        [JsonProperty(propertyName: "ECI")]
        public string ECI { get; private set; }

        /// <summary>
        /// Cardholder Authentication Verification Value (this will only be returned for 3DSecure transactions).
        /// </summary>
        [JsonProperty(propertyName: "CAVV")]
        public string CAVV { get; private set; }

        /// <summary>
        /// Exchange Identifier (this will only be returned for 3DSecure transactions).
        /// </summary>
        [JsonProperty(propertyName: "XID")]
        public string XID { get; private set; }

        /// <summary>
        /// Whatever data you have sent in the request will be returned to you.
        /// </summary>
        [JsonProperty(propertyName: "COMMENT1")]
        public string Comment1 { get; private set; }

        /// <summary>
        /// Whatever data you have sent in the request will be returned to you.
        /// </summary>
        [JsonProperty(propertyName: "COMMENT2")]
        public string Comment2 { get; private set; }

        /// <summary>
        /// The Transaction Suitability Score for the transaction. The RealScore is comprised of various distinct tests. 
        /// Using the RealControl application you can request that Realex Payments return certain individual scores to you. 
        /// These are identified by numbers - thus TSS_1032 would be the result of the check with id 1032. 
        /// You can then use these specific checks in conjunction with RealScore score to ascertain whether or not you wish to continue with the settlement.
        /// </summary>
        [JsonProperty(propertyName: "TSS")]
        public Dictionary<string, string> TSS;

        /// <summary>
        /// Anything else you sent to us in the request will be returned to you in supplementary data.
        /// </summary>
        public Dictionary<string, string> SupplementaryData = new Dictionary<string, string>();


        [JsonIgnore()]
        public bool IsEncoded
        {
            get; set;
        }

        public bool IsHashValid(string secret)
        {
            return Hash.Equals(GenerateHash(secret));
        }
        #endregion



        private string GenerateHash(string secret)
        {
            var hashFields = new List<string>();
            hashFields.AddRange(new string[]
            {
                Timestamp, MerchantID, OrderID, Result, Message, PASRef, AuthCode
            });

            // Join the fields with . separator and hash it
            var toHash = string.Join(".", hashFields);
            return GenerationUtils.GenerateHash(toHash, secret);
        }



        public void Encode()
        {
            if (!string.IsNullOrEmpty(MerchantID))
            {
                MerchantID = EncodingUtils.Base64Encode(MerchantID);
            }
            if (!string.IsNullOrEmpty(Account))
            {
                Account = EncodingUtils.Base64Encode(Account);
            }
            if (!string.IsNullOrEmpty(Amount))
            {
                Amount = EncodingUtils.Base64Encode(Amount);
            }
            if (!string.IsNullOrEmpty(AuthCode))
            {
                AuthCode = EncodingUtils.Base64Encode(AuthCode);
            }
            if (!string.IsNullOrEmpty(BatchID))
            {
                BatchID = EncodingUtils.Base64Encode(BatchID);
            }
            if (!string.IsNullOrEmpty(CAVV))
            {
                CAVV = EncodingUtils.Base64Encode(CAVV);
            }
            if (!string.IsNullOrEmpty(CVNResult))
            {
                CVNResult = EncodingUtils.Base64Encode(CVNResult);
            }
            if (!string.IsNullOrEmpty(ECI))
            {
                ECI = EncodingUtils.Base64Encode(ECI);
            }
            if (!string.IsNullOrEmpty(Comment1))
            {
                Comment1 = EncodingUtils.Base64Encode(Comment1);
            }
            if (!string.IsNullOrEmpty(Comment2))
            {
                Comment2 = EncodingUtils.Base64Encode(Comment2);
            }
            if (!string.IsNullOrEmpty(Message))
            {
                Message = EncodingUtils.Base64Encode(Message);
            }
            if (!string.IsNullOrEmpty(PASRef))
            {
                PASRef = EncodingUtils.Base64Encode(PASRef);
            }
            if (!string.IsNullOrEmpty(Hash))
            {
                Hash = EncodingUtils.Base64Encode(Hash);
            }
            if (!string.IsNullOrEmpty(Result))
            {
                Result = EncodingUtils.Base64Encode(Result);
            }
            if (!string.IsNullOrEmpty(XID))
            {
                XID = EncodingUtils.Base64Encode(XID);
            }
            if (!string.IsNullOrEmpty(OrderID))
            {
                OrderID = EncodingUtils.Base64Encode(OrderID);
            }
            if (!string.IsNullOrEmpty(Timestamp))
            {
                Timestamp = EncodingUtils.Base64Encode(Timestamp);
            }


            if (TSS != null && TSS.Keys.Count > 0)
            {
                foreach (string k in TSS.Keys)
                {
                    TSS[k] = EncodingUtils.Base64Encode(TSS[k]);
                }
            }

            if (SupplementaryData != null && SupplementaryData.Keys.Count > 0)
            {
                foreach (string k in SupplementaryData.Keys)
                {
                    SupplementaryData[k] = EncodingUtils.Base64Encode(SupplementaryData[k]);
                }
            }
            IsEncoded = true;
        }

        public void Decode()
        {

            if (!string.IsNullOrEmpty(MerchantID))
            {
                MerchantID = EncodingUtils.Base64Decode(MerchantID);
            }
            if (!string.IsNullOrEmpty(Account))
            {
                Account = EncodingUtils.Base64Decode(Account);
            }
            if (!string.IsNullOrEmpty(Amount))
            {
                Amount = EncodingUtils.Base64Decode(Amount);
            }
            if (!string.IsNullOrEmpty(AuthCode))
            {
                AuthCode = EncodingUtils.Base64Decode(AuthCode);
            }
            if (!string.IsNullOrEmpty(BatchID))
            {
                BatchID = EncodingUtils.Base64Decode(BatchID);
            }
            if (!string.IsNullOrEmpty(CAVV))
            {
                CAVV = EncodingUtils.Base64Decode(CAVV);
            }
            if (!string.IsNullOrEmpty(CVNResult))
            {
                CVNResult = EncodingUtils.Base64Decode(CVNResult);
            }
            if (!string.IsNullOrEmpty(ECI))
            {
                ECI = EncodingUtils.Base64Decode(ECI);
            }
            if (!string.IsNullOrEmpty(Comment1))
            {
                Comment1 = EncodingUtils.Base64Decode(Comment1);
            }
            if (!string.IsNullOrEmpty(Comment2))
            {
                Comment2 = EncodingUtils.Base64Decode(Comment2);
            }
            if (!string.IsNullOrEmpty(Message))
            {
                Message = EncodingUtils.Base64Decode(Message);
            }
            if (!string.IsNullOrEmpty(PASRef))
            {
                PASRef = EncodingUtils.Base64Decode(PASRef);
            }
            if (!string.IsNullOrEmpty(Hash))
            {
                Hash = EncodingUtils.Base64Decode(Hash);
            }
            if (!string.IsNullOrEmpty(Result))
            {
                Result = EncodingUtils.Base64Decode(Result);
            }
            if (!string.IsNullOrEmpty(XID))
            {
                XID = EncodingUtils.Base64Decode(XID);
            }
            if (!string.IsNullOrEmpty(OrderID))
            {
                OrderID = EncodingUtils.Base64Decode(OrderID);
            }
            if (!string.IsNullOrEmpty(Timestamp))
            {
                Timestamp = EncodingUtils.Base64Decode(Timestamp);
            }


            if (TSS != null && TSS.Keys.Count > 0)
            {
                var newTSS = TSS.Select(kpv => new KeyValuePair<string, string>(kpv.Key, EncodingUtils.Base64Decode(kpv.Value))).ToList<KeyValuePair<string, string>>();
                TSS.Clear();
                foreach (KeyValuePair<string, string> kpv in newTSS)
                {
                    TSS.Add(kpv.Key, kpv.Value);
                }
            }

            if (SupplementaryData != null && SupplementaryData.Keys.Count > 0)
            {
                var newTSS = SupplementaryData.Select(kpv => new KeyValuePair<string, string>(kpv.Key, EncodingUtils.Base64Decode(kpv.Value))).ToList<KeyValuePair<string, string>>();
                SupplementaryData.Clear();
                foreach (KeyValuePair<string, string> kpv in newTSS)
                {
                    SupplementaryData.Add(kpv.Key, kpv.Value);
                }
            }


            IsEncoded = false;
        }
    }
}
