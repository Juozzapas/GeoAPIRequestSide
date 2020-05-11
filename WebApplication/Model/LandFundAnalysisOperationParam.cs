using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Functions;

namespace WebApplication.Model
{
    public class LandFundAnalysisOperationParam : IValidatableObject
    {
        [Required]
        public Object InputLayer { get; set; }
        [Required]
        public List<int> Predicate { get; set; }
        public object Distance { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var Validation = (IValidationFunctions)validationContext.GetService(typeof(IValidationFunctions));
            if (!Validation.IsValidGeoJson(InputLayer.ToString()))
            {
                yield return new ValidationResult("Invalid GeoJSON in InputLayer", new[] { "InputLayer" });
            }
            if (!Validation.isPredicateValid(Predicate, 0, 2))
            {

                yield return new ValidationResult("Single value in Predicate list must be between 0 and 2", new[] { "Predicate" });
            }
            if (Distance != null && !Validation.isDistanceValid(Distance.ToString()))
            {
                yield return new ValidationResult("Distance field must be int or float", new[] { "Distance" });
            }
        }
    }
}
