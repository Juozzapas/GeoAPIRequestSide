using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Functions;
using WebApplication.HandleThirdPartyOutput;

namespace WebApplication.Model
{
    public class BufferOperationParam : IValidatableObject
    {
        [Required]
        public Object InputLayer { get; set; }
        [Required]
        public Object Distance { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var Validation = (IValidationFunctions)validationContext.GetService(typeof(IValidationFunctions));

            if (!Validation.IsValidGeoJson(InputLayer.ToString()))
            {
                yield return new ValidationResult("Invalid GeoJSON in InputLayer", new[] { "InputLayer" });
            }
            if (!Validation.isDistanceValid(Distance.ToString()))
            {
                yield return new ValidationResult("Distance field must be int or float", new[] { "Distance" });
            }
        }

    }
}
