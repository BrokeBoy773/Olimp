using CSharpFunctionalExtensions;
using Olimp.UserManagement.Domain.ValueObjects;

namespace Olimp.UserManagement.Domain.Entities
{
    public class User
    {
        public Guid Id { get; }
        public Name Name { get; private set; } = null!;
        public Email Email { get; private set; } = null!;
        public PhoneNumber PhoneNumber { get; private set; } = null!;
        public Address Address { get; private set; } = null!;
        public PasswordHash PasswordHash { get; private set; } = null!;

        private User()
        {
        }

        private User(
            Guid id,
            Name name,
            Email email,
            PhoneNumber phoneNumber,
            Address address,
            PasswordHash passwordHash)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            PasswordHash = passwordHash;
        }

        public static Result<User, List<string>> Create(
            Guid id,
            string firstName,
            string lastName,
            string email,
            List<string> existingEmails,
            string phoneNumber,
            List<string> existingPhoneNumbers,
            string postalCode,
            string region,
            string city,
            string street,
            string houseNumber,
            string apartmentNumber,
            string passwordHash)
        {
            List<string> errorsList = [];

            Result<Name, List<string>> resultName = Name.Create(firstName, lastName);

            if (resultName.IsFailure)
                errorsList.AddRange(resultName.Error);


            Result<Email, List<string>> resultEmail = Email.Create(email, existingEmails);

            if (resultEmail.IsFailure)
                errorsList.AddRange(resultEmail.Error);


            Result<PhoneNumber, List<string>> resultPhoneNumber = PhoneNumber.Create(phoneNumber, existingPhoneNumbers);

            if (resultPhoneNumber.IsFailure)
                errorsList.AddRange(resultPhoneNumber.Error);


            Result<Address, List<string>> resultAddress = Address.Create(postalCode, region, city, street, houseNumber, apartmentNumber);

            if (resultAddress.IsFailure)
                errorsList.AddRange(resultAddress.Error);


            Result<PasswordHash, List<string>> resultPasswordHash = PasswordHash.Create(passwordHash);

            if (resultPasswordHash.IsFailure)
                errorsList.AddRange(resultPasswordHash.Error);


            if (errorsList.Count > 0)
                return Result.Failure<User, List<string>>(errorsList);

            User user = new(
                id,
                resultName.Value,
                resultEmail.Value,
                resultPhoneNumber.Value,
                resultAddress.Value,
                resultPasswordHash.Value);

            return Result.Success<User, List<string>>(user);
        }
    }
}
