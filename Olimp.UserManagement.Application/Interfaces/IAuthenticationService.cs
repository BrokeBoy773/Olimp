﻿using CSharpFunctionalExtensions;

namespace Olimp.UserManagement.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Result> Register(
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
            CancellationToken ct);

        Task<Result<string>> Login(
            string email,
            string password,
            CancellationToken ct);
    }
}
