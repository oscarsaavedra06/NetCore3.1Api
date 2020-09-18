using CoreBuenasPracticas.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfraestructureBuenasPracticas.Validators
{
    public class PostValidator : AbstractValidator<PostDto>
    {
        public PostValidator()
        {
            RuleFor(post => post.Description)
                .NotNull()
                .Length(10, 500);

            RuleFor(post => post.Date)
                .NotNull()
                .LessThan(DateTime.Now);
        }
    }
}
