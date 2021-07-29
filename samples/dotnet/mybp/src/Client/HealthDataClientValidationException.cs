namespace MyBp.Client
{
    using Microsoft.AspNetCore.Mvc;
    using MyBp.Exceptions;

    public class HealthDataClientValidationException : ProblemDetailsException<ValidationProblemDetails>
    {
        public HealthDataClientValidationException(ValidationProblemDetails problemDetails) : base(problemDetails)
        {
        }
    }
}
