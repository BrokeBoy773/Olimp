using CSharpFunctionalExtensions;
using Olimp.UserManagement.Domain.ValueObjects;
using Xunit;

namespace Olimp.UserManagement.Domain.Tests.ValueObjectsTests
{
    public class NameTests
    {
        [Fact]
        public void CreateName_WithValidInput_ReturnsNameValueObject()
        {
            string firstName = "Иван";
            string lastName = "Петров-Смирнов";

            Result<Name> result = Name.Create(firstName, lastName);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void CreateName_WithValidInputWithSpaces_ReturnsNameValueObject()
        {
            string firstName = "    Иван   ";
            string lastName = "        Петров    ";

            Result<Name> result = Name.Create(firstName, lastName);

            Assert.Equal("Иван", result.Value.FirstName);
            Assert.Equal("Петров", result.Value.LastName);
        }

        [Theory]
        [InlineData("!Иван", "П$тров")]
        [InlineData("12", "54")]
        [InlineData("-", "-")]
        public void CreateName_WithInvalidInput_ReturnsNameValueObject(string firstName, string lastName)
        {
            Result<Name> result = Name.Create(firstName, lastName);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void CreateName_WithStringExceedingMaximumLength_ReturnsNameValueObject()
        {
            string firstName = "ИванИванИванИванИванИванИванИванИванИванИванИванИванИванИванИван";
            string lastName = "ПетровПетровПетровПетровПетровПетровПетровПетровПетровПетровПетров";

            Result<Name> result = Name.Create(firstName, lastName);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void CreateName_WithEmptyStrings_ReturnsNameValueObject()
        {
            string firstName = "";
            string lastName = "";

            Result<Name> result = Name.Create(firstName, lastName);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void CreateName_WithOnlySpaces_ReturnsNameValueObject()
        {
            string firstName = "    ";
            string lastName = "    ";

            Result<Name> result = Name.Create(firstName, lastName);

            Assert.True(result.IsFailure);
        }

#nullable disable
        [Fact]
        public void CreateName_WithNullStrings_ReturnsNameValueObject()
        {
            string firstName = null;
            string lastName = null;

            Result<Name> result = Name.Create(firstName, lastName);

            Assert.True(result.IsFailure);
        }
#nullable restore
    }
}
