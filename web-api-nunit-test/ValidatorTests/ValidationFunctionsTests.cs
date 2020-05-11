using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Functions;

namespace web_api_nunit_test.ValidatorTests
{
    [TestFixture]
    public class ValidationFunctionsTests
    {
        private static IEnumerable<TestCaseData> IsUsedOperatorValidPossitiveTestCaseData
        {
            get
            {
                yield return new TestCaseData(0).Returns(true).SetName("method 'isUsedOperatorValid' returns true when passed {0}.");
                yield return new TestCaseData(1).Returns(true).SetName("method 'isUsedOperatorValid' returns true when passed {0}.");
                yield return new TestCaseData(2).Returns(true).SetName("method 'isUsedOperatorValid' returns true when passed {0}.");
                yield return new TestCaseData(3).Returns(true).SetName("method 'isUsedOperatorValid' returns true when passed {0}.");
                yield return new TestCaseData(4).Returns(true).SetName("method 'isUsedOperatorValid' returns true when passed {0}.");
                yield return new TestCaseData(5).Returns(true).SetName("method 'isUsedOperatorValid' returns true when passed {0}.");
                yield return new TestCaseData(6).Returns(true).SetName("method 'isUsedOperatorValid' returns true when passed {0}.");
                yield return new TestCaseData(7).Returns(true).SetName("method 'isUsedOperatorValid' returns true when passed {0}.");
                yield return new TestCaseData(8).Returns(true).SetName("method 'isUsedOperatorValid' returns true when passed {0}.");
                yield return new TestCaseData(9).Returns(true).SetName("method 'isUsedOperatorValid' returns true when passed {0}.");
                yield return new TestCaseData(10).Returns(true).SetName("method 'isUsedOperatorValid' returns true when passed {0}.");
            }
        }

        [TestCaseSource(typeof(ValidationFunctionsTests), nameof(IsUsedOperatorValidPossitiveTestCaseData))]
        public bool Test_isUsedOperatorValid(int output)
        {
            ValidationFunctions functions = new ValidationFunctions();
            return functions.isUsedOperatorValid(output);

        }

        [Test, TestCase(TestName = "method 'isUsedOperatorValid' returns false when number is lower then 0")]
        public void Test_isUsedOperatorValid_when_method_input_negative_then_method_returns_false()
        {
            Random random = new Random();
            ValidationFunctions functions = new ValidationFunctions();
            bool result = functions.isUsedOperatorValid(random.Next(-1000, -1));
            Assert.AreEqual(false, result);
        }

        [Test, TestCase(TestName = "method 'isUsedOperatorValid' returns false when number is greater then 10")]
        public void Test_isUsedOperatorValid_when_method_input_greater_then_10_then_method_returns_false()
        {
            Random random = new Random();
            ValidationFunctions functions = new ValidationFunctions();
            bool result = functions.isUsedOperatorValid(random.Next(11, 1000));
            Assert.AreEqual(false, result);
        }

        [Test, TestCase(TestName = "method 'isDistanceValid' returns true when distance ins random int.")]
        public void Test_isDistanceValid_when_distance_is_number_then_return_true()
        {
            Random random = new Random();
            ValidationFunctions functions = new ValidationFunctions();
            bool result = functions.isDistanceValid("" + random.NextDouble());
            Assert.AreEqual(true, result);
        }

        [Test, TestCase(TestName = "method 'isDistanceValid' returns false when string is not int")]
        public void Test_isDistanceValid_when_distance_is_not_number_then_return_false()
        {
            Random random = new Random();
            ValidationFunctions functions = new ValidationFunctions();
            bool result = functions.isDistanceValid("test" + random.NextDouble());
            Assert.AreEqual(false, result);
        }

        [Test, TestCase(TestName = "method 'IsValidGeoJson' returns false when geoJSON does not start with {")]
        public void Test_IsValidGeoJson_when_input_starts_wrong_returns_false1()
        {
            Random random = new Random();

            string input = "[\"type\": \"Feature\",\"geometry\": {\"type\": \"Point\",\"coordinates\": [" + random.Next(100) + ", " + random.Next(100) + "]},\"properties\": {\"name\": \"Dinagat Islands\"}}";

            ValidationFunctions functions = new ValidationFunctions();

            bool result = functions.IsValidGeoJson(input);
            Assert.AreEqual(false, result);
        }

        [Test, TestCase(TestName = "method 'IsValidGeoJson' returns false when geoJSON does not end with }")]
        public void Test_IsValidGeoJson_when_input_starts_wrong_returns_false2()
        {
            Random random = new Random();

            string input = "{\"type\": \"Feature\",\"geometry\": {\"type\": \"Point\",\"coordinates\": [" + random.Next(100) + ", " + random.Next(100) + "]},\"properties\": {\"name\": \"Dinagat Islands\"}]";

            ValidationFunctions functions = new ValidationFunctions();

            bool result = functions.IsValidGeoJson(input);
            Assert.AreEqual(false, result);
        }

        [Test, TestCase(TestName = "method 'IsValidGeoJson' returns false when geoJSON does not start with { and end with }")]
        public void Test_IsValidGeoJson_when_input_starts_wrong_returns_false3()
        {
            Random random = new Random();

            string input = "[\"type\": \"Feature\",\"geometry\": {\"type\": \"Point\",\"coordinates\": [" + random.Next(100) + ", " + random.Next(100) + "]},\"properties\": {\"name\": \"Dinagat Islands\"}]";

            ValidationFunctions functions = new ValidationFunctions();

            bool result = functions.IsValidGeoJson(input);
            Assert.AreEqual(false, result);
        }

        [Test, TestCase(TestName = "method 'IsValidGeoJson' returns true when geoJSON is valid.")]
        public void Test_IsValidGeoJson_when_input_valid_returns_true()
        {
            Random random = new Random();

            string input = "{\"type\": \"Feature\",\"geometry\": {\"type\": \"Point\",\"coordinates\": [" + random.Next(100) + ", " + random.Next(100) + "]},\"properties\": {\"name\": \"Dinagat Islands\"}}";

            ValidationFunctions functions = new ValidationFunctions();

            bool result = functions.IsValidGeoJson(input);
            Assert.AreEqual(true, result);
        }

        [Test, TestCase(TestName = "method 'IsValidGeoJson' returns false when geoJSON is invalid.")]
        public void Test_IsValidGeoJson_when_input_invalid_returns_false()
        {
            Random random = new Random();

            string input = "{type\": \"Feature\",\"geometry\": {\"type\": \"Point\",\"coordinates\": [" + random.Next(100) + ", " + random.Next(100) + "]},\"properties\": {\"name\": \"Dinagat Islands\"}}";

            ValidationFunctions functions = new ValidationFunctions();

            bool result = functions.IsValidGeoJson(input);
            Assert.AreEqual(false, result);
        }

        [Test, TestCase(TestName = "method 'IsValidGeoJsonArray' returns false when geoJSON does not start with [")]
        public void Test_IIsValidGeoJsonArray_when_input_starts_wrong_returns_false()
        {
            Random random = new Random();

            string input = "{\"type\": \"Feature\",\"geometry\": {\"type\": \"Point\",\"coordinates\": [" + random.Next(100) + ", " + random.Next(100) + "]},\"properties\": {\"name\": \"Dinagat Islands\"}}]";

            ValidationFunctions functions = new ValidationFunctions();

            bool result = functions.IsValidGeoJsonArray(input);
            Assert.AreEqual(false, result);
        }

        [Test, TestCase(TestName = "method 'IsValidGeoJsonArray' returns false when geoJSON does not ends with ]")]
        public void Test_IIsValidGeoJsonArray_when_input_ends_wrong_returns_false()
        {
            Random random = new Random();

            string input = "[{\"type\": \"Feature\",\"geometry\": {\"type\": \"Point\",\"coordinates\": [" + random.Next(100) + ", " + random.Next(100) + "]},\"properties\": {\"name\": \"Dinagat Islands\"}}";

            ValidationFunctions functions = new ValidationFunctions();

            bool result = functions.IsValidGeoJsonArray(input);
            Assert.AreEqual(false, result);
        }

        [Test, TestCase(TestName = "method 'IsValidGeoJsonArray' returns false when geoJSON does not starts with [ and ends with ]")]
        public void Test_IsValidGeoJsonArray_when_input_starts_and_ends_wrong_returns_false()
        {
            Random random = new Random();

            string input = "{\"type\": \"Feature\",\"geometry\": {\"type\": \"Point\",\"coordinates\": [" + random.Next(100) + ", " + random.Next(100) + "]},\"properties\": {\"name\": \"Dinagat Islands\"}}";

            ValidationFunctions functions = new ValidationFunctions();

            bool result = functions.IsValidGeoJsonArray(input);
            Assert.AreEqual(false, result);
        }

        [Test, TestCase(TestName = "method 'IsValidGeoJsonArray' returns true when geoJSON is geoJSON array")]
        public void Test_IIsValidGeoJsonArray_when_input_is_geoJSON_array()
        {
            Random random = new Random();

            string input = "[{\"type\": \"Feature\",\"geometry\": {\"type\": \"Point\",\"coordinates\": [" + random.Next(100) + ", " + random.Next(100) + "]},\"properties\": {\"name\": \"Dinagat Islands\"}}]";

            ValidationFunctions functions = new ValidationFunctions();

            bool result = functions.IsValidGeoJsonArray(input);
            Assert.AreEqual(true, result);
        }

        [Test, TestCase(TestName = "method 'IsValidGeoJsonArray' returns false when geoJSON array_is_invalid")]
        public void Test_IIsValidGeoJsonArray_when_geoJSON_array_is_invalid()
        {
            Random random = new Random();

            string input = "[{\"type\": Feature,\"geometry\": {\"type\": \"Point\",\"coordinates\": [" + random.Next(100) + ", " + random.Next(100) + "]},\"properties\": {\"name\": \"Dinagat Islands\"}}]";

            ValidationFunctions functions = new ValidationFunctions();

            bool result = functions.IsValidGeoJsonArray(input);
            Assert.AreEqual(false, result);
        }

        [Test, TestCase(TestName = "method 'isPredicateValid' returns true when all numbers in predicate array are greater then 'from' value and lower then 'to' value.")]
        public void Test_isPredicateValid_returns_true_then_numbers_in_array_greater_then_from_and_lower_then_to()
        {
            Random random = new Random();
            List<int> output = new List<int>();
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));

            ValidationFunctions functions = new ValidationFunctions();
            bool result = functions.isPredicateValid(output, random.Next(-1000, 0), random.Next(1001, 9999));
            Assert.AreEqual(true, result);
        }

        [Test, TestCase(TestName = "method 'isPredicateValid' returns false when one of the numbers in list is greater then 'to' value.")]
        public void Test_isPredicateValid_returns_false_when_one_number_in_array_equal_or_greater_then_to()
        {
            Random random = new Random();
            List<int> output = new List<int>();
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1101, 9999));

            ValidationFunctions functions = new ValidationFunctions();
            bool result = functions.isPredicateValid(output, random.Next(-1000, 0), random.Next(1001, 1100));
            Assert.AreEqual(false, result);
        }

        [Test, TestCase(TestName = "method 'isPredicateValid' returns false when one of the numbers in list is lower then 'from' value.")]
        public void Test_isPredicateValid_returns_false_when_one_number_in_array_equal_or_lower_then_from()
        {
            Random random = new Random();
            List<int> output = new List<int>();
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(1, 1000));
            output.Add(random.Next(-1000, -100));

            ValidationFunctions functions = new ValidationFunctions();
            bool result = functions.isPredicateValid(output, random.Next(-100, 0), random.Next(1001, 9999));
            Assert.AreEqual(false, result);
        }

        [Test, TestCase(TestName = "method 'isPredicateValid' returns false when one of the numbers in aray is lower then 'from' value.")]
        public void Test_isPredicateValid_returns_false_when_list_is_empty()
        {
            Random random = new Random();
            List<int> output = new List<int>();

            ValidationFunctions functions = new ValidationFunctions();
            bool result = functions.isPredicateValid(output, random.Next(-1000, 0), random.Next(1001, 9999));
            Assert.AreEqual(false, result);
        }

    }
}
