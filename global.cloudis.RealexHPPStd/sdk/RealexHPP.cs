using global.cloudis.RealexHPP.sdk.domain;
using global.cloudis.RealexHPP.sdk.validators;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace global.cloudis.RealexHPP.sdk
{
    public class RealexHPP
    {
        public string Secret
        {
            get; set;
        }

        
        public string RequestToJson(HPPRequest req)
        {
            // Initialise the hash
            req.Prepare(Secret);

            // Validate, Will throw an error if not valid
            ValidationUtils.Validate(req);

            // Encode
            try
            {
                req.Encode();
            }
            catch (Exception e)
            {
                throw new RealexException("Exception encoding HPP request.", e);
            }

            // Convert to JSON
            var json = JsonConvert.SerializeObject(req, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return json;
        }


        public HPPRequest RequestFromJson(string json)
        {
            return RequestFromJson(json, true);
        }

        public HPPRequest RequestFromJson(string json, bool encoded)
        {
            var req = JsonConvert.DeserializeObject<HPPRequest>(json);

            // Decodeif necessary
            req.IsEncoded = encoded;
            try
            {
                req.Decode();
            }
            catch (Exception e)
            {
                throw new RealexException("Exception decoding HPP request.", e);
            }

            // Check if valid
            ValidationUtils.Validate(req);

            return req;
        }

        public HPPResponse ResponseFromJson(string json)
        {
            return ResponseFromJson(json, true);
        }

        public HPPResponse ResponseFromJson(string json, bool encoded)
        {
            var resp = JsonConvert.DeserializeObject<HPPResponse>(json);

            // Decodeif necessary
            resp.IsEncoded = encoded;
            try
            {
                resp.Decode();
            }
            catch (Exception e)
            {
                throw new RealexException("Exception decoding HPP response.", e);
            }

            // Check if valid
            ValidationUtils.Validate(resp, Secret);

            return resp;
        }
    }
}
