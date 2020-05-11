using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Functions
{
    public interface IValidationFunctions
    {
        bool isDistanceValid(string output);
        bool isUsedOperatorValid(int output);
        bool isPredicateValid(List<int> output, int from, int to);
        bool IsValidGeoJson(string strInput);
        bool IsValidGeoJsonArray(string strInput);

        bool IsValidPolygonMultiPolygon(string strInput);
    }
}

