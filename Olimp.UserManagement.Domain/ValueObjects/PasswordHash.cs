using CSharpFunctionalExtensions;

namespace Olimp.UserManagement.Domain.ValueObjects
{
    public class PasswordHash : ValueObject
    {
        public string Hash { get; }

        private PasswordHash(string hash)
        {
            Hash = hash;
        }

        public static Result<PasswordHash> Create(string hash)
        {
            Result<string> resultHash = ValidateHash(hash);

            if (resultHash.IsFailure)
                return Result.Failure<PasswordHash>("hash is invalid");

            PasswordHash validPasswordHash = new(resultHash.Value);
            
            return Result.Success(validPasswordHash);
        }

        private static Result<string> ValidateHash(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
                return Result.Failure<string>("hash is null or white space");

            return Result.Success(hash);
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Hash;
        }
    }
}
