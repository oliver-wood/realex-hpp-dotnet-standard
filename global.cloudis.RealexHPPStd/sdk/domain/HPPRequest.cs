using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using global.cloudis.RealexHPP.resources;
using global.cloudis.RealexHPP.sdk.utils;
using Newtonsoft.Json;

namespace global.cloudis.RealexHPP.sdk.domain
{
    public class HPPRequest : IHPP
    {

        #region Members
        /// <summary>
        /// Merchant ID supplied by Realex Payments
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_merchantId_size")]
        [RegularExpression(@"^[a-zA-Z0-9\.]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_merchantId_pattern")]
        [JsonProperty(propertyName: "MERCHANT_ID")]
        public string MerchantID { get; set; }

        /// <summary>
        /// The sub-account to use for this transaction. If not present, the default sub-account will be used.
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_account_size")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_account_pattern")]
        [JsonProperty(propertyName: "ACCOUNT")]
        public string Account { get; set; }

        /// <summary>
        /// The sub-account to use for this transaction. If not present, the default sub-account will be used.
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        [StringLength(maximumLength: 4, MinimumLength = 4, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_channel_size")]
        [RegularExpression(@"^(ECOM|MOTO)*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_channel_pattern")]
        [JsonProperty(propertyName: "CHANNEL")]
        public string Channel { get; set; } = "ECOM";


        /// <summary>
        /// A unique alphanumeric id that’s used to identify the transaction. No spaces are allowed.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_orderId_size")]
        [RegularExpression(@"[a-zA-Z0-9_\-]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_orderId_pattern")]
        [JsonProperty(propertyName: "ORDER_ID")]
        public string OrderID { get; set; }

        /// <summary>
        /// Total amount to authorise in the lowest unit of the currency – i.e. 100 euro would be entered as 10000. 
        /// If there is no decimal in the currency(e.g.JPY Yen) then contact Realex Payments.No decimal points are allowed. 
        /// Amount should be set to 0 for OTB transactions (i.e.where validate card only is set to 1).
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(maximumLength: 11, MinimumLength = 1, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_amount_size")]
        [RegularExpression(@"^[0-9]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_amount_pattern")]
        [JsonProperty(propertyName: "AMOUNT")]
        public string Amount { get; set; }

        /// <summary>
        /// A three-letter currency code (Eg. EUR, GBP). A list of currency codes can be provided by your account manager.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(maximumLength: 3, MinimumLength = 3, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_currency_size")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_currency_pattern")]
        [JsonProperty(propertyName: "CURRENCY")]
        public string Currency { get; set; }

        /// <summary>
        /// Date and time of the transaction. Entered in the following format: YYYYMMDDHHMMSS. Must be within 24 hours of the current time.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(maximumLength: 14, MinimumLength = 14, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_timestamp_size")]
        [RegularExpression(@"^[0-9]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_timestamp_pattern")]
        [JsonProperty(propertyName: "TIMESTAMP")]
        public string Timestamp { get; set; }

        /// <summary>
        /// A digital signature generated using the SHA-1 algorithm.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(maximumLength: 40, MinimumLength = 40, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_hash_size")]
        [RegularExpression(@"^[a-f0-9]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_hash_pattern")]
        [JsonProperty(propertyName: "SHA1HASH")]
        public string Hash { get; private set; }

        /// <summary>
        /// Used to signify whether or not you wish the transaction to be captured in the next batch. 
        /// If set to "1" and assuming the transaction is authorised then it will automatically be settled in the next batch.
        /// If set to "0" then the merchant must use the RealControl application to manually settle the transaction.
        /// This option can be used if a merchant wishes to delay the payment until after the goods have been shipped.
        /// Transactions can be settled for up to 115% of the original amount and must be settled within a certain period of time agreed with your issuing bank.
        /// </summary>
        [RegularExpression(@"(?i)^on*|^off$|^*$|^multi$|^1$|^0$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_autoSettleFlag_pattern")]
        [JsonProperty(propertyName: "AUTO_SETTLE_FLAG")]
        public string AutoSettleFlag { get; set; }

        /// <summary>
        /// A freeform comment to describe the transaction.
        /// </summary>
        [StringLength(maximumLength: 255, MinimumLength = 0, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_comment1_size")]
        [RegularExpression(@"^[\s \u0020-\u003B \u003D \u003F-\u007E \u00A1-\u00FF\u20AC\u201A\u0192\u201E\u2026\u2020\u2021\u02C6\u2030\u0160\u2039\u0152\u017D\u2018\u2019\u201C\u201D\u2022\u2013\u2014\u02DC\u2122\u0161\u203A\u0153\u017E\u0178]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_comment1_pattern")]
        [JsonProperty(propertyName: "COMMENT1")]
        public string Comment1 { get; set; }

        /// <summary>
        /// A freeform comment to describe the transaction.
        /// </summary>
        [StringLength(maximumLength: 255, MinimumLength = 0, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_comment2_size")]
        [RegularExpression(@"^[\s \u0020-\u003B \u003D \u003F-\u007E \u00A1-\u00FF\u20AC\u201A\u0192\u201E\u2026\u2020\u2021\u02C6\u2030\u0160\u2039\u0152\u017D\u2018\u2019\u201C\u201D\u2022\u2013\u2014\u02DC\u2122\u0161\u203A\u0153\u017E\u0178]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_comment2_pattern")]
        [JsonProperty(propertyName: "COMMENT2")]
        public string Comment2 { get; set; }

        /// <summary>
        /// Used to signify whether or not you want a Transaction Suitability Score for this transaction. 
        /// Can be "0" for no and "1" for yes.
        /// </summary>
        [StringLength(maximumLength: 1, MinimumLength = 0, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_returnTss_size")]
        [RegularExpression(@"^[01]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_returnTss_pattern")]
        [JsonProperty(propertyName: "RETURN_TSS")]
        public string ReturnTSS { get; set; }

        /// <summary>
        /// The postcode or ZIP of the shipping address.
        /// </summary>
        [StringLength(maximumLength: 30, MinimumLength = 0, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_shippingCode_size")]
        [RegularExpression(@"^[A-Za-z0-9\,\.\-\/\| ]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_shippingCode_pattern")]
        [JsonProperty(propertyName: "SHIPPING_CODE")]
        public string ShippingCode { get; set; }

        /// <summary>
        /// The country of the shipping address.
        /// </summary>
        [StringLength(maximumLength: 50, MinimumLength = 0, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_shippingCountry_size")]
        [RegularExpression(@"^[A-Za-z0-9\,\.\- ]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_shippingCountry_pattern")]
        [JsonProperty(propertyName: "SHIPPING_CO")]
        public string ShippingCountry { get; set; }

        /// <summary>
        /// The postcode or ZIP of the billing address.
        /// </summary>
        [StringLength(maximumLength: 30, MinimumLength = 0, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_billingCode_size")]
        [RegularExpression(@"^[A-Za-z0-9\,\.\-\/\| ]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_billingCode_pattern")]
        [JsonProperty(propertyName: "BILLING_CODE")]
        public string BillingCode { get; set; }

        /// <summary>
        /// The country of the billing address.
        /// </summary>
        [StringLength(maximumLength: 50, MinimumLength = 0, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_billingCountry_size")]
        [RegularExpression(@"^[A-Za-z0-9\,\.\- ]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_billingCountry_pattern")]
        [JsonProperty(propertyName: "BILLING_CO")]
        public string BillingCountry { get; set; }

        /// <summary>
        /// The customer number of the customer. You can send in any additional information about the transaction in this field,
        /// which will be visible under the transaction in the RealControl application.
        /// </summary>
        [StringLength(maximumLength: 50, MinimumLength = 0, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_customerNumber_size")]
        [RegularExpression(@"^[a-zA-Z0-9\._\-\,\+\@ \s]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_customerNumber_pattern")]
        [JsonProperty(propertyName: "CUST_NUM")]
        public string CustomerNumber { get; set; }

        /// <summary>
        /// A variable reference also associated with this customer. You can send in any additional information about the transaction in this field, 
        /// which will be visible under the transaction in the RealControl application.
        /// </summary>
        [StringLength(maximumLength: 50, MinimumLength = 0, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_variableReference_size")]
        [RegularExpression(@"^[a-zA-Z0-9\._\-\,\+\@ \s]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_variableReference_pattern")]
        [JsonProperty(propertyName: "VAR_REF")]
        public string VariableReference { get; set; }

        /// <summary>
        /// A product id associated with this product. You can send in any additional information about the transaction in this field, 
        /// which will be visible under the transaction in the RealControl application.
        /// </summary>
        [StringLength(maximumLength: 50, MinimumLength = 0, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_productId_size")]
        [RegularExpression(@"^[a-zA-Z0-9\._\-\,\+\@ \s]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_productId_pattern")]
        [JsonProperty(propertyName: "PROD_ID")]
        public string ProductID { get; set; }

        /// <summary>
        /// Used to set what language HPP is displayed in. Currently HPP is available in English, Spanish and German, with other languages to follow. 
        /// If the field is not sent in, the default language is the language that is set in your account configuration.This can be set by your account manager.
        /// </summary>
        [RegularExpression(@"^[a-zA-Z]{2}(_([a-zA-Z]{2}){1})?$|^$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_language_pattern")]
        [JsonProperty(propertyName: "HPP_LANG")]
        public string Language { get; set; }

        /// <summary>
        /// Used to set what text is displayed on the payment button for card transactions. If this field is not sent in, "Pay Now" is displayed on the button by default.
        /// </summary>
        [StringLength(maximumLength: 25, MinimumLength = 0, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_cardPaymentButtonText_size")]
        [RegularExpression(@"^[ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷ø¤ùúûüýþÿ\u0152\u017D\u0161\u0153\u017E\u0178¥a-zA-Z0-9\'\,\""\+\._\-\&\/\@\!\?\%\()\*\:\£\$\&\u20AC\#\[\]\|\=\\\u201C\u201D\u201C ]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_cardPaymentButtonText_pattern")]
        [JsonProperty(propertyName: "CARD_PAYMENT_BUTTON")]
        public string CardPaymentButtonText { get; set; }

        /// <summary>
        /// Enable card storage.
        /// </summary>
        [StringLength(maximumLength: 1, MinimumLength = 0, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_cardStorageEnable_size")]
        [RegularExpression(@"^[01]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_cardStorageEnable_pattern")]
        [JsonProperty(propertyName: "CARD_STORAGE_ENABLE")]
        public string CardStorageEnable { get; set; }

        /// <summary>
        /// Offer to save the card.
        /// </summary>
        [StringLength(maximumLength: 1, MinimumLength = 0, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_offerSaveCard_size")]
        [RegularExpression(@"^[01]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_offerSaveCard_pattern")]
        [JsonProperty(propertyName: "OFFER_SAVE_CARD")]
        public string OfferSaveCard { get; set; }

        /// <summary>
        /// The payer reference.
        /// </summary>
        [StringLength(maximumLength: 50, MinimumLength = 0, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_payerReference_size")]
        [RegularExpression(@"^[A-Za-z0-9_\-\\ ]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_payerReference_pattern")]
        [JsonProperty(propertyName: "PAYER_REF")]
        public string PayerReference { get; set; }

        /// <summary>
        /// The payment reference.
        /// </summary>
        [StringLength(maximumLength: 50, MinimumLength = 0, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_paymentReference_size")]
        [RegularExpression(@"^[A-Za-z0-9_\-]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_paymentReference_pattern")]
        [JsonProperty(propertyName: "PMT_REF")]
        public string PaymentReference { get; set; }

        /// <summary>
        /// Flag to indicate if the payer exists. 
        /// </summary>
        [StringLength(maximumLength: 1, MinimumLength = 0, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_payerExists_size")]
        [RegularExpression(@"^[102]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_payerExists_pattern")]
        [JsonProperty(propertyName: "PAYER_EXIST")]
        public string PayerExists { get; set; }

        /// <summary>
        /// Supplementary data to be sent to Realex Payments. This will be returned in the HPP response. 
        /// </summary>
        public Dictionary<string, string> SupplementaryData { get; set; }

        /// <summary>
        /// Used to identify an OTB transaction.
        /// </summary>
        [StringLength(maximumLength: 1, MinimumLength = 0, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_validateCardOnly_size")]
        [RegularExpression(@"^[01]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_validateCardOnly_pattern")]
        [JsonProperty(propertyName: "VALIDATE_CARD_ONLY")]
        public string ValidateCardOnly { get; set; }

        /// <summary>
        /// Transaction level configuration to enable/disable a DCC request. (Only if the merchant is configured).
        /// </summary>
        [StringLength(maximumLength: 1, MinimumLength = 0, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_dccEnable_size")]
        [RegularExpression(@"^[01]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_dccEnable_pattern")]
        [JsonProperty(propertyName: "DCC_ENABLE")]
        public string DCCEnable { get; set; }

        /// <summary>
        /// Override merchant configuration for fraud. (Only if the merchant is configured for fraud).
        /// </summary>(ACTIVE|PASSIVE|OFF)
        [StringLength(maximumLength: 7, MinimumLength = 0, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_hppFraudFilterMode_size")]
        [RegularExpression(@"^(ACTIVE|PASSIVE|OFF)*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_hppFraudFilterMode_pattern")]
        [JsonProperty(propertyName: "HPP_FRAUDFILTER_MODE")]
        public string HPPFraudFilterMode { get; set; }

        /// <summary>
        /// The HPP Version. To use HPP Card Management select HPP_VERSION = 2.
        /// </summary>
        [StringLength(maximumLength: 1, MinimumLength = 0, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_hppVersion_size")]
        [RegularExpression(@"^[1-2]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_hppVersion_pattern")]
        [JsonProperty(propertyName: "HPP_VERSION")]
        public string HPPVersion { get; set; }

        /// <summary>
        /// The payer reference. If this flag is received, HPP will retrieve a list of the payment methods saved for that payer.
        /// </summary>
        [StringLength(maximumLength: 50, MinimumLength = 0, ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_hppSelectStoredCard_size")]
        [RegularExpression(@"^[a-zA-Z0-9_\-\.\s]*$", ErrorMessageResourceType = typeof(HPPMessages), ErrorMessageResourceName = "hppRequest_hppSelectStoredCard_pattern")]
        [JsonProperty(propertyName: "HPP_SELECT_STORED_CARD")]
        public string HPPSelectStoredCard { get; set; }

        /// <summary>
        /// Indicates whether the Request is ready to send. If not, the Prepare() method should be called.
        /// </summary>
        [JsonIgnore()]
        public bool IsReady
        {
            get;
            private set;
        }

        [JsonIgnore()]
        public bool IsEncoded
        {
            get; set;
        }
        #endregion

        /// <summary>
        /// Get the request ready to send
        /// </summary>
        /// <param name="secret"></param>
        public void Prepare(string secret)
        {
            if (string.IsNullOrEmpty(Timestamp))
            {
                Timestamp = GenerationUtils.GenerateTimestamp();
            }
            if (string.IsNullOrEmpty(OrderID))
            {
                OrderID = GenerationUtils.GenerateOrderId();
            }
            InitHash(secret);
            IsReady = true;
        }

        /// <summary>
        /// Encodes all the string Member properties as Base64 strings
        /// </summary>
        public void Encode()
        {
            if (IsEncoded) return;

            if (!string.IsNullOrEmpty(MerchantID))
            {
                MerchantID = EncodingUtils.Base64Encode(MerchantID);
            }
            if (!string.IsNullOrEmpty(Account))
            {
                Account = EncodingUtils.Base64Encode(Account);
            }
            if (!string.IsNullOrEmpty(Channel))
            {
                Channel = EncodingUtils.Base64Encode(Channel);
            }
            if (!string.IsNullOrEmpty(OrderID))
            {
                OrderID = EncodingUtils.Base64Encode(OrderID);
            }
            if (!string.IsNullOrEmpty(Amount))
            {
                Amount = EncodingUtils.Base64Encode(Amount);
            }
            if (!string.IsNullOrEmpty(Currency))
            {
                Currency = EncodingUtils.Base64Encode(Currency);
            }
            if (!string.IsNullOrEmpty(Timestamp))
            {
                Timestamp = EncodingUtils.Base64Encode(Timestamp);
            }
            if (!string.IsNullOrEmpty(Hash))
            {
                Hash = EncodingUtils.Base64Encode(Hash);
            }
            if (!string.IsNullOrEmpty(AutoSettleFlag))
            {
                AutoSettleFlag = EncodingUtils.Base64Encode(AutoSettleFlag);
            }
            if (!string.IsNullOrEmpty(Comment1))
            {
                Comment1 = EncodingUtils.Base64Encode(Comment1);
            }
            if (!string.IsNullOrEmpty(Comment2))
            {
                Comment2 = EncodingUtils.Base64Encode(Comment2);
            }
            if (!string.IsNullOrEmpty(ReturnTSS))
            {
                ReturnTSS = EncodingUtils.Base64Encode(ReturnTSS);
            }
            if (!string.IsNullOrEmpty(ShippingCode))
            {
                ShippingCode = EncodingUtils.Base64Encode(ShippingCode);
            }
            if (!string.IsNullOrEmpty(ShippingCountry))
            {
                ShippingCountry = EncodingUtils.Base64Encode(ShippingCountry);
            }
            if (!string.IsNullOrEmpty(BillingCode))
            {
                BillingCode = EncodingUtils.Base64Encode(BillingCode);
            }
            if (!string.IsNullOrEmpty(BillingCountry))
            {
                BillingCountry = EncodingUtils.Base64Encode(BillingCountry);
            }
            if (!string.IsNullOrEmpty(CardPaymentButtonText))
            {
                CardPaymentButtonText = EncodingUtils.Base64Encode(CardPaymentButtonText);
            }
            if (!string.IsNullOrEmpty(CustomerNumber))
            {
                CustomerNumber = EncodingUtils.Base64Encode(CustomerNumber);
            }
            if (!string.IsNullOrEmpty(VariableReference))
            {
                VariableReference = EncodingUtils.Base64Encode(VariableReference);
            }
            if (!string.IsNullOrEmpty(ProductID))
            {
                ProductID = EncodingUtils.Base64Encode(ProductID);
            }
            if (!string.IsNullOrEmpty(Language))
            {
                Language = EncodingUtils.Base64Encode(Language);
            }
            if (!string.IsNullOrEmpty(CardStorageEnable))
            {
                CardStorageEnable = EncodingUtils.Base64Encode(CardStorageEnable);
            }
            if (!string.IsNullOrEmpty(OfferSaveCard))
            {
                OfferSaveCard = EncodingUtils.Base64Encode(OfferSaveCard);
            }
            if (!string.IsNullOrEmpty(PayerReference))
            {
                PayerReference = EncodingUtils.Base64Encode(PayerReference);
            }
            if (!string.IsNullOrEmpty(PaymentReference))
            {
                PaymentReference = EncodingUtils.Base64Encode(PaymentReference);
            }
            if (!string.IsNullOrEmpty(PayerExists))
            {
                PayerExists = EncodingUtils.Base64Encode(PayerExists);
            }
            if (!string.IsNullOrEmpty(DCCEnable))
            {
                DCCEnable = EncodingUtils.Base64Encode(DCCEnable);
            }
            if (!string.IsNullOrEmpty(HPPFraudFilterMode))
            {
                HPPFraudFilterMode = EncodingUtils.Base64Encode(HPPFraudFilterMode);
            }
            if (!string.IsNullOrEmpty(HPPVersion))
            {
                HPPVersion = EncodingUtils.Base64Encode(HPPVersion);
            }
            if (!string.IsNullOrEmpty(HPPSelectStoredCard))
            {
                HPPSelectStoredCard = EncodingUtils.Base64Encode(HPPSelectStoredCard);
            }

            if (SupplementaryData != null && SupplementaryData.Keys.Count > 0)
            {
                var encoded = SupplementaryData.Select(kpv => new KeyValuePair<string, string>(kpv.Key, EncodingUtils.Base64Encode(kpv.Value))).ToList<KeyValuePair<string, string>>();
                SupplementaryData.Clear();
                foreach (KeyValuePair<string, string> kpv in encoded)
                {
                    SupplementaryData.Add(kpv.Key, kpv.Value);
                }
            }
            

            IsEncoded = true;
        }

        /// <summary>
        /// Decodes all the string Member properties from Base64 strings
        /// </summary>
        public void Decode()
        {
            if (!IsEncoded) return;

            if (!string.IsNullOrEmpty(MerchantID))
            {
                MerchantID = EncodingUtils.Base64Decode(MerchantID);
            }
            if (!string.IsNullOrEmpty(Account))
            {
                Account = EncodingUtils.Base64Decode(Account);
            }
            if (!string.IsNullOrEmpty(Channel))
            {
                Channel = EncodingUtils.Base64Decode(Channel);
            }
            if (!string.IsNullOrEmpty(OrderID))
            {
                OrderID = EncodingUtils.Base64Decode(OrderID);
            }
            if (!string.IsNullOrEmpty(Amount))
            {
                Amount = EncodingUtils.Base64Decode(Amount);
            }
            if (!string.IsNullOrEmpty(Currency))
            {
                Currency = EncodingUtils.Base64Decode(Currency);
            }
            if (!string.IsNullOrEmpty(Timestamp))
            {
                Timestamp = EncodingUtils.Base64Decode(Timestamp);
            }
            if (!string.IsNullOrEmpty(Hash))
            {
                Hash = EncodingUtils.Base64Decode(Hash);
            }
            if (!string.IsNullOrEmpty(AutoSettleFlag))
            {
                AutoSettleFlag = EncodingUtils.Base64Decode(AutoSettleFlag);
            }
            if (!string.IsNullOrEmpty(Comment1))
            {
                Comment1 = EncodingUtils.Base64Decode(Comment1);
            }
            if (!string.IsNullOrEmpty(Comment2))
            {
                Comment2 = EncodingUtils.Base64Decode(Comment2);
            }
            if (!string.IsNullOrEmpty(ReturnTSS))
            {
                ReturnTSS = EncodingUtils.Base64Decode(ReturnTSS);
            }
            if (!string.IsNullOrEmpty(ShippingCode))
            {
                ShippingCode = EncodingUtils.Base64Decode(ShippingCode);
            }
            if (!string.IsNullOrEmpty(ShippingCountry))
            {
                ShippingCountry = EncodingUtils.Base64Decode(ShippingCountry);
            }
            if (!string.IsNullOrEmpty(BillingCode))
            {
                BillingCode = EncodingUtils.Base64Decode(BillingCode);
            }
            if (!string.IsNullOrEmpty(BillingCountry))
            {
                BillingCountry = EncodingUtils.Base64Decode(BillingCountry);
            }
            if (!string.IsNullOrEmpty(CardPaymentButtonText))
            {
                CardPaymentButtonText = EncodingUtils.Base64Decode(CardPaymentButtonText);
            }
            if (!string.IsNullOrEmpty(CustomerNumber))
            {
                CustomerNumber = EncodingUtils.Base64Decode(CustomerNumber);
            }
            if (!string.IsNullOrEmpty(VariableReference))
            {
                VariableReference = EncodingUtils.Base64Decode(VariableReference);
            }
            if (!string.IsNullOrEmpty(ProductID))
            {
                ProductID = EncodingUtils.Base64Decode(ProductID);
            }
            if (!string.IsNullOrEmpty(Language))
            {
                Language = EncodingUtils.Base64Decode(Language);
            }
            if (!string.IsNullOrEmpty(CardStorageEnable))
            {
                CardStorageEnable = EncodingUtils.Base64Decode(CardStorageEnable);
            }
            if (!string.IsNullOrEmpty(OfferSaveCard))
            {
                OfferSaveCard = EncodingUtils.Base64Decode(OfferSaveCard);
            }
            if (!string.IsNullOrEmpty(PayerReference))
            {
                PayerReference = EncodingUtils.Base64Decode(PayerReference);
            }
            if (!string.IsNullOrEmpty(PaymentReference))
            {
                PaymentReference = EncodingUtils.Base64Decode(PaymentReference);
            }
            if (!string.IsNullOrEmpty(PayerExists))
            {
                PayerExists = EncodingUtils.Base64Decode(PayerExists);
            }
            if (!string.IsNullOrEmpty(DCCEnable))
            {
                DCCEnable = EncodingUtils.Base64Decode(DCCEnable);
            }
            if (!string.IsNullOrEmpty(HPPFraudFilterMode))
            {
                HPPFraudFilterMode = EncodingUtils.Base64Decode(HPPFraudFilterMode);
            }
            if (!string.IsNullOrEmpty(HPPVersion))
            {
                HPPVersion = EncodingUtils.Base64Decode(HPPVersion);
            }
            if (!string.IsNullOrEmpty(HPPSelectStoredCard))
            {
                HPPSelectStoredCard = EncodingUtils.Base64Decode(HPPSelectStoredCard);
            }

            if (SupplementaryData != null && SupplementaryData.Keys.Count > 0)
            {
                var decoded = SupplementaryData.Select(kpv => new KeyValuePair<string, string>(kpv.Key, EncodingUtils.Base64Decode(kpv.Value))).ToList<KeyValuePair<string, string>>();
                SupplementaryData.Clear();
                foreach (KeyValuePair<string, string> kpv in decoded)
                {
                    SupplementaryData.Add(kpv.Key, kpv.Value);
                }
            }


            IsEncoded = false;
        }

        /// <summary>
        /// Explicitly initialise the Request Hash using the secret
        /// </summary>
        /// <param name="secret"></param>
        private void InitHash(string secret)
        {
            // Override payerRef with hppSelectStoredCard if present.
            if (!string.IsNullOrEmpty(HPPSelectStoredCard))
            {
                PayerReference = HPPSelectStoredCard;
            }
            
            // Create String to hash. 
            var hashFields = new List<string>();

            // Check for card storage enable flag to determine if Real Vault transaction 
            if (!string.IsNullOrEmpty(CardStorageEnable) && CardStorageEnable.Equals(HPPConstants.Flag.TRUE.ToString()) ||
                !string.IsNullOrEmpty(HPPSelectStoredCard))
            {
                hashFields.AddRange(new string[]
                {
                    Timestamp, MerchantID, OrderID, Amount, Currency, PayerReference, PaymentReference
                });                
            }
            else
            {
                hashFields.AddRange(new string[]
                {
                    Timestamp, MerchantID, OrderID, Amount, Currency
                });
            }

            // Add fraud filter mode if it has been set
            if (!string.IsNullOrEmpty(HPPFraudFilterMode))
            {
                hashFields.Add(HPPFraudFilterMode);
            }

            // Join the fields with . separator and hash it
            var toHash = string.Join(".", hashFields);
            Hash = GenerationUtils.GenerateHash(toHash, secret);
        }

    }
}
