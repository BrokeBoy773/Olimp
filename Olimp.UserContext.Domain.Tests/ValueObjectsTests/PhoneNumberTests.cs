using CSharpFunctionalExtensions;
using Olimp.UserContext.Domain.ValueObjects;
using Xunit;

namespace Olimp.UserContext.Domain.Tests.ValueObjectsTests
{
    public class PhoneNumberTests
    {
        [Fact]
        public void CreatePhoneNumber_WithValidInput_ReturnsPhoneNumberValueObject()
        {
            string phoneNumber = "89993332211";

            Result<PhoneNumber> result = PhoneNumber.Create(phoneNumber);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void CreatePhoneNumber_WithValidInputWithSpaces_ReturnsPhoneNumberValueObject()
        {
            string phoneNumber = "       89993332211           ";

            Result<PhoneNumber> result = PhoneNumber.Create(phoneNumber);

            Assert.Equal("89993332211", result.Value.Number);
        }

        [Fact]
        public void CreatePhoneNumber_WithValidInputStartingWith7_ReturnsPhoneNumberValueObject()
        {
            string phoneNumber = "79993332211";

            Result<PhoneNumber> result = PhoneNumber.Create(phoneNumber);

            Assert.Equal("89993332211", result.Value.Number);
        }

        [Theory]
        [InlineData("Иван")]
        [InlineData("1122")]
        [InlineData("-")]
        public void CreatePhoneNumber_WithInvalidInput_ReturnsPhoneNumberValueObject(string phoneNumber)
        {
            Result<PhoneNumber> result = PhoneNumber.Create(phoneNumber);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void CreatePhoneNumber_WithEmptyStrings_ReturnsPhoneNumberValueObject()
        {
            string phoneNumber = "";

            Result<PhoneNumber> result = PhoneNumber.Create(phoneNumber);

            Assert.True(result.IsFailure);
        }

#nullable disable
        [Fact]
        public void CreatePhoneNumber_WithNullStrings_ReturnsPhoneNumberValueObject()
        {
            string phoneNumber = null;

            Result<PhoneNumber> result = PhoneNumber.Create(phoneNumber);

            Assert.True(result.IsFailure);
        }
#nullable restore
    }
}
