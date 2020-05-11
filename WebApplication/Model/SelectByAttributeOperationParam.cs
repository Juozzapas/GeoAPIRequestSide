using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Functions;

namespace WebApplication.Model
{
    public class SelectByAttributeOperationParam : IValidatableObject
    {
        [Required]
        public Object InputLayer { get; set; }
        [Required]
        public string Field { get; set; }
        public int Operator { get; set; }

        public string Value { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var Validation = (IValidationFunctions)validationContext.GetService(typeof(IValidationFunctions));
            if (!Validation.IsValidGeoJson(InputLayer.ToString()))
            {
                yield return new ValidationResult("Invalid GeoJSON in InputLayer", new[] { "InputLayer" });
            }
            if (!Validation.isUsedOperatorValid(Operator))
            {
                yield return new ValidationResult("Operator value must be between 0 and 10", new[] { "Operator" });
            }
        }
    }
}