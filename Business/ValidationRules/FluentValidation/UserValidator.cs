using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator:AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p=>p.UserId).NotEmpty();

            RuleFor(p=>p.Email).NotEmpty();

            RuleFor(p=>p.UserFirstName).NotEmpty();

            RuleFor(p=>p.UserLastName).NotEmpty();

            RuleFor(p=>p.Password).NotEmpty();
        }
    }
}
