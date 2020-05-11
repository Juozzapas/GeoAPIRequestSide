using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebApplication.Functions;

namespace web_api_nunit_test
{
    class CheckPropertyValidation
    {
        public IList<ValidationResult> myValidation(object model, Mock<IValidationFunctions> mock)
        {
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider
                .Setup(x => x.GetService(typeof(IValidationFunctions)))
                .Returns(mock.Object);
            var result = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, serviceProvider.Object,null);
            Validator.TryValidateObject(model, validationContext, result,true);
            if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);

            return result;
        }
    }
}
