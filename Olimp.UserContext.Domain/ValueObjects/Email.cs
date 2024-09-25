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

        public static Result<Email> Create(string email, List<string> existingEmails)
        {
            Result<string> resultEmail = ValidateEmail(email, existingEmails);

            if (resultEmail.IsFailure)
                return Result.Failure<Email>("email is invalid");


            Email validEmail = new(resultEmail.Value);

            return Result.Success(validEmail);
        }

        private static Result<string> ValidateEmail(string email, List<string> existingEmails)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result.Failure<string>("email is null or white space");

            string updatedEmail = email.Trim();

            if (!Regex.IsMatch(updatedEmail, EmailPattern))
                return Result.Failure<string>("email does not match the regular expression");

            if (existingEmails.Any(existing => existing.Equals(updatedEmail, StringComparison.OrdinalIgnoreCase)))
                return Result.Failure<string>("email already exists");

            return Result.Success(updatedEmail);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EmailAddress;
        }
    }
}
