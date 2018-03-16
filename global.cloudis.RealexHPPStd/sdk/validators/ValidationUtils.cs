using global.cloudis.RealexHPP.sdk.domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace global.cloudis.RealexHPP.sdk.validators
{
    public static class ValidationUtils
    {
        public static void Validate(HPPRequest req)
        {
            ValidationContext context = new ValidationContext(req, null, null);
            var results = new List<ValidationResult>();
            var messages = new List<string>();

            // May need to expand this out to validate each field individually (see Unit Tests)
            bool isValid = Validator.TryValidateObject(req, context, results, false);
            if (!isValid)
            {
                foreach (ValidationResult result in results)
                {
                    messages.Add(result.ErrorMessage);
                }
                throw new RealexValidationException("HppRequest failed validation", messages);
            }
        }

        public static void Validate(HPPResponse resp, string secret)
        {
            if (!resp.IsHashValid(secret))
            {
                throw new RealexValidationException("HppResponse contains an invalid security hash");
            }
        }
    }
}
