using CSharpFunctionalExtensions;
using Olimp.UserContext.Domain.Entities;

namespace Olimp.UserContext.Domain.ApplicationServices
{
    public class UserService
    {
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
            Result<User> resultUser = User.Create(
                id,
                firstName,
                lastName,
                email,
                existingEmails,
                phoneNumber,
                existingPhoneNumbers,
                postalCode,
                region,
                city,
                street,
                houseNumber,
                apartmentNumber);

            if (resultUser.IsFailure)
                return Result.Failure<User>("User is invalid");

            return Result.Success(resultUser.Value);
        }
    }
}
