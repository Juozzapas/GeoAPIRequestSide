using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Functions;

namespace WebApplication.Model
{
    public class SelectByLocationOperationParam : IValidatableObject
    {

        [Required]
        public Object InputLayer { get; set; }
        [Required]
        public Object OverlayLayer { get; set; }

        public List<int> predicate { get; set; } = new List<int> { 0 };

        public Object Distance { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var Validation = (IValidationFunctions)validationContext.GetService(typeof(IValidationFunctions));

            if (!Validation.IsValidGeoJson(InputLayer.ToString()))
            {
                yield return new ValidationResult("Invalid geojson in InputLayer", new[] { "InputLayer" });
            }
            if (!Validation.IsValidGeoJson(OverlayLayer.ToString()))
            {
                yield return new ValidationResult("Invalid geojson in OverlayLayer", new[] { "OverlayLayer" });
            }
            if (!Validation.isPredicateValid(predicate, 0, 7))
            {
                yield return new ValidationResult("Single value in predicate array must be between 0 and 7", new[] { "Predicate" });

            }
            if (Distance != null && !Validation.isDistanceValid(Distance.ToString()))
            {
                yield return new ValidationResult("Distance field must be int or float", new[] { "Distance" });
            }
        }
    }

}
