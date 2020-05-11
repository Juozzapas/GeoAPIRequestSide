using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Functions;

namespace WebApplication.Model
{
    public class InersectionOperationParam : IValidatableObject
    {
        [Required]
        public Object InputLayer { get; set; }
        [Required]
        public Object OverlayLayer { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var Validation = (IValidationFunctions)validationContext.GetService(typeof(IValidationFunctions));

            if (!Validation.IsValidGeoJson(InputLayer.ToString()))
            {
                yield return new ValidationResult("Invalid GeoJSON in InputLayer", new[] { "InputLayer" });
            }
            if (!Validation.IsValidGeoJson(OverlayLayer.ToString()))
            {
                yield return new ValidationResult("Invalid GeoJSON in OverlayLayer", new[] { "OverlayLayer" });
            }

        }
    }
}
