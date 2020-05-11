using BAMCIS.GeoJSON;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Functions
{
    public class ValidationFunctions : IValidationFunctions
    {
        public bool isUsedOperatorValid(int output)
        {
            if (output >= 0 && output <= 10)
            {
                return true;
            }
            return false;
        }
        public bool isDistanceValid(string distance)
        {
            float number;
            bool result = float.TryParse(distance, out number);
            if (result)
            {
                return true;
            }
            return false;
        }

        public bool IsValidGeoJson(string strInput)
        {
            strInput = strInput.Trim();
            if (!strInput.StartsWith("{"))
            {
                return false;
            }

            if (!strInput.EndsWith("}"))
            {
                return false;
            }

            try
            {
                GeoJson data = JsonConvert.DeserializeObject<GeoJson>(strInput);
                return true;
            }
            catch (JsonSerializationException jex)
            {
                //Exception in parsing json
                Console.WriteLine(jex.Message);
                return false;
            }
            catch (Exception ex) //some other exception
            {
                Console.WriteLine(ex.ToString());
                return false;
            }


        }
        public bool IsValidGeoJsonArray(string strInput)
        {
            strInput = strInput.Trim();
            if (!strInput.StartsWith("["))
            {
                return false;
            }

            if (!strInput.EndsWith("]"))
            {
                return false;
            }

            try
            {
                dynamic array = JsonConvert.DeserializeObject(strInput);

                foreach (var s in array)
                {
                    var temp = JsonConvert.SerializeObject(s);
                    GeoJson objt = JsonConvert.DeserializeObject<GeoJson>(temp);
                }
                return true;
            }
            catch (JsonSerializationException jex)
            {
                //Exception in parsing json
                Console.WriteLine(jex.Message);
                return false;
            }
            catch (Exception ex) //some other exception
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        // may be used in the future
        public bool IsValidPolygonMultiPolygon(string strInput)
        {
            strInput = strInput.Trim();
            if (strInput.StartsWith("{") && strInput.EndsWith("}")) //For array
            {

                try
                {
                    GeoJson data = JsonConvert.DeserializeObject<GeoJson>(strInput);
                    switch (data.Type)
                    {
                        case GeoJsonType.FeatureCollection:
                            {
                                FeatureCollection feature = (FeatureCollection)data;
                                foreach (var VARIABLE in feature.Features)
                                {
                                    if (VARIABLE.Geometry.Type != GeoJsonType.Polygon && VARIABLE.Geometry.Type != GeoJsonType.MultiPolygon)
                                    {
                                        return false;
                                    }
                                }
                                break;
                            }
                        case GeoJsonType.Polygon:
                            {
                                return true;

                            }
                        case GeoJsonType.Feature:
                            {
                                Feature feature = (Feature)data;
                                if (feature.Geometry.Type != GeoJsonType.Polygon || feature.Geometry.Type != GeoJsonType.MultiPolygon)
                                {
                                    return false;
                                }
                                break;
                            }

                    }
                    return true;
                }
                catch (JsonSerializationException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool isPredicateValid(List<int> output, int from, int to)
        {
            if (output.Count == 0)
            {
                return false;
            }

            foreach (int number in output)
            {
                if (number > to)
                {
                    return false;
                }

                if (number < from)
                {
                    return false;
                }

            }
            return true;
        }

    }
}
