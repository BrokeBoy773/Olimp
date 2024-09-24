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
            if (string.IsNullOrWhiteSpace(firstName))
                return Result.Failure<Name>("firstName is null or white space");

            if (string.IsNullOrWhiteSpace(lastName))
                return Result.Failure<Name>("lastName is null or white space");


            string updatedFirstName = Regex.Replace(firstName, RemoveWhiteSpacesPattern, match => " ").Trim();
            string updatedLastName = Regex.Replace(lastName, RemoveWhiteSpacesPattern, match => " ").Trim();


            if (updatedFirstName.Length > 32)
                return Result.Failure<Name>("firstName exceeds maximum string length");

            if (updatedLastName.Length > 32)
                return Result.Failure<Name>("lastName exceeds maximum string length");


            if (!Regex.IsMatch(updatedFirstName, NamePattern) || updatedFirstName == "-")
                return Result.Failure<Name>("firstName contains invalid characters");

            if (!Regex.IsMatch(updatedLastName, NamePattern) || updatedLastName == "-")
                return Result.Failure<Name>("lastName contains invalid characters");

            Name validName = new(updatedFirstName, updatedLastName);

            return Result.Success(validName);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
