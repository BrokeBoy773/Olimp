using CSharpFunctionalExtensions;
using Olimp.UserContext.Domain.ValueObjects;

namespace Olimp.UserContext.Domain.Entities
{
    public class User
    {
        public Guid Id { get; }
        public Name Name { get; private set; } = null!;
        public Email Email { get; private set; } = null!;
        public PhoneNumber PhoneNumber { get; private set; } = null!;
        public Address Address { get; private set; } = null!;

        private User(
            Guid id,
            Name name,
            Email email,
            PhoneNumber phoneNumber,
            Address address)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public static Result<User> Create(
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
            string apartmentNumber)
        {
            Result<Name> resultName = Name.Create(firstName, lastName);

            if (resultName.IsFailure)
                return Result.Failure<User>("Name is invalid");


            Result<Email> resultEmail = Email.Create(email, existingEmails);

            if (resultEmail.IsFailure)
                return Result.Failure<User>("Email is invalid");


            Result<PhoneNumber> resultPhoneNumber = PhoneNumber.Create(phoneNumber, existingPhoneNumbers);

            if (resultPhoneNumber.IsFailure)
                return Result.Failure<User>("PhoneNumber is invalid");


            Result<Address> resultAddress = Address.Create(postalCode, region, city, street, houseNumber, apartmentNumber);

            if (resultAddress.IsFailure)
                return Result.Failure<User>("Address is invalid");


            User user = new(id, resultName.Value, resultEmail.Value, resultPhoneNumber.Value, resultAddress.Value);

            return Result.Success(user);
        }
    }
}
