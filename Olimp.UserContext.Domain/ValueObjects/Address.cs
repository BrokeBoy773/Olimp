using CSharpFunctionalExtensions;

namespace Olimp.UserContext.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public string PostalCode { get; }
        public string Region { get; }
        public string City { get; }
        public string Street { get; }
        public string HouseNumber { get; }
        public string ApartmentNumber { get; }

        private Address(
            string postalCode,
            string region,
            string city,
            string street,
            string houseNumber,
            string apartmentNumber)
        {
            PostalCode = postalCode;
            Region = region;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            ApartmentNumber = apartmentNumber;
        }

        public static Result<Address> Create(
            string postalCode,
            string region,
            string city,
            string street,
            string houseNumber,
            string apartmentNumber)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PostalCode;
            yield return Region;
            yield return City;
            yield return Street;
            yield return HouseNumber;
            yield return ApartmentNumber;
        }
    }
}
