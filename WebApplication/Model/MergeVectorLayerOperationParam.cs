using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Functions;

namespace WebApplication.Model
{
    public class MergeVectorLayerOperationParam : IValidatableObject
    {
        [Required]
        public Object InputLayers { get; set; }
        public string Crs { get; set; } = "None";
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var Validation = (IValidationFunctions)validationContext.GetService(typeof(IValidationFunctions));
            if (!Validation.IsValidGeoJsonArray(InputLayers.ToString()))
            {
                yield return new ValidationResult("Invalid GeoJSON in InputLayers", new[] { "InputLayer" });
            }
        }

    }
}
