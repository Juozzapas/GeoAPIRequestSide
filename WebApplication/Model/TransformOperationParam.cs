using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication.Functions;

namespace WebApplication.Model
{
    public class TransformOperationParam : IValidatableObject
    {
        private readonly Dictionary<string, string> formats = new Dictionary<string, string>(
            StringComparer.OrdinalIgnoreCase)
        {
            {"ESRI Shapefile",".shp"},
            {"CSV", ".csv"},
            {"GML", ".gml"},
            {"KML", ".kml"},
            {"GeoJSON", ".geojson"},
        };
        [Required]
        public Object InputLayer { get; set; }

        public string SingleFileFormat { get; set; }
        [Required]
        public string Type { get; set; } // field
       
        public string SourceCrs { get; set; }

        public string TargetCrs { get; set; }

        private string skipFailures; // field

        public string SkipFailures   // property
        {
            get { return skipFailures; }   // get method
            set
            {
                skipFailures = "-skipfailures";
            }
        }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var Validation = (IValidationFunctions)validationContext.GetService(typeof(IValidationFunctions));
            if (!Validation.IsValidGeoJson(InputLayer.ToString()))
            {
                yield return new ValidationResult("Invalid geojson in InputLayer", new[] { "InputLayer" });
            }
            if (string.IsNullOrEmpty(TargetCrs) && !string.IsNullOrEmpty(SourceCrs))
                yield return new ValidationResult("If sourceCrs is specified targetCrs must be specified", new[] { "TargetCrs", "SourceCrs" });

            if (!formats.ContainsKey(Type))
                yield return new ValidationResult("Wrong format. Supported drivers :"+ string.Join(", ", formats.Keys), new[] { "Type" });
            else
            {
                SingleFileFormat = formats[Type];
            }


        }

    }
}
