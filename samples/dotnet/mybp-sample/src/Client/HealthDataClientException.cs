namespace MyBp.Client
{
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using MyBp.Exceptions;

    public class HealthDataClientException : ProblemDetailsException<ProblemDetails>
    {
        public HealthDataClientException(ProblemDetails problemDetails) : base(problemDetails)
        {
        }
    }
}
