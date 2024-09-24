using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace Olimp.UserContext.Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        private static readonly string PhoneNumberPattern = @"^(7|8)(9)([0-9]{9})$";

        public string Number { get; }

        private PhoneNumber(string number)
        {
            Number = number;
        }

        public static Result<PhoneNumber> Create(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return Result.Failure<PhoneNumber>("phoneNumber is null or white space");

            string updatedPhoneNumber = phoneNumber.Trim();

            if (!Regex.IsMatch(updatedPhoneNumber, PhoneNumberPattern))
                return Result.Failure<PhoneNumber>("phoneNumber does not match the regular expression");

            if (updatedPhoneNumber.StartsWith('7'))
                updatedPhoneNumber = string.Concat("8", updatedPhoneNumber.AsSpan(1));

            PhoneNumber validPhoneNumber = new(updatedPhoneNumber);

            return Result.Success(validPhoneNumber);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
