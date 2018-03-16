using global.cloudis.RealexHPP.sdk.domain;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global.cloudis.RealexHPP.sdk.extensions;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace global.cloudis.RealexHPP.sdk.utils
{
    public static class FormUtils
    {
        public static NameValueCollection HPPRequestToFormFields(HPPRequest req)
        {

            string[] attributes = {
                "Timestamp", "MerchantID", "Account", "OrderID", "Amount", "Currency", "Hash", "AutoSettleFlag", "Channel", "Comment1", "Comment2", "ShippingCode", "ShippingCountry", "BillingCode", "BillingCountry", "CustomerNumber",
                "VariableReference", "ProductID", "Language", "HPPVersion", "CardPaymentButtonText"
            };

            var fields = new NameValueCollection();

            foreach (string attribute in attributes)
            {
                fields.Add(req.GetAttributeFrom<JsonPropertyAttribute>(attribute).PropertyName, req.GetPropertyValue<string>(attribute));
            }

            return fields;
        }
    }
}
