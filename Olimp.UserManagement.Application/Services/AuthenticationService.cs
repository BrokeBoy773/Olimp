using CSharpFunctionalExtensions;
using Olimp.UserManagement.Application.Interfaces;
using Olimp.UserManagement.Domain.Entities;
using Olimp.UserManagement.Infrastructure.Authentication.Interfaces;
using Olimp.UserManagement.Infrastructure.EntityFrameworkCore.Interfaces;

namespace Olimp.UserManagement.Application.Services
{
    public class AuthenticationService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
        : IAuthenticationService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task<Result<bool, List<string>>> RegisterAsync(
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            string postalCode,
            string region,
            string city,
            string street,
            string houseNumber,
            string apartmentNumber,
            string password,
            string repeatPassword,
            CancellationToken ct)
        {
            List<string> errorsList = [];


            Result<(List<string> ExistingEmails, List<string> ExistingPhoneNumbers), string> resultExistingEmailsAndExistingPhoneNumbers =
                await _userRepository.GetEmailsAndPhoneNumbersAsync(ct);

            if (resultExistingEmailsAndExistingPhoneNumbers.IsFailure)
            {
                errorsList.Add(resultExistingEmailsAndExistingPhoneNumbers.Error);
                return Result.Failure<bool, List<string>>(errorsList);
            }

            List<string> existingEmails = resultExistingEmailsAndExistingPhoneNumbers.Value.ExistingEmails;
            List<string> existingPhoneNumbers = resultExistingEmailsAndExistingPhoneNumbers.Value.ExistingPhoneNumbers;


            Result<string> resultPassword = ValidatePassword(password);

            if (resultPassword.IsFailure)
            {
                errorsList.Add(resultPassword.Error);
                return Result.Failure<bool, List<string>>(errorsList);
            }

            if (repeatPassword != resultPassword.Value)
            {
                errorsList.Add("Doesn't match the password");
                return Result.Failure<bool, List<string>>(errorsList);
            }

            string passwordHash = _passwordHasher.GenerateHashedPassword(resultPassword.Value);


            Result<User, List<string>> resultUser = User.Create(
                Guid.NewGuid(),
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
                apartmentNumber,
                passwordHash);

            if (resultUser.IsFailure)
            {
                errorsList.AddRange(resultUser.Error);
                return Result.Failure<bool, List<string>>(errorsList);
            }


            Result resultCreateUser = await _userRepository.CreateUserAsync(resultUser.Value, ct);

            if (resultCreateUser.IsFailure)
                errorsList.Add(resultCreateUser.Error);


            if (errorsList.Count > 0)
                return Result.Failure<bool, List<string>>(errorsList);

            return Result.Success<bool, List<string>>(true);
        }

        public async Task<Result<string>> LoginAsync(
            string email,
            string password,
            CancellationToken ct)
        {
            Result<User> resultUser = await _userRepository.GetUserByEmailAsync(email, ct);

            if (resultUser.IsFailure)
                return Result.Failure<string>(resultUser.Error);


            bool isVerify = _passwordHasher.VerifyPassword(password, resultUser.Value.PasswordHash.Hash);

            if (isVerify == false)
                return Result.Failure<string>("Wrong password");


            string jwtSecurityToken = _jwtProvider.GenerateJwtSecurityToken();

            return Result.Success(jwtSecurityToken);
        }

        private static Result<string> ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return Result.Failure<string>("password is null or white space");

            if (password.Length < 8)
                return Result.Failure<string>("password must be at least 8 characters long");

            if (password.Length > 128)
                return Result.Failure<string>("firstName exceeds maximum string length");

            return Result.Success(password);
        }
    }
}
