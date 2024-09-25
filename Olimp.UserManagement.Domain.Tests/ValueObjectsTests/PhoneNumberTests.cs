using CSharpFunctionalExtensions;
using Olimp.UserManagement.Domain.ValueObjects;
using Xunit;

namespace Olimp.UserManagement.Domain.Tests.ValueObjectsTests
{
    public class PhoneNumberTests
    {
        [Fact]
        public void CreatePhoneNumber_WithValidInput_ReturnsPhoneNumberValueObject()
        {
            List<string> existingPhoneNumbers = ["89887776655", "89446663355"];
            string phoneNumber = "89993332211";

            Result<PhoneNumber> result = PhoneNumber.Create(phoneNumber, existingPhoneNumbers);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void CreatePhoneNumber_WithValidInputWithSpaces_ReturnsPhoneNumberValueObject()
        {
            List<string> existingPhoneNumbers = [];
            string phoneNumber = "       89993332211           ";

            Result<PhoneNumber> result = PhoneNumber.Create(phoneNumber, existingPhoneNumbers);

            Assert.Equal("89993332211", result.Value.Number);
        }

        [Fact]
        public void CreatePhoneNumber_WithValidInputStartingWith7_ReturnsPhoneNumberValueObject()
        {
            List<string> existingPhoneNumbers = [];
            string phoneNumber = "79993332211";

            Result<PhoneNumber> result = PhoneNumber.Create(phoneNumber, existingPhoneNumbers);

            Assert.Equal("89993332211", result.Value.Number);
        }

        [Theory]
        [InlineData("Иван")]
        [InlineData("1122")]
        [InlineData("-")]
        public void CreatePhoneNumber_WithInvalidInput_ReturnsPhoneNumberValueObject(string phoneNumber)
        {
            List<string> existingPhoneNumbers = [];

            Result<PhoneNumber> result = PhoneNumber.Create(phoneNumber, existingPhoneNumbers);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void CreatePhoneNumber_WithExistingPhoneNumber_ReturnsPhoneNumberValueObject()
        {
            List<string> existingPhoneNumbers = ["89993332211"];
            string phoneNumber = "89993332211";

            Result<PhoneNumber> result = PhoneNumber.Create(phoneNumber, existingPhoneNumbers);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void CreatePhoneNumber_WithEmptyString_ReturnsPhoneNumberValueObject()
        {
            List<string> existingPhoneNumbers = [];
            string phoneNumber = "";

            Result<PhoneNumber> result = PhoneNumber.Create(phoneNumber, existingPhoneNumbers);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void CreatePhoneNumber_WithOnlySpaces_ReturnsPhoneNumberValueObject()
        {
            List<string> existingPhoneNumbers = [];
            string phoneNumber = "    ";

            Result<PhoneNumber> result = PhoneNumber.Create(phoneNumber, existingPhoneNumbers);

            Assert.True(result.IsFailure);
        }

#nullable disable
        [Fact]
        public void CreatePhoneNumber_WithNullString_ReturnsPhoneNumberValueObject()
        {
            List<string> existingPhoneNumbers = [];
            string phoneNumber = null;

            Result<PhoneNumber> result = PhoneNumber.Create(phoneNumber, existingPhoneNumbers);

            Assert.True(result.IsFailure);
        }
#nullable restore
    }
}
