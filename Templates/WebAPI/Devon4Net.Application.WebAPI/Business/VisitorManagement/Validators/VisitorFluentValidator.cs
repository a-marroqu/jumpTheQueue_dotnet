using Devon4Net.Application.WebAPI.Business.VisitorManagement.Dto;
using Devon4Net.Infrastructure.FluentValidation;
using FluentValidation;

namespace Devon4Net.Application.WebAPI.Business.VisitorManagement.Validators
{
    /// <summary>
    /// VisitorFluentValidator implementation
    /// </summary>
    public class VisitorFluentValidator : CustomFluentValidator<VisitorDto>
    {
        /// <summary>
        /// VisitorFluentValidator constructor
        /// </summary>
        /// <param name="launchExceptionWhenError"></param>
        public VisitorFluentValidator(bool launchExceptionWhenError = false) : base(launchExceptionWhenError)
        {
        }

        public override void CustomValidate()
        {
            RuleFor(VisitorDto => VisitorDto.Name).NotNull();
            RuleFor(VisitorDto => VisitorDto.Name).NotEmpty();
            RuleFor(VisitorDto => VisitorDto.Name).Must(name => name.Contains(" "));

            RuleFor(VisitorDto => VisitorDto.Username).NotNull();
            RuleFor(VisitorDto => VisitorDto.Username).NotEmpty();
            RuleFor(VisitorDto => VisitorDto.PhoneNumber).NotNull();
            RuleFor(VisitorDto => VisitorDto.PhoneNumber).NotEmpty();
            RuleFor(VisitorDto => VisitorDto.Mail).NotNull();
            RuleFor(VisitorDto => VisitorDto.Mail).NotEmpty();
            RuleFor(VisitorDto => VisitorDto.Password).NotNull();
            RuleFor(VisitorDto => VisitorDto.Password).NotEmpty();
            RuleFor(VisitorDto => VisitorDto.Terms).Must(terms => terms == true);
        }
    }
}
