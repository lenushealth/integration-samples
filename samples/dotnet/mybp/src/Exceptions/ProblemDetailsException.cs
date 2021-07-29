using System;
using Microsoft.AspNetCore.Mvc;

namespace MyBp.Exceptions
{
    public class ProblemDetailsException<T> : ApplicationException where T : ProblemDetails
    {
        public T ProblemDetails { get; }
        
        public ProblemDetailsException(T problemDetails) : base(problemDetails.Detail)
        {
            this.ProblemDetails = problemDetails;
        }
    }

    public class ProblemDetailsException : ProblemDetailsException<ProblemDetails>
    {
        public ProblemDetailsException(ProblemDetails problemDetails) : base(problemDetails)
        {
        }
    }
}
