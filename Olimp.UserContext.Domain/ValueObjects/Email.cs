using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace Olimp.UserContext.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        private static readonly string EmailPattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

        public string EmailAddress { get; }

        private Email(string emailAddress)
        {
            EmailAddress = emailAddress;
        }

        public static Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result.Failure<Email>("email is null or white space");

            string updatedEmail = email.Trim();

            if (Regex.IsMatch(updatedEmail, EmailPattern))
                return Result.Failure<Email>("email does not match the regular expression");

            Email validEmail = new(email);

            return Result.Success(validEmail);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EmailAddress;
        }
    }
}
