using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace Olimp.UserContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        private static readonly string RemoveWhiteSpacesPattern = @"\s+";
        private static readonly string NamePattern = @"^[a-zA-Zа-яА-ЯЁё-]+$";

        public string FirstName { get; }
        public string LastName { get; }

        private Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static Result<Name> Create(string firstName, string lastName)
        {
            Result<string> resultFirstName = ValidateFirstName(firstName);

            if (resultFirstName.IsFailure)
                return Result.Failure<Name>("firstName is invalid");


            Result<string> resultLastName = ValidateLastName(lastName);

            if (resultLastName.IsFailure)
                return Result.Failure<Name>("lastName is invalid");


            Name validName = new(resultFirstName.Value, resultLastName.Value);

            return Result.Success(validName);
        }

        private static Result<string> ValidateFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return Result.Failure<string>("firstName is null or white space");

            string updatedFirstName = Regex.Replace(firstName, RemoveWhiteSpacesPattern, match => " ").Trim();

            if (updatedFirstName.Length > 32)
                return Result.Failure<string>("firstName exceeds maximum string length");

            if (!Regex.IsMatch(updatedFirstName, NamePattern) || updatedFirstName == "-")
                return Result.Failure<string>("firstName contains invalid characters");

            return Result.Success(updatedFirstName);
        }

        private static Result<string> ValidateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                return Result.Failure<string>("lastName is null or white space");

            string updatedLastName = Regex.Replace(lastName, RemoveWhiteSpacesPattern, match => " ").Trim();

            if (updatedLastName.Length > 32)
                return Result.Failure<string>("lastName exceeds maximum string length");

            if (!Regex.IsMatch(updatedLastName, NamePattern) || updatedLastName == "-")
                return Result.Failure<string>("lastName contains invalid characters");

            return Result.Success(updatedLastName);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
